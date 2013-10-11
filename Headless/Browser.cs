namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="Browser" />
    ///     class provides the implementation of a HTTP browsing session.
    /// </summary>
    /// <example>
    ///     <para>
    ///         The <see cref="Browser" /> class executes HTTP requests and returns an <see cref="IPage" /> result. It can
    ///         either return a strongly typed page (page model) or a dynamic page (dynamic model).
    ///     </para>
    ///     <para>
    ///         <b>Page Model</b>
    ///     </para>
    ///     <para>
    ///         The page model design uses strongly typed pages that provide reusability of a page and its model.
    ///     </para>
    ///     <code lang="C#" title="Page model example">
    /// <![CDATA[
    /// public class HomeIndexPage : HtmlPage
    /// {
    ///     public HtmlLink SignIn
    ///     {
    ///         get
    ///         {
    ///             return Find<HtmlLink>().ByText("Sign in");
    ///         }
    ///     }
    /// 
    ///     public override Uri TargetLocation
    ///     {
    ///         get
    ///         {
    ///             return HomeLocation.Index;
    ///         }
    ///     }
    /// }
    /// 
    /// public class SignInPage : HtmlPage
    /// {
    ///     public HtmlInput UserName
    ///     {
    ///         get
    ///         {
    ///             return Find<HtmlInput>().ByName("UserName");
    ///         }
    ///     }
    /// 
    ///     public HtmlInput Password
    ///     {
    ///         get
    ///         {
    ///             return Find<HtmlInput>().ByName("Password");
    ///         }
    ///     }
    /// 
    ///     public HtmlButton Submit
    ///     {
    ///         get
    ///         {
    ///             return Find<HtmlButton>().ByText("Sign in");
    ///         }
    ///     }
    /// 
    ///     public override Uri TargetLocation
    ///     {
    ///         get
    ///         {
    ///             return HomeLocation.SignIn;
    ///         }
    ///     }
    /// }
    /// 
    /// public class AccountIndexPage : HtmlPage
    /// {
    ///     public override Uri TargetLocation
    ///     {
    ///         get
    ///         {
    ///             return AccountLocation.Index;
    ///         }
    ///     }
    /// }
    /// 
    /// [TestMethod]
    /// public void SignInRedirectsToAccountIndexPageTest()
    /// {
    ///     using (var browser = new Browser())
    ///     {
    ///         var homePage = browser.GoTo<HomeIndexPage>();
    /// 
    ///         var signInPage = homePage.SignIn.Click<SignInPage>();
    /// 
    ///         signInPage.UserName.Value = "account name";
    ///         signInPage.Password.Value = "account password";
    /// 
    ///         // The response from a click using the page model will validate location and 200 status code by default
    ///         signInPage.Submit.Click<AccountIndexPage>();
    ///     }
    /// }
    /// 
    /// ]]>
    /// </code>
    ///     <para>
    ///         <b>Dynamic Model</b>
    ///     </para>
    ///     <para>
    ///         The dynamic model design uses dynamic types to make requests and process results.
    ///     </para>
    ///     <code lang="C#" title="Page model example">
    /// <![CDATA[
    /// [TestMethod]
    /// public void SignInRedirectsToAccountIndexPageTest()
    /// {
    ///     using (var browser = new Browser())
    ///     {
    ///         var homePage = browser.GoTo<HomeIndexPage>();
    /// 
    ///         var signInPage = homePage.SignIn.Click();
    /// 
    ///         signInPage.UserName.Value = "account name";
    ///         signInPage.Password.Value = "account password";
    /// 
    ///         // The response from a click using the dynamic model will only validate a 200 status code by default,
    ///         // it does not validate the ultimate location because there is no known target
    ///         var accountPage = signInPage.Submit.Click();
    /// 
    ///         // Validate final url of the page against an expected value
    ///         accountPage.IsOn(AccountLocation.Index).Should().BeTrue();
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class Browser : IDisposable, IBrowser
    {
        /// <summary>
        ///     Stores the http client.
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        ///     Stores the http handler.
        /// </summary>
        private readonly HttpClientHandler _handler;

        /// <summary>
        ///     Stores whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     The user agent.
        /// </summary>
        private string _userAgent = BuildDefaultUserAgent();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Browser" /> class.
        /// </summary>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "The handler is disposed by the client when it is disposed.")]
        public Browser()
        {
            _handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            _client = new HttpClient(_handler);
            ContentTypeResolver = new DefaultPageContentTypeResolver();
            LocationValidator = new RelaxedFolderLocationValidator();
        }

        /// <summary>
        ///     Clears the cookies.
        /// </summary>
        public void ClearCookies()
        {
            _handler.CookieContainer = new CookieContainer();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public T Execute<T>(HttpRequestMessage request, HttpStatusCode expectedStatusCode, IPageFactory pageFactory)
            where T : IPage, new()
        {
            try
            {
                return ExecuteInternal<T>(request, expectedStatusCode, pageFactory);
            }
            catch (AggregateException ex)
            {
                var exception = ex.Flatten();

                if (exception.InnerExceptions.Count > 1)
                {
                    throw;
                }

                var canceledException = exception.InnerExceptions[0] as TaskCanceledException;

                if (canceledException == null)
                {
                    throw;
                }

                throw new TimeoutException(canceledException.Message, canceledException);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _client.Dispose();
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            _disposed = true;
        }

        /// <summary>
        ///     Builds the default user agent.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> value.
        /// </returns>
        private static string BuildDefaultUserAgent()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;

            var info = FileVersionInfo.GetVersionInfo(assemblyPath);

            const string UserAgentFormat = "Headless ({0}.{1}.{2})";

            return string.Format(
                CultureInfo.CurrentCulture, 
                UserAgentFormat, 
                info.ProductMajorPart, 
                info.ProductMinorPart, 
                info.ProductBuildPart);
        }

        /// <summary>
        /// Determines whether the specified response is redirect.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified response is redirect; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsRedirect(HttpResponseMessage response)
        {
            if (response.Headers.Location == null)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.Ambiguous)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Moved)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.MultipleChoices)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Found)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.RedirectKeepVerb)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.RedirectMethod)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.SeeOther)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Executes the request internally.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="pageFactory">
        /// The page factory.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="request"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="pageFactory"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="HttpOutcomeException">
        /// </exception>
        private T ExecuteInternal<T>(
            HttpRequestMessage request, 
            HttpStatusCode expectedStatusCode, 
            IPageFactory pageFactory) where T : IPage, new()
        {
            var page = default(T);

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (pageFactory == null)
            {
                throw new ArgumentNullException("pageFactory");
            }

            request.Headers.Add("user-agent", UserAgent);

            var currentResourceLocation = request.RequestUri;

            var outcomes = new List<HttpOutcome>();

            var stopwatch = Stopwatch.StartNew();

            var task = _client.SendAsync(request);

            Uri redirectLocation = null;
            bool requiresRedirect;

            using (var response = task.Result)
            {
                stopwatch.Stop();

                var outcome = new HttpOutcome(
                    currentResourceLocation, 
                    response.RequestMessage.Method, 
                    response.StatusCode, 
                    response.ReasonPhrase, 
                    stopwatch.Elapsed);

                outcomes.Add(outcome);

                requiresRedirect = IsRedirect(response);

                if (requiresRedirect)
                {
                    redirectLocation = response.Headers.Location;
                }
                else
                {
                    // This the final response
                    var result = new HttpResult(outcomes);

                    page = pageFactory.Create<T>(this, response, result);
                }
            }

            while (requiresRedirect)
            {
                // Get the redirect address
                Uri fullRedirectLocation;

                if (redirectLocation.IsAbsoluteUri)
                {
                    fullRedirectLocation = redirectLocation;
                }
                else
                {
                    fullRedirectLocation = new Uri(currentResourceLocation, redirectLocation);
                }

                currentResourceLocation = fullRedirectLocation;
                stopwatch = Stopwatch.StartNew();
                task = _client.GetAsync(currentResourceLocation);

                using (var response = task.Result)
                {
                    stopwatch.Stop();

                    var outcome = new HttpOutcome(
                        currentResourceLocation, 
                        response.RequestMessage.Method, 
                        response.StatusCode, 
                        response.ReasonPhrase, 
                        stopwatch.Elapsed);

                    outcomes.Add(outcome);

                    requiresRedirect = IsRedirect(response);

                    if (requiresRedirect)
                    {
                        redirectLocation = response.Headers.Location;
                    }
                    else
                    {
                        // This the final response
                        var result = new HttpResult(outcomes);

                        page = pageFactory.Create<T>(this, response, result);
                    }
                }
            }

            SetCurrentPage(page);

            var lastOutcome = outcomes[outcomes.Count - 1];

            if (lastOutcome.StatusCode != expectedStatusCode)
            {
                var result = new HttpResult(outcomes);

                throw new HttpOutcomeException(result, expectedStatusCode);
            }

            // Validate that the final address matches the page
            if (page.IsOn(currentResourceLocation) == false)
            {
                var result = new HttpResult(outcomes);

                throw new HttpOutcomeException(result, page.TargetLocation);
            }

            return page;
        }

        /// <summary>
        /// Sets the current page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        private void SetCurrentPage(IPage page)
        {
            var dynamicPage = page as DynamicResolverPage;

            if (dynamicPage == null)
            {
                Page = page;
            }
            else
            {
                Page = dynamicPage.ResolvedPage;
            }
        }

        /// <inheritdoc />
        public IPageContentTypeResolver ContentTypeResolver
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the cookies for the browser session.
        /// </summary>
        /// <value>
        ///     The cookies for the browser session.
        /// </value>
        public CookieContainer Cookies
        {
            [DebuggerStepThrough]
            get
            {
                return _handler.CookieContainer;
            }
        }

        /// <inheritdoc />
        public ILocationValidator LocationValidator
        {
            get;
            set;
        }

        /// <inheritdoc />
        public IPage Page
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public TimeSpan Timeout
        {
            get
            {
                return _client.Timeout;
            }

            set
            {
                _client.Timeout = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether cookies are used in the browsing session.
        /// </summary>
        /// <value>
        ///     <c>true</c> if cookies are used in the browsing session; otherwise, <c>false</c>.
        /// </value>
        public bool UseCookies
        {
            [DebuggerStepThrough]
            get
            {
                return _handler.UseCookies;
            }

            [DebuggerStepThrough]
            set
            {
                _handler.UseCookies = value;
            }
        }

        /// <summary>
        ///     Gets or sets the user agent.
        /// </summary>
        /// <value>
        ///     The user agent.
        /// </value>
        public string UserAgent
        {
            get
            {
                return _userAgent;
            }

            set
            {
                _userAgent = value;
            }
        }
    }
}
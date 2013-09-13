namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Headless.Activation;
    using Microsoft.Win32;

    /// <summary>
    ///     The <see cref="BrowserExtensions" />
    ///     class is used to provide extension methods to the <see cref="IBrowser" /> interface.
    /// </summary>
    public static class BrowserExtensions
    {
        /// <summary>
        /// Browses to the specified location.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static dynamic GoTo(this IBrowser browser, Uri location)
        {
            return browser.GoTo(location, HttpStatusCode.OK);
        }

        /// <summary>
        /// Browses to the specified location.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static dynamic GoTo(this IBrowser browser, Uri location, HttpStatusCode expectedStatusCode)
        {
            var pageFactory = new DefaultPageFactory();

            var dynamicPage = browser.GoTo<DynamicResolverPage>(location, expectedStatusCode, pageFactory);

            return dynamicPage.ResolvedPage;
        }

        /// <summary>
        /// Browses to the location defined by the specified page type.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        public static T GoTo<T>(this IBrowser browser) where T : IPage, new()
        {
            var page = new T();

            return browser.GoTo<T>(page.TargetLocation, HttpStatusCode.OK);
        }

        /// <summary>
        /// Browses to the location defined by the specified page type and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        public static T GoTo<T>(this IBrowser browser, HttpStatusCode expectedStatusCode) where T : IPage, new()
        {
            var page = new T();

            return browser.GoTo<T>(page.TargetLocation, expectedStatusCode);
        }

        /// <summary>
        /// Browses to the specified location.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="location">
        /// The specific location to request rather than that identified by the page.
        /// </param>
        /// <returns>
        /// A <see cref="Page"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static T GoTo<T>(this IBrowser browser, Uri location) where T : IPage, new()
        {
            return browser.GoTo<T>(location, HttpStatusCode.OK);
        }

        /// <summary>
        /// Browses to the specified location and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static T GoTo<T>(this IBrowser browser, Uri location, HttpStatusCode expectedStatusCode)
            where T : IPage, new()
        {
            var pageFactory = new DefaultPageFactory();

            return browser.GoTo<T>(location, expectedStatusCode, pageFactory);
        }

        /// <summary>
        /// Browses to the specified location and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <param name="pageFactory">
        /// The page factory.
        /// </param>
        /// <returns>
        /// A <see cref="IPage"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="pageFactory"/> parameter is <c>null</c>.
        /// </exception>
        public static T GoTo<T>(
            this IBrowser browser, 
            Uri location, 
            HttpStatusCode expectedStatusCode, 
            IPageFactory pageFactory) where T : IPage, new()
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser");
            }

            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            if (pageFactory == null)
            {
                throw new ArgumentNullException("pageFactory");
            }

            using (var request = new HttpRequestMessage(HttpMethod.Get, location))
            {
                return browser.Execute<T>(request, expectedStatusCode, pageFactory);
            }
        }

        /// <summary>
        /// Posts the parameters to the specified location.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static dynamic PostTo(this IBrowser browser, IEnumerable<PostEntry> parameters, Uri location)
        {
            return browser.PostTo(parameters, location, HttpStatusCode.OK);
        }

        /// <summary>
        /// Posts the parameters to the specified location.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// An <see cref="IPage"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static dynamic PostTo(
            this IBrowser browser, 
            IEnumerable<PostEntry> parameters, 
            Uri location, 
            HttpStatusCode expectedStatusCode)
        {
            var pageFactory = new DefaultPageFactory();

            var dynamicPage = browser.PostTo<DynamicResolverPage>(parameters, location, expectedStatusCode, pageFactory);

            return dynamicPage.ResolvedPage;
        }

        /// <summary>
        /// Posts the parameters to the location defined by the specified page type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        public static T PostTo<T>(this IBrowser browser, IEnumerable<PostEntry> parameters) where T : IPage, new()
        {
            var page = new T();

            return browser.PostTo<T>(parameters, page.TargetLocation, HttpStatusCode.OK);
        }

        /// <summary>
        /// Posts the parameters to the specified location.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static T PostTo<T>(this IBrowser browser, IEnumerable<PostEntry> parameters, Uri location)
            where T : IPage, new()
        {
            return browser.PostTo<T>(parameters, location, HttpStatusCode.OK);
        }

        /// <summary>
        /// Posts the parameters to the specified location and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        public static T PostTo<T>(
            this IBrowser browser, 
            IEnumerable<PostEntry> parameters, 
            HttpStatusCode expectedStatusCode) where T : IPage, new()
        {
            var page = new T();

            return browser.PostTo<T>(parameters, page.TargetLocation, expectedStatusCode);
        }

        /// <summary>
        /// Posts the parameters to the specified location and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The POST parameters.
        /// </param>
        /// <param name="location">
        /// The location to post to.
        /// </param>
        /// <param name="expectedStatusCode">
        /// The expected status code.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static T PostTo<T>(
            this IBrowser browser, 
            IEnumerable<PostEntry> parameters, 
            Uri location, 
            HttpStatusCode expectedStatusCode) where T : IPage, new()
        {
            var pageFactory = new DefaultPageFactory();

            return browser.PostTo<T>(parameters, location, expectedStatusCode, pageFactory);
        }

        /// <summary>
        /// Posts the parameters to the specified location and validates the status code.
        /// </summary>
        /// <typeparam name="T">
        /// The type of page to return.
        /// </typeparam>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="parameters">
        /// The post parameters.
        /// </param>
        /// <param name="location">
        /// The location to post to.
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
        /// The <paramref name="browser"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="parameters"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="location"/> parameter is <c>null</c>.
        /// </exception>
        public static T PostTo<T>(
            this IBrowser browser, 
            IEnumerable<PostEntry> parameters, 
            Uri location, 
            HttpStatusCode expectedStatusCode, 
            IPageFactory pageFactory) where T : IPage, new()
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            if (pageFactory == null)
            {
                throw new ArgumentNullException("pageFactory");
            }
            
            var parameterSet = parameters.ToList();

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, location))
                {
                    using (var multiPart = BuildPostContent(parameterSet))
                    {
                        request.Content = multiPart;

                        return browser.Execute<T>(request, expectedStatusCode, pageFactory);
                    }
                }
            }
            finally
            {
                // Ensure that any stream based file post entries are disposed
                var streamEntries = parameterSet.OfType<PostFileStreamEntry>();

                foreach (var streamEntry in streamEntries)
                {
                    streamEntry.Dispose();
                }
            }
        }

        /// <summary>
        /// Builds the content of the multipart.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// A <see cref="MultipartFormDataContent"/> value.
        /// </returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "All content types and streams are disposed when the HTTP Request is disposed.")]
        private static MultipartFormDataContent BuildMultipartContent(List<PostEntry> parameters)
        {
            var multiPart = new MultipartFormDataContent();

            for (var index = 0; index < parameters.Count; index++)
            {
                var entry = parameters[index];
                var fileEntry = entry as PostFileEntry;

                if (fileEntry == null)
                {
                    var formDataContent = new StringContent(entry.Value);

                    formDataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"" + entry.Name + "\""
                    };

                    multiPart.Add(formDataContent);
                }
                else if (fileEntry.IsValid == false)
                {
                    var fileContent = new StringContent(string.Empty);

                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"" + fileEntry.Name + "\"", 
                        FileName = "\"\""
                    };
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    multiPart.Add(fileContent);
                }
                else
                {
                    var fileStream = fileEntry.ReadContent();
                    var fileContent = new StreamContent(fileStream);
                    var fileName = Path.GetFileName(fileEntry.Value);
                    var fileExtension = Path.GetExtension(fileEntry.Value);
                    var contentType = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + fileExtension, "Content Type", null) as string;

                    if (string.IsNullOrWhiteSpace(contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"" + fileEntry.Name + "\"", 
                        FileName = "\"" + fileName + "\""
                    };
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                    multiPart.Add(fileContent);
                }
            }

            return multiPart;
        }

        /// <summary>
        /// Builds the content of the post.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// A <see cref="HttpContent"/> value.
        /// </returns>
        private static HttpContent BuildPostContent(List<PostEntry> parameters)
        {
            if (parameters.OfType<PostFileEntry>().Any())
            {
                return BuildMultipartContent(parameters);
            }

            // There are no files to post so this is going to be url encoded form data
            var pairs = parameters.Select(x => new KeyValuePair<string, string>(x.Name, x.Value));

            return new FormUrlEncodedContent(pairs);
        }
    }
}
namespace Headless.IntegrationTests
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Text;
    using System.Threading;

    /// <summary>
    ///     The <see cref="WebServer" /> class is used to provide a built in web server.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", 
        Justification = "This is a false positive. The class is named correctly.")]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", 
        Justification = "The HttpListener type implements a IDisposableprivate privately.")]
    public class WebServer
    {
        /// <summary>
        ///     The _listener.
        /// </summary>
        private readonly HttpListener _listener = new HttpListener();

        /// <summary>
        ///     The _responder method.
        /// </summary>
        private readonly Func<HttpListenerContext, string> _responderMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServer"/> class.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="prefixes">
        /// The prefixes.
        /// </param>
        public WebServer(Func<HttpListenerContext, string> method, params string[] prefixes)
        {
            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            foreach (var prefix in prefixes)
            {
                _listener.Prefixes.Add(prefix);
            }

            _responderMethod = method;
            _listener.Start();
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "Generic catch is used to ensure that exceptions do not crash the server.")]
        public void Start()
        {
            ThreadPool.QueueUserWorkItem(
                o =>
                {
                    Trace.WriteLine("Webserver running...");

                    try
                    {
                        while (_listener.IsListening)
                        {
                            ThreadPool.QueueUserWorkItem(
                                c =>
                                {
                                    var context = c as HttpListenerContext;
                                    try
                                    {
                                        var response = string.Empty;

                                        if (_responderMethod != null)
                                        {
                                            response = _responderMethod(context);
                                        }

                                        var buf = Encoding.UTF8.GetBytes(response);

                                        context.Response.ContentLength64 = buf.Length;
                                        context.Response.OutputStream.Write(buf, 0, buf.Length);
                                    }
                                    catch
                                    {
                                        // suppress any exceptions
                                    }
                                    finally
                                    {
                                        // always close the stream
                                        context.Response.OutputStream.Close();
                                    }
                                }, 
                                _listener.GetContext());
                        }
                    }
                    catch
                    {
                        // suppress any exceptions
                    }
                });
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
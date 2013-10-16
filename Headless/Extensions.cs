namespace Headless
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    ///     The <see cref="Extensions" />
    ///     class is used to provide common extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Traces the details of the HTTP result.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="result"/> parameter is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method uses <see cref="Trace.WriteLine(string)"/> to trace each outcome in the result and the overall
        ///     response time.
        /// </remarks>
        public static void TraceResults(this HttpResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            foreach (var outcome in result.Outcomes)
            {
                Trace.WriteLine(outcome);
            }

            Trace.WriteLine("Total response time: " + result.ResponseTime.TotalMilliseconds + " milliseconds");
        }

        /// <summary>
        /// Builds to string.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> value.
        /// </returns>
        internal static string BuildToString(this IPage page)
        {
            const string Layout = "{0}: {1} - {2} ({3}) - {4}";

            return string.Format(
                CultureInfo.CurrentCulture, 
                Layout, 
                page.GetType().Name, 
                page.Location, 
                page.StatusCode, 
                (int)page.StatusCode, 
                page.StatusDescription);
        }
    }
}
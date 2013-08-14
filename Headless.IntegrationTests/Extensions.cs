namespace Headless.IntegrationTests
{
    using System.Diagnostics;

    /// <summary>
    ///     The <see cref="Extensions" />
    ///     class provides common extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Traces the results.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        public static void TraceResults(this HttpResult result)
        {
            foreach (var outcome in result.Outcomes)
            {
                Trace.WriteLine(outcome);
            }

            Trace.WriteLine("Total response time: " + result.ResponseTime.TotalMilliseconds + " milliseconds");
        }
    }
}
namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// The <see cref="HttpResult{T}"/>
    ///     class provides the results for an HTTP request.
    /// </summary>
    /// <typeparam name="T">
    /// The type of page expected from the request.
    /// </typeparam>
    public class HttpResult<T> where T : Page
    {
        /// <summary>
        ///     Stores the http request outcomes.
        /// </summary>
        private readonly IReadOnlyCollection<HttpOutcome> _outcomes;

        /// <summary>
        ///     Stores the page.
        /// </summary>
        private readonly T _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResult{T}"/> class.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="outcomes">
        /// The http request outcomes.
        /// </param>
        internal HttpResult(T page, IReadOnlyCollection<HttpOutcome> outcomes)
        {
            _page = page;
            _outcomes = outcomes;
        }

        /// <summary>
        ///     Gets the outcomes of the HTTP request.
        /// </summary>
        /// <value>
        ///     The outcomes.
        /// </value>
        public IReadOnlyCollection<HttpOutcome> Outcomes
        {
            [DebuggerStepThrough]
            get
            {
                return _outcomes;
            }
        }

        /// <summary>
        ///     Gets the page.
        /// </summary>
        /// <value>
        ///     The page.
        /// </value>
        public T Page
        {
            [DebuggerStepThrough]
            get
            {
                return _page;
            }
        }

        /// <summary>
        ///     Gets the response time.
        /// </summary>
        /// <value>
        ///     The response time.
        /// </value>
        public TimeSpan ResponseTime
        {
            get
            {
                var totalTime = Outcomes.Aggregate(
                    TimeSpan.Zero, 
                    (current, outcome) => current.Add(outcome.ResponseTime));

                return totalTime;
            }
        }
    }
}
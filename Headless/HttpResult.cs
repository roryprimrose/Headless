namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// The <see cref="HttpResult"/>
    ///     class provides the results for an HTTP request.
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        ///     Stores the http request outcomes.
        /// </summary>
        private readonly IReadOnlyCollection<HttpOutcome> _outcomes;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResult"/> class.
        /// </summary>
        /// <param name="outcomes">
        /// The http request outcomes.
        /// </param>
        public HttpResult(IEnumerable<HttpOutcome> outcomes)
        {
            if (outcomes == null)
            {
                throw new ArgumentNullException("outcomes");
            }

            _outcomes = new ReadOnlyCollection<HttpOutcome>(outcomes.ToList());
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
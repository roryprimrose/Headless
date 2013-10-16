namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Headless.Properties;

    /// <summary>
    /// The <see cref="HttpResult"/>
    ///     class provides the overall result for a HTTP request.
    /// </summary>
    [Serializable]
    public class HttpResult
    {
        /// <summary>
        ///     Stores the http request outcomes.
        /// </summary>
        private readonly ReadOnlyCollection<HttpOutcome> _outcomes;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResult"/> class.
        /// </summary>
        /// <param name="outcomes">
        /// The http request outcomes.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="outcomes"/> parameter is <c>null</c>.
        /// </exception>
        public HttpResult(IEnumerable<HttpOutcome> outcomes)
        {
            if (outcomes == null)
            {
                throw new ArgumentNullException("outcomes");
            }

            var httpOutcomes = outcomes.ToList();

            if (httpOutcomes.Count == 0)
            {
                throw new ArgumentException(Resources.HttpResult_NoHttpOutcomeProvided);
            }

            _outcomes = new ReadOnlyCollection<HttpOutcome>(httpOutcomes);
        }

        /// <summary>
        ///     Gets the outcomes of the HTTP request.
        /// </summary>
        /// <value>
        ///     The outcomes.
        /// </value>
        public ReadOnlyCollection<HttpOutcome> Outcomes
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
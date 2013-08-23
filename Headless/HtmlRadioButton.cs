namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlRadioButton" />
    ///     class is used to represent an HTML radio button set.
    /// </summary>
    [SupportedTag("input", "type", "radio")]
    public class HtmlRadioButton : HtmlFormElement
    {
        /// <summary>
        ///     The related nodes.
        /// </summary>
        private readonly IList<IXPathNavigable> _relatedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlRadioButton"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlRadioButton(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
            _relatedNodes = FindRelatedNodes(page, node);
        }

        /// <summary>
        /// Finds the related nodes.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private IList<IXPathNavigable> FindRelatedNodes(IHtmlPage page, IXPathNavigable node)
        {
            var form = node.GetHtmlForm(page);

            // We want to use the finder to build the xpath query for radio buttons
            var query = form.Find<HtmlRadioButton>().BuildElementQuery() + "[@name='" + Name + "']";

            var navigator = form.Node.GetNavigator();

            var matchingNodes = navigator.Select(query);

            return new List<IXPathNavigable>(matchingNodes.OfType<IXPathNavigable>());
        }

        /// <summary>
        /// Gets the values available by the radio button set.
        /// </summary>
        /// <value>
        /// The values available by the radio button set.
        /// </value>
        public IEnumerable<string> Values
        {
            get
            {
                for (var index = 0; index < _relatedNodes.Count; index++)
                {
                    var node = _relatedNodes[index];
                    var navigator = node.GetNavigator();

                    yield return navigator.GetAttribute("value", string.Empty);
                }
            }
        }

        /// <inheritdoc />
        public override string Value
        {
            get
            {
                for (var index = 0; index < _relatedNodes.Count; index++)
                {
                    var node = _relatedNodes[index];
                    var navigator = node.GetNavigator();

                    if (navigator.IsChecked())
                    {
                        return navigator.GetAttribute("value", string.Empty);
                    }
                }

                return null;
            }

            set
            {
                for (var index = 0; index < _relatedNodes.Count; index++)
                {
                    var node = _relatedNodes[index];
                    var navigator = node.GetNavigator();

                    if (value == null)
                    {
                        navigator.SetChecked(false);

                        continue;
                    }

                    var nodeValue = navigator.GetAttribute("value", string.Empty);

                    if (nodeValue.Equals(value, StringComparison.Ordinal))
                    {
                        navigator.SetChecked(true);
                    }
                    else
                    {
                        navigator.SetChecked(false);
                    }
                }
            }
        }
    }
}
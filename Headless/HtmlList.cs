namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.XPath;
    using Headless.Activation;

    /// <summary>
    ///     The <see cref="HtmlList" />
    ///     class provides access to the HTML list and dropdown elements.
    /// </summary>
    [SupportedTag("select")]
    public class HtmlList : HtmlFormElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlList"/> class.
        /// </summary>
        /// <param name="page">
        /// The owning page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public HtmlList(IHtmlPage page, IXPathNavigable node) : base(page, node)
        {
        }

        /// <summary>
        /// Deselects the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Deselect(string value)
        {
            var node = this[value];

            if (node == null)
            {
                return;
            }

            node.SetSelected(false);
        }

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Select(string value)
        {
            var node = this[value];

            if (node == null)
            {
                return;
            }

            node.SetSelected(true);
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            var selectedEntries = new List<PostEntry>();

            foreach (var node in Nodes)
            {
                var optionNavigator = node.GetNavigator();

                if (optionNavigator.IsSelected() == false)
                {
                    continue;
                }

                var value = optionNavigator.GetAttribute("value", string.Empty);

                var entry = new PostEntry(Name, value);

                selectedEntries.Add(entry);
            }

            if (selectedEntries.Count == 0)
            {
                return selectedEntries;
            }

            if (IsDropDown)
            {
                var lastEntry = selectedEntries[selectedEntries.Count - 1];

                return new[]
                {
                    lastEntry
                };
            }

            return selectedEntries;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is a drop down list.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is a drop down list; otherwise, <c>false</c>.
        /// </value>
        public bool IsDropDown
        {
            get
            {
                var navigator = Node.GetNavigator();

                var allowsMuliple = navigator.MoveToAttribute("multiple", string.Empty);

                if (allowsMuliple)
                {
                    return false;
                }

                return true;
            }
        }

        /// <inheritdoc />
        public override string Value
        {
            get
            {
                var values = SelectedValues;

                return string.Join(Environment.NewLine, values);
            }

            set
            {
                List<string> values = null;

                if (value != null)
                {
                    values = value.Split(
                        new[]
                        {
                            Environment.NewLine
                        }, 
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                foreach (var node in Nodes)
                {
                    var navigator = node.GetNavigator();

                    if (value == null)
                    {
                        navigator.SetSelected(false);

                        continue;
                    }

                    var nodeValue = navigator.GetAttribute("value", string.Empty);

                    if (values.Contains(nodeValue, StringComparer.Ordinal))
                    {
                        navigator.SetSelected(true);
                    }
                    else
                    {
                        navigator.SetSelected(false);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <value>
        /// The selected values.
        /// </value>
        public IEnumerable<string> SelectedValues
        {
            get
            {
                foreach (var node in Nodes)
                {
                    var navigator = node.GetNavigator();

                    if (navigator.IsSelected())
                    {
                        var value = navigator.GetAttribute("value", string.Empty);

                        yield return value;
                    }
                }
            }
        }


        /// <summary>
        ///     Gets the values available by the radio button set.
        /// </summary>
        /// <value>
        ///     The values available by the radio button set.
        /// </value>
        public IEnumerable<string> Values
        {
            get
            {
                foreach (var node in Nodes)
                {
                    yield return node.GetNavigator().GetAttribute("value", string.Empty);
                }
            }
        }

        /// <summary>
        ///     Gets the nodes.
        /// </summary>
        /// <value>
        ///     The nodes.
        /// </value>
        protected IReadOnlyCollection<IXPathNavigable> Nodes
        {
            get
            {
                var navigator = Node.GetNavigator();

                var nodes = navigator.Select("option").OfType<IXPathNavigable>();

                return new ReadOnlyCollection<IXPathNavigable>(nodes.ToList());
            }
        }

        /// <summary>
        /// Gets the <see cref="IXPathNavigable"/> with the specified value.
        /// </summary>
        /// <value>
        /// The <see cref="IXPathNavigable"/>.
        /// </value>
        /// <param value="value">
        /// The value.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <see cref="IXPathNavigable"/> value.
        /// </returns>
        public IXPathNavigable this[string value]
        {
            get
            {
                var navigator = Node.GetNavigator();

                var nodes = navigator.Select("option[@value='" + value + "']").OfType<IXPathNavigable>();
                var node = nodes.FirstOrDefault();

                return node;
            }
        }
    }
}
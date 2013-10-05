namespace Headless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.XPath;
    using Headless.Activation;
    using Headless.Properties;

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
        /// <exception cref="HtmlElementNotFoundException">
        /// No option element found for the specified value.
        /// </exception>
        public void Deselect(string value)
        {
            var item = this[value];

            if (item == null)
            {
                throw BuildItemNotFoundException(value);
            }

            item.Selected = false;
        }

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="HtmlElementNotFoundException">
        /// No option element found for the specified value.
        /// </exception>
        public void Select(string value)
        {
            var item = this[value];

            if (item == null)
            {
                throw BuildItemNotFoundException(value);
            }
            
            item.Selected = true;
        }

        /// <inheritdoc />
        protected internal override IEnumerable<PostEntry> BuildPostData()
        {
            var selectedItems = SelectedItems;

            if (selectedItems.Count == 0)
            {
                yield break;
            }

            if (IsDropDown)
            {
                // Return only the last item
                var lastEntry = selectedItems.Last();

                var postEntry = new PostEntry(Name, lastEntry.PostValue);

                yield return postEntry;
            }
            else
            {
                foreach (var selectedItem in selectedItems)
                {
                    var postEntry = new PostEntry(Name, selectedItem.PostValue);

                    yield return postEntry;
                }
            }
        }

        /// <summary>
        /// Throws the item not found.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="HtmlElementNotFoundException"/> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private HtmlElementNotFoundException BuildItemNotFoundException(string value)
        {
            var message = string.Format(CultureInfo.CurrentCulture, Resources.HtmlList_NoOptionFoundForValue, value);

            return new HtmlElementNotFoundException(message, Node);
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

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public IReadOnlyCollection<HtmlListItem> Items
        {
            get
            {
                var items = Find<HtmlListItem>().All();

                return new ReadOnlyCollection<HtmlListItem>(items.ToList());
            }
        }

        /// <summary>
        ///     Gets the selected items.
        /// </summary>
        /// <value>
        ///     The selected items.
        /// </value>
        public IReadOnlyCollection<HtmlListItem> SelectedItems
        {
            get
            {
                var items = Find<HtmlListItem>().AllByPredicate(x => x.Selected).ToList();

                if (items.Count > 0)
                {
                    return new ReadOnlyCollection<HtmlListItem>(items);
                }

                if (IsDropDown == false)
                {
                    return new ReadOnlyCollection<HtmlListItem>(items);
                }

                // This is a drop down but nothing is explicltly selected
                // We will take the first available item
                var implicitItems = new List<HtmlListItem>();
                var firstItem = Items.FirstOrDefault();

                if (firstItem != null)
                {
                    implicitItems.Add(firstItem);
                }

                return new ReadOnlyCollection<HtmlListItem>(implicitItems);
            }
        }

        /// <summary>
        ///     Gets the selected values in the list.
        /// </summary>
        /// <value>
        ///     The selected values in the list.
        /// </value>
        /// <exception cref="HtmlElementNotFoundException">No option element found for the specified value.</exception>
        public IEnumerable<string> SelectedValues
        {
            get
            {
                return SelectedItems.Select(x => x.PostValue);
            }

            set
            {
                var items = Items.ToList();

                items.ForEach(x => x.Selected = false);

                if (value == null)
                {
                    return;
                }

                foreach (var item in value)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }

                    var matchingItems = items.Where(x => x.PostValue.Equals(item, StringComparison.Ordinal)).ToList();

                    if (matchingItems.Count == 0)
                    {
                        throw BuildItemNotFoundException(item);
                    }

                    matchingItems.ForEach(x => x.Selected = true);
                }
            }
        }

        /// <inheritdoc />
        /// <exception cref="HtmlElementNotFoundException">No option element found for the specified value.</exception>
        public override string Value
        {
            get
            {
                var values = SelectedValues.ToList();

                if (values.Count == 0)
                {
                    return string.Empty;
                }

                return values.Aggregate((current, next) => current + Environment.NewLine + next);
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

                SelectedValues = values;
            }
        }

        /// <summary>
        ///     Gets the values available in the list.
        /// </summary>
        /// <value>
        ///     The values available in the list.
        /// </value>
        public IEnumerable<string> Values
        {
            get
            {
                return Items.Select(x => x.PostValue);
            }
        }

        /// <summary>
        /// Gets the <see cref="HtmlListItem"/> with the specified value.
        /// </summary>
        /// <value>
        /// The <see cref="HtmlListItem"/>.
        /// </value>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A <see cref="HtmlListItem"/> value.
        /// </returns>
        public HtmlListItem this[string value]
        {
            get
            {
                var matchingItems =
                    Find<HtmlListItem>()
                        .AllByPredicate(x => x.PostValue.Equals(value, StringComparison.Ordinal));

                return matchingItems.FirstOrDefault();
            }
        }
    }
}
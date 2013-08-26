namespace Headless
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.XPath;
    using Headless.Properties;

    /// <summary>
    ///     The <see cref="XPathNavigableExtensions" />
    ///     class provides extension methods for the <see cref="IXPathNavigable" /> interface.
    /// </summary>
    public static class XPathNavigableExtensions
    {
        /// <summary>
        /// Gets the navigator.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <returns>
        /// A <see cref="XPathNavigator"/> value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The navigator could not be created.
        /// </exception>
        public static XPathNavigator GetNavigator(this IXPathNavigable navigable)
        {
            if (navigable == null)
            {
                throw new ArgumentNullException("navigable");
            }

            var navigator = navigable.CreateNavigator();

            if (navigator == null)
            {
                throw new InvalidOperationException(Resources.XPathNavigator_NavigatorNotCreated);
            }

            return navigator;
        }

        /// <summary>
        /// Determines whether the specified navigable has the specified attribute.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified navigable has the attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this IXPathNavigable navigable, string name)
        {
            var navigator = navigable.GetNavigator();

            var checkedFound = navigator.MoveToAttribute(name, string.Empty);

            return checkedFound;
        }

        /// <summary>
        /// Determines whether the specified navigable is checked.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified navigable is checked; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsChecked(this IXPathNavigable navigable)
        {
            return HasAttribute(navigable, "checked");
        }

        /// <summary>
        /// Determines whether the specified navigable is selected.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified navigable is selected; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSelected(this IXPathNavigable navigable)
        {
            return HasAttribute(navigable, "selected");
        }

        /// <summary>
        /// Sets the attribute value against the specified navigable instance.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <param name="localName">
        /// Name of the local.
        /// </param>
        /// <param name="namespaceUri">
        /// The namespace URI.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// The navigator could not be created.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The XML document cannot be edited.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#", 
            Justification =
                "The methods on XPathNavigator use string parameter types and these parameter values are passthrough.")]
        public static void SetAttribute(
            this IXPathNavigable navigable, 
            string prefix, 
            string localName, 
            string namespaceUri, 
            string value)
        {
            var navigator = GetNavigator(navigable);

            if (navigator.CanEdit == false)
            {
                throw new InvalidOperationException(Resources.XPathNavigator_EditNotAllowed);
            }

            // Check if given localName exist
            if (navigator.MoveToAttribute(localName, namespaceUri))
            {
                // Exist, so set current attribute with new value.
                navigator.SetValue(value);

                // Move navigator back to beginning of node
                navigator.MoveToParent();
            }
            else
            {
                // Does not exist, create the new attribute
                navigator.CreateAttribute(prefix, localName, namespaceUri, value);
            }
        }

        /// <summary>
        /// Sets the checked state of the specified navigable instance.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <param name="value">
        /// if set to <c>true</c> [value].
        /// </param>
        public static void SetChecked(this IXPathNavigable navigable, bool value)
        {
            var navigator = navigable.GetNavigator();

            var hasAttribute = navigator.MoveToAttribute("checked", string.Empty);

            if (hasAttribute == value)
            {
                // No change is required
                return;
            }

            if (value)
            {
                // Set the attribute
                navigator.SetAttribute(string.Empty, "checked", string.Empty, "checked");
            }
            else
            {
                // Remove the checked attribute if it exists
                navigator.DeleteSelf();
            }
        }

        /// <summary>
        /// Sets the selected.
        /// </summary>
        /// <param name="navigable">
        /// The navigable.
        /// </param>
        /// <param name="value">
        /// if set to <c>true</c> [value].
        /// </param>
        public static void SetSelected(this IXPathNavigable navigable, bool value)
        {
            var navigator = navigable.GetNavigator();

            var hasAttribute = navigator.MoveToAttribute("selected", string.Empty);

            if (hasAttribute == value)
            {
                // No change is required
                return;
            }

            if (value)
            {
                // Set the attribute
                navigator.SetAttribute(string.Empty, "selected", string.Empty, "selected");
            }
            else
            {
                // Remove the checked attribute if it exists
                navigator.DeleteSelf();
            }
        }
    }
}
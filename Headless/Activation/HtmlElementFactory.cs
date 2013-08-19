namespace Headless.Activation
{
    using System;
    using System.Globalization;
    using System.Xml.XPath;

    /// <summary>
    ///     The <see cref="HtmlElementFactory" />
    ///     class is used to create <see cref="HtmlElement" /> instances.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This library uses
    ///         <see
    ///             cref="Activator.CreateInstance(System.Type,System.Reflection.BindingFlags,System.Reflection.Binder,object[],System.Globalization.CultureInfo)">
    ///             Activator.CreateInstance
    ///         </see>
    ///         for creating <see cref="HtmlElement" /> instances rather than the new() type constraint so that a cleaner API
    ///         can
    ///         be provided while maintaining the element constructor constraints.
    ///     </p>
    ///     <p>
    ///         It is also located under this namespace so that developers can explicitly include it when they want to create
    ///         their
    ///         own instances and so it does not otherwise pollute intellisense when they don't need it.
    ///     </p>
    /// </remarks>
    public static class HtmlElementFactory
    {
        /// <summary>
        /// Creates the specified page.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to return.
        /// </typeparam>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// A <typeparamref name="T"/> value.
        /// </returns>
        public static T Create<T>(IHtmlPage page, IXPathNavigable node) where T : HtmlElement
        {
            // Find the most appropriate type that supports this node
            var sourceType = typeof(T);

            // Find a constructor on the type that takes in HtmlPage and IXPathNavigable
            var typeToCreate = sourceType.FindBestMatchingType(node);

            if (typeof(HtmlElement).IsAssignableFrom(typeToCreate) == false)
            {
                var message = string.Format(
                    CultureInfo.CurrentCulture, 
                    "The instance could not be created because {0} does not inherit from {1}.", 
                    typeToCreate.FullName, 
                    typeof(HtmlElement).FullName);

                throw new InvalidOperationException(message);
            }

            return (T)Activator.CreateInstance(typeToCreate, page, node);
        }
    }
}
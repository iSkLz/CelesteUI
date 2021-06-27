using System;
using System.Collections.Generic;
using System.Xml;

/*
 * CelesteUI language library
 * Element context implementation
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Contains information about an element being parsed.
    /// </summary>
    public class CuiParsingContext
    {
        /// <summary>
        /// Represents the container document.
        /// </summary>
        public CuiDocument Document;

        /// <summary>
        /// Represents the markup of the element.
        /// </summary>
        public XmlElement XML;

        /// <summary>
        /// Represents the markup of the element's parent.
        /// </summary>
        public XmlElement ParentXML;

        /// <summary>
        /// Creates a new instance for the specified document and XML element.
        /// </summary>
        /// <param name="document">The container document of the element</param>
        /// <param name="element">The element's markup</param>
        /// <param name="getChildren">A function that parses the children of the element</param>
        /// <param name="getChildren">A function that parses the children of the element</param>
        internal CuiParsingContext(CuiDocument document, XmlElement element,
            Func<CuiElement, List<CuiElement>> getChildren, Func<CuiElement> getParent, Action<CuiElement> parseChildren)
        {
            Document = document;
            XML = element;
            GetChildrenFunc = getChildren;
            GetParentFunc = getParent;
            ParseChildrenFunc = parseChildren;
        }

        #region Context Functions
        private readonly Func<CuiElement, List<CuiElement>> GetChildrenFunc;
        private readonly Func<CuiElement> GetParentFunc;
        private readonly Action<CuiElement> ParseChildrenFunc;

        public List<CuiElement> GetChildren(CuiElement element)
            => GetChildrenFunc(element);

        public CuiElement GetParent()
            => GetParentFunc();

        public void ParseChildren(CuiElement element)
            => ParseChildrenFunc(element);
        #endregion

        /// <summary>
        /// Attempts to retrieve the definition that corresponds to the specified XML element name.
        /// </summary>
        /// <returns>A <see cref="CuiElementDefinition"/> if one is found.</returns>
        /// <exception cref="KeyNotFoundException" />
        public CuiElementDefinition GetDefinition()
        {
            return Document.Elements.TryGetValue(XML.Name, out var def)
                ? def
                : throw new KeyNotFoundException("No element of the specified name is defined in the document.");
        }
    }
}

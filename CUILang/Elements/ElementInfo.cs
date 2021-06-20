using System.Collections.Generic;
using System.Xml;

/*
 * CelesteUI language library
 * Element information definition
 * 
 * Author: SkLz
 * Last edit: 20/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes an XML CUI element.
    /// </summary>
    public class CUIElementInfo
    {
        /// <summary>
        /// Represents the container document.
        /// </summary>
        public CUIDocument Document;

        /// <summary>
        /// Represents the parent element if one exists and null otherwise.
        /// </summary>
        public CUIElement Parent;

        /// <summary>
        /// Represents the markup of the element.
        /// </summary>
        public XmlElement XML;

        /// <summary>
        /// Creates a new instance for the specified document and XML element.
        /// </summary>
        /// <param name="document">The container document of the element</param>
        /// <param name="element">The element's markup</param>
        public CUIElementInfo(CUIDocument document, XmlElement element)
        {
            Document = document;
            XML = element;
        }

        /// <summary>
        /// Creates a new instance for the specified parent and XML element.
        /// </summary>
        /// <param name="document">The parent element of the element</param>
        /// <param name="element">The element's markup</param>
        public CUIElementInfo(CUIElement parent, XmlElement element)
            : this(parent.Document, element)
        {
            Parent = parent;
        }

        /// <summary>
        /// Attempts to retrieve the definition that corresponds to the specified XML element name.
        /// </summary>
        /// <returns>A <see cref="CUIElementDefinition"/> if one is found.</returns>
        /// <exception cref="KeyNotFoundException" />
        public CUIElementDefinition GetDefinition()
        {
            if (Document.Elements.TryGetValue(XML.Name, out var def))
            {
                return def;
            }
            else
            {
                throw new KeyNotFoundException("No element of the specified name is defined in the document.");
            }
        }
    }
}

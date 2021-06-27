using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/*
 * CelesteUI language library
 * CelesteUI document implementation
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a CelesteUI document.
    /// </summary>
    public sealed partial class CuiDocument
    {
        #region Parse Fields
        // Those fields are specific to and are changed every parse.
        /// <summary>
        /// Contains the macro defined on this document.
        /// </summary>
        public Dictionary<string, ICuiMacro> Macros;

        /// <summary>
        /// Contains the elements defined on this document.
        /// </summary>
        public Dictionary<string, CuiElementDefinition> Elements;
        #endregion

        /// <summary>
        /// Represents the document's XML markup.
        /// </summary>
        public XmlDocument Markup = new XmlDocument();

        #region Constructors
        /// <summary>
        /// Creates a document from a stream.
        /// </summary>
        /// <param name="stream">The input stream</param>
        public CuiDocument(Stream stream)
        {
            Markup.Load(stream);
            stream.Close();
        }

        /// <summary>
        /// Creates a document from an XML reader.
        /// </summary>
        /// <param name="reader">The XML input.</param>
        public CuiDocument(XmlReader reader)
        {
            Markup.Load(reader);
        }

        /// <summary>
        /// Creates a document from an XML markup string.
        /// </summary>
        /// <param name="xml">The XML input.</param>
        public CuiDocument(string xml)
        {
            Markup.LoadXml(xml);
        }

        /// <summary>
        /// Creates a document from an XML document.
        /// </summary>
        /// <param name="document">The XML input.</param>
        public CuiDocument(XmlDocument document)
        {
            Markup = document;
        }

        /// <summary>
        /// Creates a document from an XML element.
        /// </summary>
        /// <param name="root">The XML root element.</param>
        public CuiDocument(XmlElement root)
        {
            Markup.AppendChild(root);
        }
        #endregion

        /// <summary>
        /// Parses the XML tree into CUI elements and returns them.
        /// </summary>
        /// <returns>An indexed dictionary of elements that have an ID assigned to them.</returns>
        public Dictionary<string, CuiElement> Parse()
        {
            Macros = new Dictionary<string, ICuiMacro>();
            Elements = new Dictionary<string, CuiElementDefinition>();

            var dict = new Dictionary<string, CuiElement>();

            // TODO: Uhhh, what's the capitalization convention for local methods?
            CuiParsingContext createContext(XmlElement element, CuiElement parent)
            {
                return new CuiParsingContext(this, element, (cuiElement) =>
                {
                    var list = new List<CuiElement>();
                    foreach (XmlElement childElement in element.ChildNodes)
                    {
                        list.Add(parseElement(childElement, cuiElement));
                    }
                    return list;
                }, () => parent, (cuiElement) =>
                {
                    foreach (XmlElement childElement in element.ChildNodes)
                    {
                        parseElement(childElement, cuiElement);
                    }
                })
                {
                    ParentXML = element.ParentNode.NodeType == XmlNodeType.Element
                        ? (XmlElement)element.ParentNode
                        : null
                };
            };

            CuiElement parseElement(XmlElement element, CuiElement parent)
            {
                var context = createContext(element, parent);
                var parsedElement = context.GetDefinition().Parse(context);
                if (parsedElement.HasID) dict.Add(parsedElement.ID, parsedElement);
                return parsedElement;
            }

            parseElement(Markup.DocumentElement, null);
            return dict;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;

/*
 * CelesteUI language library
 * Element definition implementation
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a CUI element and exposes functionality to instantiate it.
    /// </summary>
    /// <remarks>
    /// This class is written generically to cover the needs of a typical element.
    /// If custom behavior is needed, inherit the class and override the methods.
    /// </remarks>
    public class CuiElementDefinition
    {
        /// <summary>
        /// References the property <see cref="CuiElement.Document"/>.
        /// </summary>
        protected static readonly PropertyInfo ElementDocumentField = typeof(CuiElement).GetProperty("Document");

        /// <summary>
        /// References the property <see cref="CuiElement.Parent"/>.
        /// </summary>
        protected static readonly PropertyInfo ElementParentField = typeof(CuiElement).GetProperty("Parent");

        /// <summary>
        /// References the property <see cref="CuiElement.Children"/>.
        /// </summary>
        protected static readonly PropertyInfo ElementChildrenField = typeof(CuiElement).GetProperty("Children");

        /// <summary>
        /// Represents the type of the element of this definition.
        /// </summary>
        public readonly Type ElementType;

        /// <summary>
        /// Represents the name of the XML element of this definition.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Contains the attributes of the element of this definition.
        /// </summary>
        protected readonly List<CuiAttributeDefinition> Attributes = new List<CuiAttributeDefinition>();

        public IEnumerable<CuiAttributeDefinition> GetAttributes()
        {
            // Fun fact: (List<CUIAttribute>)Def.GetAttributes() allows you to bypass this middleware function.
            return Attributes;
        }

        /// <summary>
        /// Constructs an element definition with the specified name and for the specified element type.
        /// </summary>
        /// <typeparam name="TElement">The element type</typeparam>
        /// <param name="name">The definition's XML element name</param>
        /// <returns>The resultant definition.</returns>
        public static CuiElementDefinition CreateDefinition<TElement>(string name)
            => new CuiElementDefinition(name, typeof(TElement));

        /// <summary>
        /// Constructs an element definition with the specified name and for the specified element type.
        /// </summary>
        /// <param name="name">The definition's XML element name</param>
        /// <param name="elementType">The element type</typeparam>
        public CuiElementDefinition(string name, Type elementType) : this(name)
        {
            if (elementType == null)
                elementType = typeof(CuiElement);

            if (!elementType.InheritsFrom<CuiElement>())
                throw new ArgumentException("The element type doesn't derive from CUIElement.", nameof(elementType));

            ElementType = elementType;
        }

        /// <summary>
        /// Constructs an element definition with the specified name.
        /// </summary>
        protected CuiElementDefinition(string name)
        {
            if ((Name = name) == null)
                throw new ArgumentNullException(nameof(name), "Null element name.");

            // ID attribute is present on all elements
            DefineAttribute(CuiCommonLibrary.ID);
        }

        /// <summary>
        /// Adds an attribute to the definition
        /// </summary>
        /// <param name="definition">The attribute to add</param>
        public virtual void DefineAttribute(CuiAttributeDefinition definition)
        {
            Attributes.Add(definition);
        }

        /// <summary>
        /// Adds a list of attributes to the definition
        /// </summary>
        /// <param name="definitions">The attributes to add</param>
        public virtual void DefineAttributes(params CuiAttributeDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                DefineAttribute(definition);
            }
        }

        /// <summary>
        /// Parses the markup of an element into an instance.
        /// </summary>
        /// <param name="info">Information of the element</param>
        /// <returns>The resultant CUI element</returns>
        public virtual CuiElement Parse(CuiParsingContext info)
        {
            var attributes = new CuiAttributesCollection(ParseAttributes(info));
            var element = CreateElement(attributes, info);
            info.ParseChildren(element);
            return element;
        }

        /// <summary>
        /// Enumerates the definition's attributes and parses them one by one.
        /// </summary>
        /// <param name="info">Information of the element</param>
        /// <returns>An iterator that parses an attribute definition and returns the result on each iteration.</returns>
        protected virtual IEnumerable<CuiAttribute> ParseAttributes(CuiParsingContext info)
        {
            foreach (var attr in Attributes)
            {
                yield return attr.Parse(info);
            }
        }

        /// <summary>
        /// Creates an element instance with the specified attributes.
        /// </summary>
        /// <param name="attributes">The attributes to pass</param>
        /// <param name="info">Information of the element</param>
        /// <returns>The resultant CUI element</returns>
        protected virtual CuiElement CreateElement(CuiAttributesCollection attributes, CuiParsingContext info)
        {
            // Invoke the constructor
            var element = (CuiElement)Activator.CreateInstance(ElementType, attributes);

            // Initialize attributes
            attributes.AssignOwner(element);

            // Initialize the element
            element.Initialize();

            return element;
        }
    }
}

using System.Collections.Generic;

/*
 * CelesteUI language library
 * Element implementation
 * 
 * Author: SkLz
 * Last edit: 20/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a CUI element. This class is abstract.
    /// </summary>
    /// <remarks>
    /// Elements are left to implement their own activation methods to create needed instances.
    /// If an element creates none (it doesn't represent a specific object), it can simply not implement any activation method.
    /// Inside activation methods, elements should evaluate their activation time attributes.
    /// Elements are also left to implement their own update methods. It is common for update methods to have the access modifier "protected" and to accept one parameter which is the instance being updated.
    /// Upon activation, it is also common for elements that need updating and define an update method to set their instances to call their update methods. This allows third parties to modify elements and apply changes to the attributes and have the changes visible on the instances without modifying them directly.
    /// </remarks>
    public abstract class CUIElement
    {
        /// <summary>
        /// Represents the container document of the element.
        /// </summary>
        public readonly CUIDocument Document;

        /// <summary>
        /// Represents the parent of the element if one exists, and null otherwise.
        /// </summary>
        public readonly CUIElement Parent;

        /// <summary>
        /// Contains a sorted list of the element's attributes.
        /// </summary>
        protected readonly List<CUIAttribute> Attributes;

        /// <summary>
        /// Creates a new instance without assigning a document, a parent or a list of attributes.
        /// </summary>
        public CUIElement()
        {
        }

        /// <summary>
        /// Creates a new instance and assigns a document and an array of attributes.
        /// </summary>
        /// <param name="document">The container document to assign</param>
        /// <param name="attributes">The array of attributes to assign</param>
        public CUIElement(CUIDocument document, params CUIAttribute[] attributes)
            : this(document, new List<CUIAttribute>(attributes))
        {
        }

        /// <summary>
        /// Creates a new instance and assigns a parent element and an array of attributes.
        /// </summary>
        /// <param name="parent">The container element to assign</param>
        /// <param name="attributes">The array of attributes to assign</param>
        public CUIElement(CUIElement parent, params CUIAttribute[] attributes)
            : this(parent, new List<CUIAttribute>(attributes))
        {
        }

        /// <summary>
        /// Creates a new instance and assigns a document and a list of attributes
        /// </summary>
        /// <param name="document">The container document to assign</param>
        /// <param name="attributes">The list of attributes to assign</param>
        public CUIElement(CUIDocument document, List<CUIAttribute> attributes)
        {
            Document = document;
            Attributes = attributes;
        }

        /// <summary>
        /// Creates a new instance and assigns a parent element and a list of attributes
        /// </summary>
        /// <param name="parent">The container element to assign</param>
        /// <param name="attributes">The list of attributes to assign</param>
        public CUIElement(CUIElement parent, List<CUIAttribute> attributes)
            : this(parent.Document, attributes)
        {
            Parent = parent;
        }

        /// <summary>
        /// Initializes the element's contents.
        /// </summary>
        /// <remarks>
        /// This is where the element evaluates its creation time attributes and does the rest of the initialization work.
        /// </remarks>
        public abstract void Initialize();

        /// <summary>
        /// Represents an enumerable list of parents starting from this element's parent ending with the root element.
        /// </summary>
        /// <remarks>
        /// If this element is the root element, the resultant enumerable contains 0 items (no iterations).
        /// </remarks>
        /// <returns>An iterator that works its way through the tree of parents.</returns>
        public IEnumerable<CUIElement> Parents()
        {
            var elem = Parent;

            while (elem != null)
            {
                yield return elem;
                elem = elem.Parent;
            }
        }
    }
}

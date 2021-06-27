using System.Collections.Generic;

/*
 * CelesteUI language library
 * Element implementation
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
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
    public class CuiElement
    {
        /// <summary>
        /// Represents the container document of the element.
        /// </summary>
        public CuiDocument Document { get; protected set; }

        /// <summary>
        /// Represents the parent of the element if it was retrieved.
        /// </summary>
        public CuiElement Parent { get; protected set; }

        /// <summary>
        /// Contains the element's children if they were retrieved.
        /// </summary>
        public List<CuiElement> Children { get; protected set; }

        /// <summary>
        /// Represents the element's identifier is one was specified.
        /// </summary>
        public readonly string ID;

        /// <summary>
        /// Checks whether the element has been assigned an identifier or not.
        /// </summary>
        /// <value>
        /// Whether the element has an identifier or not.
        /// </value>
        public bool HasID => ID != null;

        /// <summary>
        /// Contains the element's attributes.
        /// </summary>
        protected readonly CuiAttributesCollection Attributes;

        /// <summary>
        /// Creates an element with the specified ID and attributes.
        /// </summary>
        /// <param name="attributes">The attributes assigned to the element</param>
        /// <param name="id">The ID of the element or null if none is assigned</param>
        protected CuiElement(CuiAttributesCollection attributes, string id)
        {
            ID = id;
            Attributes = attributes;
        }

        /// <summary>
        /// Creates an element with the specified attributes.
        /// </summary>
        /// <remarks>
        /// This automatically resolves the ID from the attributes.
        /// </remarks>
        /// <param name="attributes">The attributes assigned to the element</param>
        public CuiElement(CuiAttributesCollection attributes)
            : this(attributes, (string)attributes["ID"].Value)
        {
        }

        /// <summary>
        /// Initializes the element.
        /// </summary>
        /// <remarks>
        /// This is where the element evaluates its creation time attributes and does the rest of the initialization work.
        /// </remarks>
        public virtual void Initialize() { }

        /// <summary>
        /// Represents an enumerable list of parents starting from this element's parent ending with the root element.
        /// </summary>
        /// <remarks>
        /// If this element is the root element, the resultant enumerable contains 0 items (no iterations).
        /// </remarks>
        /// <returns>An iterator that works its way through the tree of parents.</returns>
        public IEnumerable<CuiElement> Parents()
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

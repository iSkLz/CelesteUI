using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

/*
 * CelesteUI language library
 * Attribute collection class
 * 
 * Author: SkLz
 * Last edit: 25/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a collection of attributes.
    /// </summary>
    public class CuiAttributesCollection
        : DynamicObject, IEnumerable<KeyValuePair<string, CuiAttribute>>
    {
        /// <summary>
        /// Contains the attributes of the collection indexed by name.
        /// </summary>
        protected Dictionary<string, CuiAttribute> List = new Dictionary<string, CuiAttribute>();

        /// <summary>
        /// Represents the owner of the attributes.
        /// </summary>
        /// <remarks>
        /// Use <see cref="AssignOwner(CuiElement)"/> to assign the owner if needed.
        /// </remarks>
        /// <value>
        /// The owner element.
        /// </value>
        public CuiElement Owner
        {
            get
            {
                return owner;
            }
            protected set
            {
                owner = value;

                foreach (var attribute in List)
                {
                    attribute.Value.Owner = owner;
                }
            }
        }
        // Backer field
        private CuiElement owner;

        /// <summary>
        /// Returns the total number of the attributes in the collection.
        /// </summary>
        /// <value>
        /// The attributes count in the collection.
        /// </value>
        public int Count => List.Count;

        /// <summary>
        /// Creates a new collection from a list of attributes.
        /// </summary>
        /// <param name="attributes">A list of attributes</param>
        public CuiAttributesCollection(IEnumerable<CuiAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                List.Add(attribute.Name, attribute);
            }
        }

        /// <summary>
        /// Retrieves an attribute with the specified name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The attribute if it exists.</returns>
        public CuiAttribute this[string name]
        {
            get
            {
                return List[name];
            }
        }

        /// <summary>
        /// Retrieves the value of an attribute.
        /// </summary>
        /// <param name="binder">The binder representing the attribute</param>
        /// <param name="result">The result value</param>
        /// <returns>Whether the attribute exists or not.</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool exists = List.ContainsKey(binder.Name);
            result = exists ? this[binder.Name].Value : null;
            return exists;
        }

        /// <summary>
        /// Assigns an owner to the attributes if one doesn't already exist.
        /// </summary>
        /// <param name="owner">The owner element</param>
        /// <exception cref="InvalidOperationException" />
        public void AssignOwner(CuiElement owner)
        {
            Owner = (Owner == null)
                ? owner
                : throw new InvalidOperationException("The attribute already has an owner.");
        }

        public IEnumerator<KeyValuePair<string, CuiAttribute>> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

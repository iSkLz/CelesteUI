using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * Element definition implementation
 * 
 * Author: SkLz
 * Last edit: 20/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a CUI element and exposes functionality to instantiate it.
    /// </summary>
    /// <remarks>
    /// This class is written generically to cover all the needs of most typical elements. If custom behavior is needed, inherit the class and override the methods.
    /// </remarks>
    public class CUIElementDefinition
    {
        /// <summary>
        /// Contains the types for element contrsuctors
        /// </summary>
        private static readonly Type[] ConstructorParentTypes = new Type[]
        {
            typeof(CUIElement), typeof(List<CUIAttribute>)
        };

        private static readonly Type[] ConstructorDocumentTypes = new Type[]
        {
            typeof(CUIDocument), typeof(List<CUIAttribute>)
        };

        public readonly string Name;
        protected readonly List<CUIAttributeDefinition> Attributes = new List<CUIAttributeDefinition>();

        public IEnumerable<CUIAttributeDefinition> GetAttributes()
        {
            // Fun fact: (List<CUIAttribute>)Def.GetAttributes() allows you to bypass this middleware function.
            return Attributes;
        }

        public CUIElementDefinition(string name)
        {
            if ((Name = name) == null)
                throw new ArgumentNullException(nameof(name), "Null element name.");
        }

        public virtual void DefineAttribute(CUIAttributeDefinition definition)
        {
            Attributes.Add(definition);
        }

        public virtual void DefineAttributes(params CUIAttributeDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                DefineAttribute(definition);
            }
        }

        /// <summary>
        /// Parses the markup of an element into an instance.
        /// </summary>
        /// <remarks>
        /// Custom element types passed to <typeparamref name="T"/> must provide a constructor that corresponds to one of the two <see cref="CUIElement"/> constructors with a <see cref="List{T}"/> of attributes.
        /// The appropriate constructor (depending on the context) is located and invoked.
        /// </remarks>
        /// <typeparam name="T">Type of the element to construct.</typeparam>
        /// <param name="info">Information of the element.</param>
        /// <returns>The resultant CUI element</returns>
        public virtual T Parse<T>(CUIElementInfo info) where T : CUIElement
        {
            // Parse attributes
            var attributes = new List<CUIAttribute>();
            foreach (var attr in Attributes)
            {
                attributes.Add(attr.Parse(info));
            }

            // Construct the element (invoke constructor via reflection)
            var args = new object[]
            {
                info.Parent ?? (object)info.Document,
                attributes
            };
            var ctor = info.Parent != null
                ? typeof(T).GetConstructor(ConstructorParentTypes)
                : typeof(T).GetConstructor(ConstructorDocumentTypes);
            T element = (T)ctor.Invoke(args);

            // Initialize
            foreach (var attr in attributes)
            {
                attr.AssignOwner(element);
            }
            element.Initialize();

            return element;
        }
    }
}

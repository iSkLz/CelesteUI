using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * Attribute implementation
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a CUI element attribute.
    /// </summary>
    public partial class CuiAttribute
    {
        /// <summary>
        /// Represents the value assigned to the attribute.
        /// </summary>
        /// <value>
        /// The evaluated value of the attribute.
        /// </value>
        public virtual object Value
        {
            get
            {
                return GetValue();
            }
        }

        /// <summary>
        /// Represents a <see cref="Func{T, TResult}"/> that retrieves the value of the attribute.
        /// </summary>
        /// <remarks>
        /// This is automatically assigned to by the constructor.
        /// Override <see cref="Value"/> to change the value retrieving mechanism.
        /// </remarks>
        protected Func<object> GetValue;

        /// <summary>
        /// Represents the stage in which the attribute should be evaluated.
        /// </summary>
        /// <remarks>
        /// Check this to know when the attribute should be evaluated.
        /// </remarks>
        /// <value>
        /// The stage of the attribute
        /// </value>
        public CuiStage Stage { get; protected set; } = CuiStage.Activation;

        /// <summary>
        /// Represents the owner of the attribute.
        /// </summary>
        /// <remarks>
        /// Use <see cref="AssignOwner(CuiElement)"/> to assign the owner if needed.
        /// </remarks>
        /// <value>
        /// The owner element.
        /// </value>
        public CuiElement Owner { get; protected internal set; }

        /// <summary>
        /// Represents the name of the attribute.
        /// </summary>
        /// <value>
        /// The attribute's name.
        /// </value>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Constructs an attribute with a constant value.
        /// </summary>
        /// <param name="value">The value of the attribute</param>
        public CuiAttribute(string name, object value)
        {
            GetValue = () => value;
            Stage = CuiStage.Creation;
            Name = name;
        }

        /// <summary>
        /// Constructs an attribute with a macro value.
        /// </summary>
        /// <param name="macros">The list of the macros from the innermost to the outermost</param>
        /// <param name="expression">The expression inside the innermost macro</param>
        public CuiAttribute(string name, IEnumerable<ICuiMacro> macros, string expression)
        {
            // The latest stage is the attribute's stage
            Stage = CuiStage.Creation;
            foreach (var macro in macros)
            {
                if (macro.Stage > Stage) Stage = macro.Stage;
            }

            // Walk through the macros
            GetValue = () =>
            {
                object value = expression;
                return macros.WalkThrough<object, string>(expression, Owner);
            };
            Name = name;
        }

        /// <summary>
        /// Assigns an owner to the attribute if one doesn't already exist.
        /// </summary>
        /// <param name="owner">The owner element</param>
        /// <exception cref="InvalidOperationException" />
        public void AssignOwner(CuiElement owner)
        {
            Owner = (Owner == null)
                ? owner
                : throw new InvalidOperationException("The attribute already has an owner.");
        }
    }
}

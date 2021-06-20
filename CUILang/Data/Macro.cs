/*
 * CelesteUI language library
 * Macro interface implementation
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a macro.
    /// </summary>
    public interface ICUIMacro
    {
        /// <summary>
        /// Represents the macro's definition name.
        /// </summary>
        /// <value>
        /// The macro's name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Represents the macro's evaluation stage.
        /// </summary>
        /// <value>
        /// The stage the macro should be evaluated in.
        /// </value>
        CUIStage Stage { get; }

        /// <summary>
        /// Evaluates the macro given it's input value and the parent element.
        /// </summary>
        /// <typeparam name="R">The macro's output type</typeparam>
        /// <typeparam name="T">The macro's input type</typeparam>
        /// <param name="value">The macro's input value</param>
        /// <param name="element">The element whose attribute contains the macro</param>
        /// <returns>The output value of the macro</returns>
        R Evaluate<R, T>(T value, CUIElement element);
    }
}

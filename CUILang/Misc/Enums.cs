/*
 * CelesteUI language library
 * Macro interface implementation
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 * 
 * Recent changes:
 * + Added CUIStage enum
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Defines element lifecycle stages.
    /// </summary>
    public enum CUIStage
    {
        /// <summary>
        /// When the element's data is parsed and it's initially created
        /// </summary>
        Creation,

        /// <summary>
        /// When the element is instanciated (a UI object is created)
        /// </summary>
        Activation,

        /// <summary>
        /// When the UI object updates (every frame of instance lifecycles)
        /// </summary>
        Update
    }
}

/*
 * CelesteUI language library
 * Macro interface implementation
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Defines element lifecycle stages.
    /// </summary>
    public enum CuiStage
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

    /// <summary>
    /// Identifies the type of a parsed value.
    /// </summary>
    public enum CuiExpressionType
    {
        /// <summary>
        /// 16-bit integer
        /// </summary>
        Short,

        /// <summary>
        /// 32-bit integer
        /// </summary>
        Integer,

        /// <summary>
        /// 64-bit integer
        /// </summary>
        Long,

        /// <summary>
        /// Single precision floating point number
        /// </summary>
        Float,

        /// <summary>
        /// Double precision floating point number
        /// </summary>
        Double,

        /// <summary>
        /// String (text)
        /// </summary>
        String
    }

    /// <summary>
    /// Contains constants for attribute parsing modes.
    /// </summary>
    public enum CuiAttributeParseType
    {
        /// <summary>
        /// Parse as a constant expressional value.
        /// </summary>
        Value,

        /// <summary>
        /// Parse as a macro.
        /// </summary>
        Macro,

        /// <summary>
        /// Parse as a macro, and if no macro could be found parse as a value.
        /// </summary>
        MacroOrValue
    }
}

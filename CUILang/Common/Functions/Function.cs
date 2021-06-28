/*
 * CelesteUI language library
 * Common native library implementation
 * Func macros
 * Native functions interface
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI.Common
{
    // Functions are defined as interfaces rather than delegates so that they can store and accumulate data.

    /// <summary>
    /// Represents a native implementation of a macro function.
    /// </summary>
    public interface ICuiFunction
    {
        /// <summary>
        /// Calls the function and returns it's return value.
        /// </summary>
        /// <returns>The return value of the function</returns>
        object Call();
    }
}

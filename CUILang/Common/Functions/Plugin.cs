using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * Common native library implementation
 * Func macros
 * Func plugin
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents a library that exports functions.
    /// </summary>
    public interface ICuiFunctionsLibrary
    {
        /// <summary>
        /// Exports the library's function onto the specified plugin instance.
        /// </summary>
        /// <param name="plugin">The plugin instance to export onto.</param>
        void Export(CuiFunctionsPlugin plugin);
    }

    public sealed class CuiFunctionsPlugin : CuiPlugin
    {
        /// <summary>
        /// Contains the functions defined every parse.
        /// </summary>
        public Dictionary<string, ICuiFunction> Functions { get; private set; }

        public override void PreParse()
        {
            // Reset the functions list every parse
            Functions = new Dictionary<string, ICuiFunction>();
        }

        public override void ImportLibrary(CuiLibrary library)
        {
            // Allows libraries to export functions as well
            if (library is ICuiFunctionsLibrary functionsLibrary)
                functionsLibrary.Export(this);
        }
    }
}

using System.Collections.Generic;

/*
 * CelesteUI language library
 * Utilities and extensions.
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Contains extension method used throughout the library's code.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Walks through a list of macros passing the output value of each one to the next as an input value.
        /// </summary>
        /// <typeparam name="R">The final output type</typeparam>
        /// <typeparam name="T">The starting input type</typeparam>
        /// <param name="macros">The list of the macros</param>
        /// <param name="startingExp">The starting value</param>
        /// <param name="element">The element whose attribute contains the macros</param>
        /// <returns>The output value of the last macro</returns>
        public static R WalkThrough<R, T>(this List<ICUIMacro> macros, T startingExp, CUIElement element)
        {
            object value = startingExp;
            foreach (var macro in macros)
            {
                value = macro.Evaluate<object, object>(value, element);
            }

            return (R)value;
        }

        /// <summary>
        /// Iterates an <see cref="IEnumerable{T}"/> appending each item to a <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="T">The generic type of the list</typeparam>
        /// <param name="list">The list to which the items are appended</param>
        /// <param name="source">The source of the items to append</param>
        public static void AddFrom<T>(this List<T> list, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Checks whether a specific export type is allowed within an allowance enum value.
        /// </summary>
        /// <param name="allowed">The value specifying which exports are allowed</param>
        /// <param name="type">The export type to check</param>
        /// <returns>Whether the type is allowed or not</returns>
        public static bool IsAllowed(this CUILibrary.ExportFlags allowed, CUILibrary.ExportFlags type)
        {
            return (allowed & type) > CUILibrary.ExportFlags.None;
        }
    }
}

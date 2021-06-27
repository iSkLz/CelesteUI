using System;
using System.Collections.Generic;
using System.Reflection;

/*
 * CelesteUI language library
 * Utilities and extensions.
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Contains extension and utility methods used throughout the library's code.
    /// </summary>
    public static class CuiUtils
    {
        public static readonly BindingFlags AllInstanceFields =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Walks through a list of macros passing the output value of each one to the next as an input value.
        /// </summary>
        /// <typeparam name="R">The final output type</typeparam>
        /// <typeparam name="T">The starting input type</typeparam>
        /// <param name="macros">The list of the macros</param>
        /// <param name="startingExp">The starting value</param>
        /// <param name="element">The element whose attribute contains the macros</param>
        /// <returns>The output value of the last macro</returns>
        public static R WalkThrough<R, T>(this IEnumerable<ICuiMacro> macros, T startingExp, CuiElement element)
        {
            object value = startingExp;
            foreach (var macro in macros)
            {
                value = macro.Evaluate(value, element);
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
        /// Represents an enumerable list of base types starting from this types's base type ending with the root base type.
        /// </summary>
        /// <param name="type">The type to get the base types of</param>
        /// <returns>An iterator that works its way through the ladder of base types.</returns>
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            var curType = type;

            while (curType != null)
            {
                yield return curType;
                curType = curType.BaseType;
            }
        }

        /// <summary>
        /// Checks whether the specified type is a base type.
        /// </summary>
        /// <param name="type">The type to perform the check on</param>
        /// <param name="baseType">The base type to look for</param>
        /// <returns>Whether the specified type is a base type or not.</returns>
        public static bool InheritsFrom(this Type type, Type baseType)
        {
            foreach (var curType in GetBaseTypes(type))
            {
                if (curType.Equals(baseType)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether the specified type is a base type.
        /// </summary>
        /// <param name="type">The type to perform the check on</param>
        /// <typeparam name="T">The base type to look for</param>
        /// <returns>Whether the specified type is a base type or not.</returns>
        public static bool InheritsFrom<T>(this Type type)
        {
            return type.InheritsFrom(typeof(T));
        }

        /// <summary>
        /// Attempts to parse the expression into an integer then a floating point number then a string.
        /// </summary>
        /// <param name="expression">The expression to parse</param>
        /// <param name="type">The type of the result</param>
        /// <returns>The resultant value.</returns>
        public static object ParseExpression(this string expression, out CuiExpressionType type)
        {
            // TryParse fails if the number cannot be represented in the type
            // So we try from parsing as integers then floats from smallest to biggest
            #region Integers
            if (short.TryParse(expression, out short int16))
            {
                type = CuiExpressionType.Short;
                return int16;
            }

            if (int.TryParse(expression, out int int32))
            {
                type = CuiExpressionType.Integer;
                return int32;
            }

            if (long.TryParse(expression, out long int64))
            {
                type = CuiExpressionType.Long;
                return int64;
            }
            #endregion

            #region Floating-point numbers
            if (float.TryParse(expression, out float single))
            {
                type = CuiExpressionType.Float;
                return single;
            }

            if (double.TryParse(expression, out double num))
            {
                type = CuiExpressionType.Double;
                return num;
            }

            if (decimal.TryParse(expression, out decimal dec))
            {
                type = CuiExpressionType.Double;
                return dec;
            }
            #endregion

            // If all fails, pass as a string
            type = CuiExpressionType.String;
            return expression;
        }
    }
}

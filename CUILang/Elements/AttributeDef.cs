using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
 * CelesteUI language library
 * Attribute definition implementation
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a CUI element attribute and exposes functionality to parse it.
    /// </summary>
    /// <remarks>
    /// Override this if custom parsing behaviour is needed.
    /// </remarks>
    public class CUIAttributeDefinition
    {
        /// <summary>
        /// Contains constants for attribute parsing modes.
        /// </summary>
        public enum ParseType
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

        /// <summary>
        /// Represents a regular expression used to find macros.
        /// </summary>
        /// <remarks>
        /// Contains two capture groups: The first contains the macro's name and the second contains its expression.
        /// </remarks>
        public static Regex MacroRegex = new Regex(@"(\w+)\((\w+)\)");

        /// <summary>
        /// Defines the parsing behavior of the attribute.
        /// </summary>
        public readonly ParseType Type;

        /// <summary>
        /// Defines the possible names of the attribute.
        /// </summary>
        public readonly string[] Names;

        /// <summary>
        /// Defines the default value of the attribute.
        /// </summary>
        public readonly object Default;

        /// <summary>
        /// Constructs a new attribute definition given its parsing mode, default value and names.
        /// </summary>
        /// <param name="type">The parsing behavior of the attribute.</param>
        /// <param name="defaultValue">The possible names of the attribute.</param>
        /// <param name="names">The default value of the attribute. </param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public CUIAttributeDefinition(ParseType type, object defaultValue, params string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("No attribute names specified.", nameof(names));

            foreach (var name in names)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(names), "One of the attribute names was null.");
            }

            if (type > ParseType.MacroOrValue)
                throw new ArgumentOutOfRangeException(nameof(type), "Invalid attribute type.");

            if (defaultValue == null)
                throw new ArgumentNullException(nameof(defaultValue), "Invalid default value: null.");
            
            Type = type;
            Names = names;
        }

        /// <summary>
        /// Parses the attribute on an XML element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public virtual CUIAttribute Parse(CUIElementInfo element)
        {
            var xml = element.XML;
            foreach (string name in Names)
            {
                if (xml.HasAttribute(name))
                {
                    return Parse(element, xml.GetAttribute(name));
                }
            }

            return new CUIAttribute(Default);
        }

        public virtual CUIAttribute Parse(CUIElementInfo element, string expression)
        {
            switch (Type)
            {
                case ParseType.Macro:
                case ParseType.MacroOrValue:
                    if (ParseMacro(element, expression, out List<ICUIMacro> macros, out string exp))
                    {
                        return new CUIAttribute(macros, exp);
                    }
                    break;
            }

            return ParseValue(element, expression);
        }

        protected virtual CUIAttribute ParseValue(CUIElementInfo element, string expression)
        {
            // TODO: Floats, Ints
            return new CUIAttribute(expression);
        }

        protected virtual bool HasMacro(CUIElementInfo element, string expression, out string macro)
        {
            Match match;
            if ((match = MacroRegex.Match(expression)).Success)
            {
                macro = match.Captures[1].Value;
                return true;
            }

            macro = null;
            return false;
        }

        protected virtual bool ParseMacro(CUIElementInfo element, string expression, out List<ICUIMacro> macros, out string startingExpression)
        {
            var stack = new Stack<ICUIMacro>();

            string exp = expression;
            Match match;
            int count = 0;
            while ((match = MacroRegex.Match(exp)).Success)
            {
                count++;
                var macroName = match.Captures[1].Value;

                if (!element.Document.Macros.ContainsKey(macroName))
                    throw new ParsingException($"No macro {macroName} defined in the document.", element.Document.File);

                stack.Push(element.Document.Macros[macroName]);
                exp = match.Captures[2].Value;
            }
            startingExpression = exp;

            macros = new List<ICUIMacro>();
            foreach (var item in stack)
            {
                macros.Add(item);
            }

            return count != 0;
        }
    }
}

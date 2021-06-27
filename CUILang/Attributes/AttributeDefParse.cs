using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
 * CelesteUI language library
 * Attribute definition implementation
 * Parsing members
 * 
 * Author: SkLz
 * Last edit: 25/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    public partial class CuiAttributeDefinition {
        /// <summary>
        /// Parses the attribute on an XML element.
        /// </summary>
        /// <param name="element">The owner element information</param>
        /// <returns>The resultant parsed attribute.</returns>
        public virtual CuiAttribute Parse(CuiParsingContext element)
        {
            var xml = element.XML;
            foreach (string name in Names)
            {
                if (xml.HasAttribute(name))
                {
                    return Parse(element, xml.GetAttribute(name));
                }
            }

            return new CuiAttribute(Name, Default);
        }

        /// <summary>
        /// Parses the attribute from an expression.
        /// </summary>
        /// <param name="element">The owner element information</param>
        /// <returns>The resultant parsed attribute.</returns>
        public virtual CuiAttribute Parse(CuiParsingContext element, string expression)
        {
            switch (Type)
            {
                case CuiAttributeParseType.Macro:
                case CuiAttributeParseType.MacroOrValue:
                    if (ParseMacro(element, expression, out var macros, out string exp))
                    {
                        return new CuiAttribute(Name, macros, exp);
                    }
                    break;
            }

            return Type == CuiAttributeParseType.Macro
                ? throw new ParsingException("Couldn't parse the attribute as a macro.")
                : ParseValue(element, expression);
        }

        /// <summary>
        /// Parses the attribute as a value (not a macro).
        /// </summary>
        /// <param name="element">The owner element information</param>
        /// <param name="expression">The expression of the attribute</param>
        /// <returns>The resultant parsed attribute.</returns>
        protected virtual CuiAttribute ParseValue(CuiParsingContext element, string expression)
        {
            return new CuiAttribute(Name, expression.ParseExpression(out _));
        }

        /// <summary>
        /// Checks whether the attribute's expression is in a macro form.
        /// </summary>
        /// <param name="element">The owner element information</param>
        /// <param name="expression">The expression of the attribute</param>
        /// <param name="macro">The macro's name if it was found</param>
        /// <returns>Whether a macro was found or not..</returns>
        protected virtual bool HasMacro(CuiParsingContext element, string expression, out string macro)
        {
            // Check with the regex if a macro exists
            Match match;
            if ((match = MacroRegex.Match(expression)).Success)
            {
                macro = match.Captures[1].Value;
                return true;
            }

            macro = null;
            return false;
        }

        /// <summary>
        /// Attempts to parse the attribute as a macro.
        /// </summary>
        /// <param name="element">The owner element information</param>
        /// <param name="expression">The expression of the attribute</param>
        /// <param name="macros">A list of the macros that were found from the innermost to the outermost</param>
        /// <param name="startingExpression">The expression inside the innermost macro</param>
        /// <returns>Whether one or more macros was/were parsed or not.</returns>
        protected virtual bool ParseMacro(CuiParsingContext element, string expression, out List<ICuiMacro> macros, out string startingExpression)
        {
            // Sort by reverse
            var stack = new Stack<ICuiMacro>();

            // Loop variables
            string exp = expression;
            Match match;
            int count = 0;
            // Keep looping while a macro still exists (check that via a regex)
            while ((match = MacroRegex.Match(exp)).Success)
            {
                count++;
                var macroName = match.Captures[1].Value;

                // Check if the macro with the parsed name is defined in the document
                if (!element.Document.Macros.ContainsKey(macroName))
                    throw new ParsingException($"No macro {macroName} defined in the document.");

                // Store the macro
                stack.Push(element.Document.Macros[macroName]);

                // Next iteration check the expression inside this macro
                exp = match.Captures[2].Value;
            }

            // The last expression is the expression of the innermost macro
            startingExpression = exp;

            // TODO: Can you just pass the stack to the list constructor?
            // Convert the stack to a list
            macros = new List<ICuiMacro>();
            while (stack.Count > 0)
            {
                macros.Add(stack.Pop());
            }

            // If no macros were parsed count will be 0
            return count != 0;
        }
    }
}

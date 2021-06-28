using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
 * CelesteUI language library
 * Attribute definition implementation
 * Public members
 * 
 * Author: SkLz
 * Last edit: 26/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a CUI element attribute and exposes functionality to parse it.
    /// </summary>
    /// <remarks>
    /// Derive from this class if custom parsing behaviour is needed.
    /// </remarks>
    public partial class CuiAttributeDefinition
    {
        /// <summary>
        /// Represents a regular expression used to find macros.
        /// </summary>
        /// <remarks>
        /// Contains two capture groups: The first contains the macro's name and the second contains its expression.
        /// </remarks>
        public static Regex MacroRegex = new Regex(@"(\w+)\(([\s\S]+)\)");

        /// <summary>
        /// Defines the parsing behavior of the attribute.
        /// </summary>
        public readonly CuiAttributeParseType Type;

        /// <summary>
        /// Defines the possible XML names of the attribute.
        /// </summary>
        public readonly string[] Names;

        /// <summary>
        /// Represents the attribute's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Defines the default value of the attribute.
        /// </summary>
        public readonly object Default;

        /// <summary>
        /// Constructs a new attribute definition given its parsing mode, default value and names.
        /// </summary>
        /// <param name="name">The runtime name assigned to the attribute</param>
        /// <param name="type">The parsing behavior of the attribute</param>
        /// <param name="defaultValue">The default value of the attribute</param>
        /// <param name="names">The possible XML names of the attribute</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public CuiAttributeDefinition(string name, CuiAttributeParseType type, object defaultValue, params string[] names)
            : this(name, type, names)
        {
            if ((Default = defaultValue) == null)
                throw new ArgumentNullException(nameof(defaultValue), "Invalid default value: null.");
        }

        /// <summary>
        /// Constructs a new attribute definition given its parsing mode and names.
        /// </summary>
        /// <param name="name">The runtime name assigned to the attribute</param>
        /// <param name="type">The parsing behavior of the attribute</param>
        /// <param name="defaultValue">The default value of the attribute</param>
        /// <param name="names">The possible XML names of the attribute</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException" />
        public CuiAttributeDefinition(string name, CuiAttributeParseType type, params string[] names)
        {
            if (names.Length == 0)
                throw new ArgumentException("No attribute names specified.", nameof(names));

            foreach (var curName in names)
            {
                if (curName == null)
                    throw new ArgumentNullException(nameof(names), "One of the attribute names was null.");
            }

            if (type > CuiAttributeParseType.MacroOrValue)
                throw new ArgumentOutOfRangeException(nameof(type), "Invalid attribute type.");

            Name = name ?? throw new ArgumentNullException(nameof(name), "Invalid name: null.");
            Type = type;
            Names = names;
        }
    }
}

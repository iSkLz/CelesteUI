using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * Native import library implementation
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 * 
 * Recent changes:
 * + Added automatic exports
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a native C# importable library.
    /// This is an abstract class.
    /// </summary>
    public abstract class CUILibrary
    {
        /// <summary>
        /// Defines what the library should export.
        /// </summary>
        public enum ExportFlags
        {
            None = 0,
            Functions = 1 << 0,
            Macros = 1 << 1,
            DataSources = 1 << 2,
            Elements = 1 << 3
        }

        /// <summary>
        /// Exports the library onto a given document.
        /// </summary>
        /// <param name="document">The library's target document.</param>
        /// <param name="allowed">The export types allowed.</param>
        public abstract void Export(CUIDocument document, ExportFlags allowed);

        /// <summary>
        /// Automatically exports <see cref="Macros"/> and <see cref="Elements"/> onto a given document.
        /// </summary>
        /// <remarks>
        /// This method will strictly follow <paramref name="allowed"/>.
        /// </remarks>
        /// <param name="document">The library's target document.</param>
        /// <param name="allowed">The export types allowed.</param>
        protected void AutoExport(CUIDocument document, ExportFlags allowed)
        {
            if (allowed.IsAllowed(ExportFlags.Macros))
            {
                foreach (var macro in Macros)
                {
                    document.Macros.Add(macro.Name, macro);
                }
            }

            if (allowed.IsAllowed(ExportFlags.Elements))
            {
                foreach (var element in Elements)
                {
                    document.Elements.Add(element.Name, element);
                }
            }
        }

        /// <summary>
        /// Represents a list of macros that can be exported using <see cref="AutoExport(CUIDocument, ExportFlags)"/>.
        /// </summary>
        protected List<ICUIMacro> Macros = new List<ICUIMacro>();

        /// <summary>
        /// Represents a list of elements that can be exported using <see cref="AutoExport(CUIDocument, ExportFlags)"/>.
        /// </summary>
        protected List<CUIElementDefinition> Elements = new List<CUIElementDefinition>();

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddMacros(IEnumerable<ICUIMacro> list)
            => Macros.AddFrom(list);

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddMacros(params ICUIMacro[] list)
            => AddMacros((IEnumerable<ICUIMacro>)list);
        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddElements(IEnumerable<CUIElementDefinition> list)
            => Elements.AddFrom(list);

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddElements(params CUIElementDefinition[] list)
            => AddElements((IEnumerable<CUIElementDefinition>)list);
    }
}

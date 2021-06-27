using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * Native import library implementation
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Describes a native importable library.
    /// This is an abstract class.
    /// </summary>
    public abstract class CuiLibrary
    {
        /// <summary>
        /// Exports the library onto a given document.
        /// </summary>
        /// <param name="document">The library's target document.</param>
        public virtual void Export(CuiDocument document)
            => AutoExport(document);

        /// <summary>
        /// Automatically exports <see cref="Macros"/>, <see cref="Plugins"/> and <see cref="Elements"/> onto a given document.
        /// </summary>
        /// <param name="document">The library's target document.</param>
        protected void AutoExport(CuiDocument document)
        {
            foreach (var macro in Macros)
            {
                document.Macros.Add(macro.Name, macro);
            }

            foreach (var element in Elements)
            {
                document.Elements.Add(element.Name, element);
            }

            foreach (var plugin in Plugins)
            {
                document.AddPlugin(plugin);
            }
        }

        #region Auto-Export Macros
        /// <summary>
        /// Represents a list of macros that can be exported using <see cref="AutoExport(CuiDocument)"/>.
        /// </summary>
        protected List<ICuiMacro> Macros = new List<ICuiMacro>();

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddMacros(IEnumerable<ICuiMacro> list)
            => Macros.AddFrom(list);

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddMacros(params ICuiMacro[] list)
            => AddMacros((IEnumerable<ICuiMacro>)list);
        #endregion

        #region Auto-Export Elements
        /// <summary>
        /// Represents a list of elements that can be exported using <see cref="AutoExport(CuiDocument)"/>.
        /// </summary>
        protected List<CuiElementDefinition> Elements = new List<CuiElementDefinition>();

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddElements(IEnumerable<CuiElementDefinition> list)
            => Elements.AddFrom(list);

        /// <summary>
        /// Adds a list of macros to <see cref="Macros"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddElements(params CuiElementDefinition[] list)
            => AddElements((IEnumerable<CuiElementDefinition>)list);
        #endregion

        #region Auto-Export Plugins
        /// <summary>
        /// Represents a list of plugins that can be exported using <see cref="AutoExport(CuiDocument)"/>.
        /// </summary>
        protected List<Type> Plugins = new List<Type>();

        // TODO: Validate plugin types
        /// <summary>
        /// Adds a list of plugins to <see cref="Plugins"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddPlugins(IEnumerable<Type> list)
            => Plugins.AddFrom(list);

        /// <summary>
        /// Adds a list of plugins to <see cref="Plugins"/>.
        /// </summary>
        /// <param name="list">The list to add.</param>
        protected virtual void AddPlugins(params Type[] list)
            => AddPlugins((IEnumerable<Type>)list);

        /// <summary>
        /// Adds the specified plugin to <see cref="Plugins"/>.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin to add</typeparam>
        protected virtual void AddPlugin<TPlugin>()
            => AddPlugins(typeof(TPlugin));

        /// <summary>
        /// Adds two plugins to <see cref="Plugins"/>.
        /// </summary>
        /// <typeparam name="TPlugin1">The first plugin to add</typeparam>
        /// <typeparam name="TPlugin2">The second plugin to add</typeparam>
        protected virtual void AddPlugins<TPlugin1, TPlugin2>()
            => AddPlugins(typeof(TPlugin1), typeof(TPlugin2));

        /// <summary>
        /// Adds three plugins to <see cref="Plugins"/>.
        /// </summary>
        /// <typeparam name="TPlugin1">The first plugin to add</typeparam>
        /// <typeparam name="TPlugin2">The second plugin to add</typeparam>
        /// <typeparam name="TPlugin3">The third plugin to add</typeparam>
        protected virtual void AddPlugins<TPlugin1, TPlugin2, TPlugin3>()
            => AddPlugins(typeof(TPlugin1), typeof(TPlugin2), typeof(TPlugin3));

        // TODO: Seven quadrillion more generic methods :arbH:
        // (just kidding, only 7 more with type arguments up to TPlugin10)
        #endregion
    }
}

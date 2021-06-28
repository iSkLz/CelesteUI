using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * CelesteUI document implementation
 * External APIs
 * 
 * Author: SkLz
 * Last edit: 28/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    public sealed partial class CuiDocument
    {
        #region Libraries
        /// <summary>
        /// Contains the libraries available for import to all documents.
        /// </summary>
        /// <remarks>
        /// Add libraries to this list to allow importing them via the import elements.
        /// </remarks>
        public static readonly List<CuiLibrary> GlobalLibraries = new List<CuiLibrary>();

        /// <summary>
        /// Contains the libraries imported in all documents.
        /// </summary>
        /// <remarks>
        /// Add libraries to this list to have them imported in every document.
        /// </remarks>
        public static readonly List<CuiLibrary> AutoGlobalLibraries = new List<CuiLibrary>();

        /// <summary>
        /// Contains the libraries available for import to this document.
        /// </summary>
        /// <remarks>
        /// Add libraries to this list to allow importing them via the import elements.
        /// </remarks>
        public List<CuiLibrary> Libraries = new List<CuiLibrary>();

        /// <summary>
        /// Contains the libraries imported in each parse.
        /// </summary>
        /// <remarks>
        /// Add libraries to this list to have them imported every parse.
        /// </remarks>
        public List<CuiLibrary> AutoLibraries = new List<CuiLibrary>();

        /// <summary>
        /// Imports a library directly.
        /// </summary>
        /// <param name="library">The library to import.</param>
        public void ImportLibrary(CuiLibrary library)
        {
            library.Export(this);
        }
        #endregion

        /// <summary>
        /// Checks that a <see cref="Type"/> inherits from <see cref="CuiPlugin"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <exception cref="ArgumentException"/>
        private static void ValidatePluginType(Type type)
        {
            if (!type.InheritsFrom<CuiPlugin>())
                throw new ArgumentException("The provided plugin type doesn't inherit from CUIPlugin.");
        }

        #region Global Plugins
        /// <summary>
        /// Contains a list of plugin types that are added to every document.
        /// </summary>
        private static readonly List<Type> GlobalPlugins = new List<Type>();

        /// <summary>
        /// Register a plugin to be attached to every document.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin's type</typeparam>
        public static void AddGlobalPlugin<TPlugin>() where TPlugin : CuiPlugin, new()
        {
            GlobalPlugins.Add(typeof(TPlugin));
        }

        /// <summary>
        /// Register a plugin to be attached to every document.
        /// </summary>
        /// <param name="pluginType">The plugin's type</param>
        public static void AddGlobalPlugin(Type pluginType)
        {
            ValidatePluginType(pluginType);
            GlobalPlugins.Add(pluginType);
        }

        /// <summary>
        /// Unregister a plugin from being attached to every document.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin's type</typeparam>
        public static void RemoveGlobalPlugin<TPlugin>() where TPlugin : CuiPlugin
        {
            GlobalPlugins.Remove(typeof(TPlugin));
        }

        /// <summary>
        /// Unregister a plugin from being attached to every document.
        /// </summary>
        /// <param name="pluginType">The plugin's type</param>
        public static void RemoveGlobalPlugin(Type pluginType)
        {
            ValidatePluginType(pluginType);
            GlobalPlugins.Remove(pluginType);
        }
        #endregion

        #region Plugins
        /// <summary>
        /// Contains the plugins attached to this document.
        /// </summary>
        private readonly Dictionary<Type, CuiPlugin> Plugins = new Dictionary<Type, CuiPlugin>();

        /// <summary>
        /// Attaches a new instance of the specified plugin to this document.
        /// </summary>
        /// <typeparam name="TPlugin"></typeparam>
        public void AddPlugin<TPlugin>() where TPlugin : CuiPlugin, new()
        {
            Plugins.Add(typeof(TPlugin), new TPlugin());
        }

        /// <summary>
        /// Attaches a new instance of the specified plugin to this document.
        /// </summary>
        /// <param name="pluginType">The plugin's type</param>
        public void AddPlugin(Type pluginType)
        {
            ValidatePluginType(pluginType);
            Plugins.Add(pluginType, (CuiPlugin)Activator.CreateInstance(pluginType));
        }

        /// <summary>
        /// Retrieves the plugin of the specified type.
        /// </summary>
        /// <typeparam name="TPlugin">The plugin's type</typeparam>
        public TPlugin GetPlugin<TPlugin>() where TPlugin : CuiPlugin
        {
            return (TPlugin)Plugins[typeof(TPlugin)];
        }

        /// <summary>
        /// Retrieves the plugin of the specified type.
        /// </summary>
        /// <param name="pluginType">The plugin's type</param>
        public CuiPlugin GetPlugin(Type pluginType)
        {
            // Validate only in debug builds
#if DEBUG
            ValidatePluginType(pluginType);
#endif
            return Plugins[pluginType];
        }
        #endregion

        /// <summary>
        /// Handles global libraries/plugins
        /// </summary>
        private CuiDocument()
        {
            foreach (var lib in GlobalLibraries)
            {
                Libraries.Add(lib);
            }

            foreach (var lib in AutoGlobalLibraries)
            {
                AutoLibraries.Add(lib);
            }

            foreach (var plugin in GlobalPlugins)
            {
                AddPlugin(plugin);
            }
        }
    }
}

/*
 * CelesteUI language library
 * Language plugin implementation
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents an addition to the language that can applied to a document. This class is abstract.
    /// </summary>
    /// <remarks>
    /// An implementation of this class can be passed to <see cref="CuiDocument.AddPlugin{TPlugin}"/> which will create an instance of it.
    /// A plugin instance can hold data specific to the document and to each parse, and that data can be accessed by custom elements via <see cref="CuiDocument.GetPlugin{TPlugin}"/>.
    /// </remarks>
    public abstract class CuiPlugin
    {
        // Dispose on finalizer
        ~CuiPlugin()
        {
            Dispose();
        }

        /// <summary>
        /// Initializes the plugin.
        /// </summary>
        /// <param name="document">The target document</param>
        public virtual void Initialize(CuiDocument document) { }

        /// <summary>
        /// Performs actions before parsing.
        /// </summary>
        public virtual void PreParse() { }

        /// <summary>
        /// Performs actions after parsing.
        /// </summary>
        public virtual void PostParse() { }

        /// <summary>
        /// Performs actions after a library is imported.
        /// </summary>
        /// <param name="library">The library being imported</param>
        public virtual void ImportLibrary(CuiLibrary library) { }

        /// <summary>
        /// Frees the resources that are no longer needed after the plugin instance is reclaimed by garbage collection.
        /// </summary>
        public virtual void Dispose() { }
    }
}

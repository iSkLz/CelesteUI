/*
 * CelesteUI language library
 * Common native library implementation
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI.Common
{
    /// <summary>
    /// Implements the basic features of CUI.
    /// </summary>
    public partial class CuiCommonLibrary : CuiLibrary
    {
        /// <summary>
        /// Represents the library's single instance.
        /// </summary>
        public readonly static CuiCommonLibrary Instance = new CuiCommonLibrary();

        private CuiCommonLibrary()
        {
            AddElements(Root);

            // Functions
            AddMacros(MFunc.Instance, MSelfFunc.Instance, MDynFunc.Instance, MStaticFunc.Instance);
            AddPlugin<CuiFunctionsPlugin>();
        }
    }
}

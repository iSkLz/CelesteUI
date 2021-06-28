/*
 * CelesteUI language library
 * Common native library implementation
 * Attribute definitions
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI.Common
{
    public partial class CuiCommonLibrary : CuiLibrary
    {
        /// <summary>
        /// Defines the ID attribute.
        /// </summary>
        public static readonly CuiAttributeDefinition ID = new CuiAttributeDefinition("ID", CuiAttributeParseType.Value, "id");
    }
}

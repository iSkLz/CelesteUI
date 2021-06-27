/*
 * CelesteUI language library
 * CelesteUI document implementation
 * Common library
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    public sealed partial class CuiDocument
    {
        static CuiDocument()
        {
            // Add the common library to all documents
            GlobalLibraries.Add(CuiCommonLibrary.Instance);
        }
    }
}

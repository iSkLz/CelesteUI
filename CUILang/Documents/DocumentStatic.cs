using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * CelesteUI document implementation
 * Static members
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    public partial class CUIDocument
    {
        public static List<CUILibrary> GlobalLibraries = new List<CUILibrary>();
        public static List<CUIDocument> GlobalFiles = new List<CUIDocument>();

        public static event Action<CUIDocument> PreParse;
    }
}

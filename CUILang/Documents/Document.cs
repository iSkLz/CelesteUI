using System;
using System.Collections.Generic;

/*
 * CelesteUI language library
 * CelesteUI document implementation
 * Instance members
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 */

namespace Celeste.Mod.CelesteUI
{
    public partial class CUIDocument
    {
        public Dictionary<string, ICUIMacro> Macros = new Dictionary<string, ICUIMacro>();
        public Dictionary<string, Action> Functions = new Dictionary<string, Action>();
        public Dictionary<string, CUIElementDefinition> Elements = new Dictionary<string, CUIElementDefinition>();

        public string File;
    }
}

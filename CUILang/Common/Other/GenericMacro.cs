using System;

/*
 * CelesteUI language library
 * Common native library implementation
 * Generic macro
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI.Common
{
    public sealed class MGeneric : ICuiMacro
    {
        public delegate object Evaluater(object argument, CuiElement element);

        public string Name { get; }
        public CuiStage Stage { get; }

        private readonly Evaluater EvaluateFunc;

        public MGeneric(string name, CuiStage stage, Evaluater func)
        {
            Name = name;
            Stage = stage;
            EvaluateFunc = func;
        }

        public object Evaluate(object argument, CuiElement element)
        {
            return EvaluateFunc(argument, element);
        }
    }
}

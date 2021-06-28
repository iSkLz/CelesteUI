using System;

/*
 * CelesteUI language library
 * Common native library implementation
 * Func macros
 * Macro implementations
 * 
 * Author: SkLz
 * Last edit: 27/06/2021
 */

namespace Celeste.Mod.CelesteUI.Common
{
    /// <summary>
    /// Implements the basic functionality of the Func macros.
    /// </summary>
    public abstract class MBaseFunc : ICuiMacro
    {
        public abstract string Name { get; }
        public abstract CuiStage Stage { get; }

        public virtual object Evaluate(object arg, CuiElement element)
        {
            // Retrieve & call the function then return the result
            var id = (string)arg;
            return element.Document.GetPlugin<CuiFunctionsPlugin>().Functions[id].Call();
        }
    }

    /// <summary>
    /// Implements the macro Func.
    /// </summary>
    public class MFunc : MBaseFunc
    {
        public static MFunc Instance = new MFunc();

        public override string Name => "Func";
        public override CuiStage Stage => CuiStage.Activation;
    }

    /// <summary>
    /// Implements the macro StaticFunc.
    /// </summary>
    public class MStaticFunc : MBaseFunc
    {
        public static MStaticFunc Instance = new MStaticFunc();

        public override string Name => "StaticFunc";
        public override CuiStage Stage => CuiStage.Creation;
    }

    /// <summary>
    /// Implements the macro DynFunc.
    /// </summary>
    public class MDynFunc : MBaseFunc
    {
        public static MDynFunc Instance = new MDynFunc();

        public override string Name => "DynFunc";
        public override CuiStage Stage => CuiStage.Update;
    }

    /// <summary>
    /// Implements the macro SelfFunc.
    /// </summary>
    public class MSelfFunc : MBaseFunc
    {
        public static MSelfFunc Instance = new MSelfFunc();

        public override string Name => "SelfFunc";
        public override CuiStage Stage => CuiStage.Creation;

        public override object Evaluate(object arg, CuiElement element)
        {
            // Retrieve & store the function object
            var id = (string)arg;
            var func = element.Document.GetPlugin<CuiFunctionsPlugin>().Functions[id];

            // Return a delegate that calls it
            Func<object> del = () => func.Call();
            return del;
        }
    }
}
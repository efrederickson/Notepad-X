/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/16/2011
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using NotepadX.Macros.AST;

namespace NotepadX.Macros
{
    /// <summary>
    /// Description of Environment.
    /// </summary>
    public class Environment
    {
        Hashtable Objects = new Hashtable(); // key = value
        
        public Environment()
        {
            // set basic stuff
            Set("true", true);
            Set("True", true);
            Set("false", false);
            Set("False", false);
            Set("null", null);
            Set("nil", null);
            Set("Nothing", null);
            Set("T", true);
            
            //TODO set basic functions
        }
        
        public object Run(object[] Parsed)
        {
            object ret = null;
            foreach (object o in Parsed)
            {
                if (o is WhileLoop)
                    ret = ((WhileLoop)o).Execute(this, null);
                if (o is ForLoop)
                    ret = ((ForLoop)o).Execute(this, null);
                if (o is IfStatement)
                    ret = ((IfStatement)o).Execute(this, null);
                if (o is SetVariable)
                    ret = ((SetVariable)o).Execute(this, null);
                if (o is DefineVariable)
                    ret = ((DefineVariable)o).Execute(this, null);
                if (o is DefinedFunction)
                    ret = ((DefinedFunction)o).Execute(this, null);
                if (o is FunctionCall)
                    ret = ((FunctionCall)o).Execute(this, null);
                if (o is MathFormula)
                    ret = AdvancedMathProcesser.Calculate((o as MathFormula).Formula);
                if (o is string)
                    ret = (string)o;
                
                ret = o;
            }
            return ret;
        }
        
        public bool IsRunnable(object[] parsed)
        {
            //TODO
            return true;
        }
        
        public object GetObject(string name)
        {
            return Objects.ContainsKey(name) == true ? Objects[name] : null;
        }
        
        public void Set(string name, object val)
        {
            Objects[name] = val;
        }
        
        public void AddFunction(string name, Function f)
        {
            Objects[name] = f;
        }
    }
}

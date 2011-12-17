/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/16/2011
 * Time: 10:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NotepadX.Macros.AST
{
    /// <summary>
    /// base expression class
    /// </summary>
    public abstract class Expression
    {
        public abstract object Execute(Environment e, object[] args);
        
        public List<object> Pieces = new List<object>();
    }
    
    /// <summary>
    /// Macro While Loop
    /// </summary>
    public class WhileLoop : Expression
    {
        public object WhilePiece;
        
        public WhileLoop(object whilePiece, List<object> pieces)
        {
            this.WhilePiece = whilePiece;
            this.Pieces = pieces;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            object o = null;
            while (IExtendFramework.Converter.ObjectToBoolean(e.Run(new object[] {WhilePiece})) == true)
            {
                o = e.Run(Pieces.ToArray());
            }
            return o;
        }
    }
        
    /// <summary>
    /// Basic for loop
    /// </summary>
    public class ForLoop : Expression
    {
        string var;
        int max, incremental;
        public ForLoop(string var, int max, int incremental, List<object> pieces)
        {
            this.var = var;
            this.max = max;
            this.Pieces = pieces;
            this.incremental = incremental;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            int _v = 0;
            object o = null;
            e.Set(var, _v);
            for (; int.Parse(e.GetObject(var).ToString()) < max; _v += incremental)
            {
                e.Set(var, _v);
                o = e.Run(Pieces.ToArray());
            }
            return o;
        }
    }
    
    /// <summary>
    /// basic if statement
    /// TODO: elseif
    /// </summary>
    public class IfStatement : Expression
    {
        public List<object> TruePieces;
        public List<object> ElsePieces;
        public object Decider;
        
        public IfStatement(List<object> truePieces, List<object> falsePieces, object decider)
        {
            this.TruePieces = truePieces;
            this.ElsePieces = falsePieces;
            this.Decider = decider;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            object o = null;
            if (IExtendFramework.Converter.ObjectToBoolean(e.Run(new object[] { Decider} )))
            {
                o = e.Run(TruePieces.ToArray());
            }
            else
            {
                o = e.Run(ElsePieces.ToArray());
            }
            return o;
        }
    }
    
    public class SetVariable : Expression
    {
        string VarName;
        public SetVariable(string varname, object val)
        {
            this.VarName = varname;
            this.Pieces.Add(val);
        }
        
        public override object Execute(Environment e, object[] args)
        {
            e.Set(VarName, e.Run(Pieces.ToArray()));
            return null;
        }
    }
    
    public class DefineVariable : Expression
    {
        string varname;
        public DefineVariable(string varname, object val)
        {
            this.varname = varname;
            this.Pieces.Add(val);
        }
        
        public override object Execute(Environment e, object[] args)
        {
            e.Set(varname, e.Run(Pieces.ToArray()));
            return null;
        }
    }
    
    public class DefinedFunction : Expression
    {
        string funcName;
        object[] argnames;
        public DefinedFunction(string funcName, List<object> body, object[] argnames)
        {
            this.Pieces = body;
            this.funcName = funcName;
            this.argnames = argnames;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            // create the function
            MessageBox.Show("adding function " + funcName);
            e.Set(funcName, new Function(Invoke));
            return null;
        }
        
        public object Invoke(Environment e, object[] args)
        {
            foreach (object o in argnames)
            {
                // FIXME
                e.Set(o.ToString(), e.GetObject(o.ToString()));
            }
            return e.Run(Pieces.ToArray());
            // TODO: remove args from env
        }
    }
    
    public class FunctionCall : Expression
    {
        // function name
        string fn;
        string[] args;
        public FunctionCall(string fn, string[] args)
        {
            this.fn = fn;
            this.args = args;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            if (e.GetObject(fn) == null)
                throw new Exception("Function '" + fn + "' not defined!");
            return (e.GetObject(fn) as Function)(e, this.args);
        }
    }
    
    public class DoStatement : Expression
    {
        public DoStatement(List<object> pieces)
        {
            this.Pieces = pieces;
        }
        
        public override object Execute(Environment e, object[] args)
        {
            return e.Run(this.Pieces.ToArray());
        }
    }
}

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
        public static Environment LastEnvironment;
        
        public Hashtable Objects = new Hashtable(); // key = value
        
        public Environment()
        {
            // set basic stuff
            Set("true", true);
            Set("false", false);
            Set("null", null);
            Set("nil", null);
            Set("nothing", null);
            Set("t", true);
            
            // set basic functions
            Set("open", new Function(Functions.Open));
            Set("save", new Function(Functions.Save));
            Set("saveas", new Function(Functions.SaveAs));
            Set("undo", new Function(Functions.Undo));
            Set("redo", new Function(Functions.Redo));
            Set("print", new Function(Functions.Print));
            Set("printpreview", new Function(Functions.PrintPreview));
            Set("printsetup", new Function(Functions.PrintSetup));
            Set("name", new Function(Functions.Name));
            Set("closecurrentwindow", new Function(Functions.CloseCurrentWindow));
            Set("exit", new Function(Functions.Exit));
            Set("end", new Function(Functions.Exit));
            Set("messagebox", new Function(Functions.MessageBox));
            Set("msgbox", new Function(Functions.MessageBox));
            Set("calculate", new Function(Functions.Calculate));
            Set("calc", new Function(Functions.Calculate));
            
            LastEnvironment = this;
        }
        
        public object Run(object[] Parsed)
        {
            object ret = null;
            foreach (object o in Parsed)
            {
                ret = o; // default value
                if (o is Expression) // if it has a meaning to its poor existance.
                    ret = ((Expression)o).Execute(this, null);
                else if (o is string) // waht is dis madnes?
                    ret = (string)o;
                // check if >, <, <=, <=, =<, =>, =, ==
                if (o.ToString().Contains(">"))
                {
                    object[] Os = o.ToString().Split('>');
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a > b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                if (o.ToString().Contains("<"))
                {
                    object[] Os = o.ToString().Split('<');
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a < b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                if (o.ToString().Contains(">="))
                {
                    object[] Os = o.ToString().Split(new string[] {">="} , StringSplitOptions.None);
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a >= b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                if (o.ToString().Contains("<="))
                {
                    object[] Os = o.ToString().Split(new string[] {"<="} , StringSplitOptions.None);
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a <= b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                if (o.ToString().Contains("=>"))
                {
                    object[] Os = o.ToString().Split(new string[] {"=>"} , StringSplitOptions.None);
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a >= b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                if (o.ToString().Contains("=<"))
                {
                    object[] Os = o.ToString().Split(new string[] {"=<"} , StringSplitOptions.None);
                    try {
                        int a = int.Parse(Os[0].ToString());
                        int b = int.Parse(Os[1].ToString());
                        ret = a <= b;
                    } catch (Exception ex) { throw new Exception("Error comparing numbers: " + ex.ToString());
                    }
                }
                
                if (o.ToString().Contains("="))
                {
                    object[] Os = o.ToString().Split(new string[] {"="} , StringSplitOptions.None);
                    try {
                        object a = Os[0];
                        object b = Os[1];
                        ret = a == b;
                    } catch (Exception ex) { throw new Exception("Error comparing objects: " + ex.ToString());
                    }
                }
                
            }
            return ret;
        }
        
        public object GetObject(string name)
        {
            return Objects.ContainsKey(name.ToLower().Trim()) == true ? Objects[name] : null;
        }
        
        public void Set(string name, object val)
        {
            Objects[name.ToLower().Trim()] = val;
        }
        
    }
}

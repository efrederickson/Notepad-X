/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/16/2011
 * Time: 11:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NotepadX.Macros
{
    /// <summary>
    /// Description of Parser.
    /// </summary>
    public class Parser
    {
        public List<object> Parsed = new List<object>();
        
        public Parser(string input)
        {
            Parsed = Parse(new StringReader(input));
        }
        
        // used so you can see what line the error is on
        int lineNumber = 0;
        
        private List<object> Parse(StringReader input)
        {
            List<object> r = new List<object>();
            StringReader sr = input;
            
            while (sr.Peek() != -1)
            {
                lineNumber++;
                string line = sr.ReadLine();
                r.Add(ParseLine(line, sr));
            }
            return r;
        }
        
        private object ParseLine(string line, StringReader sr)
        {
            string lline = line.ToLower();
            // check for if-then/for/while/ etc.
            if (lline.StartsWith("for "))
                return ParseForLoop(line, sr);
            else if (lline.StartsWith("if "))
                return ParseIfStatement(line, sr);
            else if (lline.StartsWith("while "))
                return ParseWhileLoop(line, sr);
            else if (lline.StartsWith("set "))
                return ParseSetStatement(line, sr);
            else if (lline.StartsWith("var "))
                return ParseVarStatement(line, sr);
            else if (lline.StartsWith("function "))
                return ParseFunction(line, sr);
            else if (lline.StartsWith("call "))
                return ParseFunctionCall(line, sr);
            else if (lline.StartsWith("do"))
                return ParseDoStatement(line, sr);
             else if (line.ToLower().Trim() == "true")
                return true;
            else if (line.ToLower().Trim() == "false")
                return false;
            // parse math, int, double, string
            try {
                int.Parse(line);
                // passed - its an int
                return int.Parse(line);
            } catch (Exception) { }

            try {
                double.Parse(line);
                // passed - its a double
                return double.Parse(line);
            } catch (Exception) { }
            
            return line;
        }
        
        object ParseDoStatement(string line, StringReader sr)
        {
            line=sr.ReadLine();
            List<object> PiEcEs = new List<object>();
            while (line.ToLower().Trim() != "end")
            {
                PiEcEs.Add(ParseLine(line, sr));
                line = sr.ReadLine();
                lineNumber ++;
            }
            return new AST.DoStatement(PiEcEs);
        }
        
        object ParseFunctionCall(string line, StringReader sr)
        {
            string fn;
            string[] args;
            Console.WriteLine("Macro: parsing function call");
            //AST.FunctionCall fCall = new NotepadX.Macros.AST.FunctionCall(name, args)
            
            line = line.Substring("call ".Length);
            fn = line.Substring(0, line.IndexOf(" ") == -1 ? line.Length : line.IndexOf(" "));
            line = line.Substring(fn.Length); // cut out function name
            args = line.Split(',');
            List<string> tmpArgs = new List<string>();
            foreach (string s in args)
            {
                tmpArgs.Add(s.Trim());
            }
            
            return new AST.FunctionCall(fn, tmpArgs.ToArray());
        }
        
        AST.DefinedFunction ParseFunction(string line, StringReader sr)
        {
            Console.WriteLine("Macro: Parsing function");
            //AST.DefinedFunction func = new NotepadX.Macros.AST.DefinedFunction(name, body, args)
            string fName;
            List<object> body = new List<object>();
            List<string> args = new List<string>();
            
            // parse name
            string sFunc = line.Substring("function".Length);
            fName = sFunc.Substring(0, sFunc.IndexOf("("));
            
            // parse args
            sFunc = sFunc.Substring(sFunc.IndexOf("(") + 1);
            sFunc = sFunc.Substring(0, sFunc.IndexOf(")"));
            string[] argnames = sFunc.Split(',');
            foreach (string arg in argnames) 
            {
                args.Add(arg);
            }
            
            // parse the body
            string line2 = sr.ReadLine();
            while (line2.ToLower().Trim() != "end")
            {
                body.Add(ParseLine(line2, sr));
                line2 = sr.ReadLine();
                lineNumber++;
            }
            return new AST.DefinedFunction(fName, body, args.ToArray());
        }
        
        AST.DefineVariable ParseVarStatement(string line, StringReader sr)
        {
            Console.WriteLine("Macro: Parsing var statement");
            
            // format: var <var> = <val>
            //AST.DefineVariable def = new NotepadX.Macros.AST.DefineVariable(varname, val)
            
            string varname;
            object val;
            
            line = line.Substring("var ".Length);
            if (line.Contains("="))
            {
                string[] line2 = line.Split('=');
                varname = line2[0];
                val = ParseLine(line2[1], sr);
            }
            else
            {
                varname = line;
                val = null;
            }
            return new AST.DefineVariable(varname, val);
        }
        
        AST.SetVariable ParseSetStatement(string line, StringReader sr)
        {
            Console.WriteLine("Macro: Parsing set statement");
            
            // format: set <var> = <val>
            //AST.SetVariable sVal = new NotepadX.Macros.AST.SetVariable(varname, val)
            
            string varname;
            object val;
            
            line = line.Substring("set ".Length);
            
            string[] line2 = line.Split('=');
            
            varname = line2[0];
            val = ParseLine(line2[1], sr);
            return new AST.SetVariable(varname, val);
        }
        
        AST.WhileLoop ParseWhileLoop(string whileLine, StringReader sr)
        {
            Console.WriteLine("Macro: parsing while loop");
            
            //AST.WhileLoop wLoop = new NotepadX.Macros.AST.WhileLoop(whilePiece, pieces)
            object whilePiece;
            List<object> pieces = new List<object>();
            string nWLine = whileLine.Substring("while".Length);
            nWLine = nWLine.Substring(0, nWLine.Length - 3);
            whilePiece = ParseLine(nWLine, sr);
            
            string line = sr.ReadLine();
            while (line.ToLower().Trim() != "end")
            {
                pieces.Add(ParseLine(line, sr));
                line = sr.ReadLine();
                lineNumber++;
            }
            
            return new AST.WhileLoop(whilePiece, pieces);
        }
        
        AST.ForLoop ParseForLoop(string forLine, StringReader input)
        {
            Console.WriteLine("Macro: Parsing for loop");
            
            string var;
            int max, incrementer;
            //AST.ForLoop floop = new NotepadX.Macros.AST.ForLoop(var, max, increment, body)
            string nFLine = forLine.Substring("for".Length);
            nFLine = nFLine.Substring(0, nFLine.Length - 3);
            string[] fLoopDec = nFLine.Split(',');
            var = fLoopDec[0].ToString();
            try {
                max = int.Parse(fLoopDec[1]);
                incrementer = int.Parse(fLoopDec[2]);
            } catch (Exception ex) {
                throw new Exception("Cannot parse for loop on line " + lineNumber + "!\nLine: " + forLine + "\nMaximum: " + fLoopDec[1] +  "\nIncrementer : " + fLoopDec[2] + "\n" + ex.ToString());
            }
            
            // parse body
            List<object> body = new List<object>();
            string line = input.ReadLine();
            while (line.ToLower().Trim() != "end")
            {
                body.Add(ParseLine(line, input));
                line = input.ReadLine();
                lineNumber++;
            }
            return new NotepadX.Macros.AST.ForLoop(var, max, incrementer, body);
        }
        
        AST.IfStatement ParseIfStatement(string ifLine, StringReader input)
        {
            Console.WriteLine("Macro: parsing if statement");
            
            //AST.IfStatement ifS = new NotepadX.Macros.AST.IfStatement(truePieces, falsePieces, decider)
            
            List<object> truePieces = new List<object>(), falsePieces = new List<object>();
            object decider;
            
            string shortLine = ifLine.Substring("if".Length).Substring(0, ifLine.LastIndexOf(" then"));
            decider = ParseLine(shortLine, input);
            
            string line = input.ReadLine();
            while (line.ToLower().Trim() != "else")
            {
                truePieces.Add(ParseLine(line, input));
                line = input.ReadLine();
                lineNumber++;
            }
            while (line.ToLower().Trim() != "end")
            {
                falsePieces.Add(ParseLine(line, input));
                line = input.ReadLine();
                lineNumber++;
            }
            
            return new NotepadX.Macros.AST.IfStatement(truePieces, falsePieces, decider);
        }
    }
}

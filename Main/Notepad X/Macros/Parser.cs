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
                string lline = line.ToLower();
                
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
                
            return line;
        }
        
        object ParseSetStatement(string line, StringReader sr)
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
        
        object ParseWhileLoop(string whileLine, StringReader sr)
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
        
        private AST.ForLoop ParseForLoop(string forLine, StringReader input)
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
        
        private AST.IfStatement ParseIfStatement(string ifLine, StringReader input)
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

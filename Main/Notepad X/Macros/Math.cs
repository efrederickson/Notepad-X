/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/16/2011
 * Time: 11:19 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Numerics;
using System.Windows.Forms;

namespace NotepadX.Macros
{
    /// <summary>
    /// An advanced math processer to process formulas
    /// </summary>
    public class AdvancedMathProcesser
    {
        //RegEx for Real and Complex numbers
        static string Real = "(?<!([E][+-][0-9]+))([-]?\\d+\\.?\\d*([E][+-][0-9]+)?(?!([i0-9.E]))|[-]?\\d*\\.?\\d+([E][+-][0-9]+)?)(?![i0-9.E])";
        
        static string Img = "(?<!([E][+-][0-9]+))([-]?\\d+\\.?\\d*([E][+-][0-9]+)?(?![0-9.E])(?:i)|([-]?\\d*\\.?\\d+)?([E][+-][0-9]+)?\\s*(?:i)(?![0-9.E]))";
        
        public static string Calculate(string Formula)
        {
            return EvaluateBrackets(Formula.Replace(" ", "")) + "\n";
        }
        
        static string EvaluateBrackets(string input)
        {
            
            input = "(" + input + ")";
            
            string pattern = "(?>\\( (?<LEVEL>)(?<CURRENT>)| (?=\\))(?<LAST-CURRENT>)(?(?<=\\(\\k<LAST>)(?<-LEVEL> \\)))|\\[ (?<LEVEL>)(?<CURRENT>)|(?=\\])(?<LAST-CURRENT>)(?(?<=\\[\\k<LAST>)(?<-LEVEL> \\] ))|[^()\\[\\]]*)+(?(LEVEL)(?!))";
            
            MatchCollection MAtchBracets = Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace);
            
            CaptureCollection captures = MAtchBracets[0].Groups["LAST"].Captures;
            
            List<string> ListOfPara = new List<string>();
            
            foreach (Capture c in captures) {
                ListOfPara.Add(c.Value);
            }
            
            string result = input;
            
            List<string> CalcList = new List<string>();
            for (int i = 0; i <= ListOfPara.Count - 1; i++) {
                if (i == 0) {
                    CalcList.Add(Evaluate(ListOfPara[i]));
                    result = CalcList[i];
                } else {
                    for (int j = i; j <= ListOfPara.Count - 1; j++) {
                        ListOfPara[j] = ListOfPara[j].Replace(ListOfPara[i - 1], CalcList[i - 1]).Replace(" ", "");
                    }
                    result = Evaluate(ListOfPara[i]).Replace(" ", "");
                    CalcList.Add(result);
                }
            }
            result = Evaluate(ListOfPara[ListOfPara.Count - 1]);
            return result;
        }
        
        /// <summary>
        /// Source of code: Programming Visual Basic .NET Author: Franceisco Balena. The code is modified from the original, as this one can deal with complex numbers
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        static string Evaluate(string Input)
        {
            
            string NumType = "((?<Both>((" + Real + "\\s*([+])*\\s*" + Img + ")|(" + Img + "\\s*([+])*\\s*" + Real + ")))|(?<Real>(" + Real + "))|(?<Imag>(" + Img + ")))";
            string NumTypeSingle = "((?<Real>(" + Real + "))|(?<Imag>(" + Img + ")))";
            
            
            const string Func1 = "(exp|log|log10|abs|sqr|sqrt|sin|cos|tan|asin|acos|atan)";
            // List of 2-operand functions.
            const string Func2 = "(atan2)";
            // List of N-operand functions.
            const string FuncN = "(min|max)";
            
            // List of predefined constants.
            const string Constants = "(e|pi)";
            
            Regex rePower = new Regex("\\(?(?<No1>" + NumType + ")\\)?" + "\\s*(?<Operator>(\\^))\\s*\\(?(?<No2>" + NumType + ")\\)?");
            Regex rePower2 = new Regex("\\(?(?<No1>" + NumType + ")\\)?" + "\\s*(?<Operator>(\\^))\\s*(?<No2>" + NumTypeSingle + ")");
            Regex rePowerSingle = new Regex("(?<No1>" + NumTypeSingle + ")" + "\\s*(?<Operator>(\\^))\\s*(?<No2>" + NumTypeSingle + ")");
            Regex rePowerSingle2 = new Regex("(?<No1>" + NumTypeSingle + ")" + "\\s*(?<Operator>(\\^))\\s*\\(?(?<No2>" + NumType + ")\\)?");
            
            Regex reMulDiv = new Regex("\\(?\\s*(?<No1>" + NumType + ")\\)?" + "\\s*(?<Operator>([*/]))\\s*\\(?(?<No2>" + NumType + ")\\s*\\)?\\)?");
            Regex reMulDiv2 = new Regex("\\(?\\s*(?<No1>" + NumType + ")\\)?" + "\\s*(?<Operator>([*/]))\\s*(?<No2>" + NumTypeSingle + ")");
            Regex reMulDivSingle = new Regex("\\(?\\s*(?<No1>" + NumTypeSingle + ")" + "\\s*(?<Operator>([*/]))\\s*(?<No2>" + NumTypeSingle + ")\\s*\\)?\\)?");
            Regex reMulDivSingle2 = new Regex("\\(?\\s*(?<No1>" + NumTypeSingle + ")" + "\\s*(?<Operator>([*/]))\\s*\\(?(?<No2>" + NumType + ")\\s*\\)?");
            
            Regex reAddSub = new Regex("\\(?(?<No1>" + NumType + ")\\)?" + "\\s*(?<Operator>([-+]))\\s*\\(?(?<No2>" + NumType + ")\\)?");
            
            Regex reFunc1 = new Regex("\\s*(?<Function>" + Func1 + ")\\(?\\s*" + "(?<No1>" + NumType + ")" + "\\s*\\)?", RegexOptions.IgnoreCase);
            Regex reFunc2 = new Regex("\\s*(?<Function>" + Func2 + ")\\(\\s*" + "(?<No1>" + NumType + ")" + "\\s*,\\s*" + "(?<No2>" + NumType + ")" + "\\s*\\)", RegexOptions.IgnoreCase);
            Regex reFuncN = new Regex("\\s*(?<Function>" + FuncN + ")\\((?<Numbers>(\\s*" + NumType + "\\s*,)+\\s*" + NumType + ")\\s*\\)", RegexOptions.IgnoreCase);
            Regex reSign1 = new Regex("([-+/*^])\\s*\\+");
            
            // This Regex object converts a double minus into a plus.
            Regex reSign2 = new Regex("\\-\\s*\\-");
            
            // This Regex object drops parenthesis around a number.
            // (must not be preceded by an alphanum char (it might be a function name)
            //Dim rePar As New Regex("(?<![A-Za-z0-9])\(\s*" & NumType & "\s*\)")
            
            // A Regex object that tells that the entire expression is a number
            Regex reNum = new Regex("^\\s*" + NumType + "\\s*$");
            
            // The Regex object deals with constants. (Requires case insensitivity.)
            Regex reConst = new Regex("\\s*(?<Const>" + Constants + ")\\s*");
            
            // This resolves predefined constants. (Can be kept out of the loop.)
            Input = reConst.Replace(Input, DoConstants);
            
            while (!(reNum.IsMatch(Input))) {
                string saveExpr = Input;
                
                while (rePowerSingle.IsMatch(Input)) {
                    Input = rePowerSingle.Replace(Input, DoPower).ToString().Replace(" ", "");
                }
                
                while (rePowerSingle2.IsMatch(Input)) {
                    Input = rePowerSingle2.Replace(Input, DoPower).ToString().Replace(" ", "");
                }
                
                while (rePower2.IsMatch(Input)) {
                    Input = rePower2.Replace(Input, DoPower).ToString().Replace(" ", "");
                }
                
                while (reMulDivSingle.IsMatch(Input)) {
                    Input = reMulDivSingle.Replace(Input, DoMulDiv).ToString().Replace(" ", "");
                }
                
                while (reMulDivSingle2.IsMatch(Input)) {
                    Input = reMulDivSingle2.Replace(Input, DoMulDiv).ToString().Replace(" ", "");
                }
                
                while (reMulDiv.IsMatch(Input)) {
                    Input = reMulDiv.Replace(Input, DoMulDiv).ToString().Replace(" ", "");
                }
                
                while (reMulDiv2.IsMatch(Input)) {
                    Input = reMulDiv2.Replace(Input, DoMulDiv).ToString().Replace(" ", "");
                }
                
                // Perform functions with variable numbers of arguments.
                while (reFuncN.IsMatch(Input)) {
                    Input = reFuncN.Replace(Input, DoFuncN);
                }
                
                // Perform functions with 2 arguments.
                while (reFunc2.IsMatch(Input)) {
                    Input = reFunc2.Replace(Input, DoFunc2);
                }
                
                while (reFunc1.IsMatch(Input)) {
                    Input = reFunc1.Replace(Input, DoFunc1);
                }
                
                // Discard + symbols (unary pluses)that follow another operator.
                Input = reSign1.Replace(Input, "$1");
                // Simplify 2 consecutive minus signs into a plus sign.
                Input = reSign2.Replace(Input, "+");
                
                string saveAddSub = Input;
                while (reAddSub.IsMatch(Input) & !(Regex.Match(Input, NumType).Groups[0].Value == Input)) {
                    Input = reAddSub.Replace(Input, DoAddSub).ToString().Replace(" ", "");
                    if (saveAddSub == Input) {
                        break; // TODO: might not be correct. Was : Exit While
                    } else {
                        saveAddSub = Input;
                    }
                }
                
                //  expr = rePar.Replace(expr, "$1")
                
                if (Input == saveExpr) {
                    return Input;
                    // if it didn't work, exit with syntax error exception.
                    throw new NotImplementedException();
                    //SyntaxErrorException
                }
                
            }
            
            return Input;
        }
        
        static string DoAddSub(Match m)
        {
            dynamic n1 = default(Complex);
            Complex n2 = new Complex();
            n1 = GenerateComplexNumberFromString(m.Groups["No1"].Value);
            n2 = GenerateComplexNumberFromString(m.Groups["No2"].Value);
            
            switch (m.Groups["Operator"].Value) {
                case "+":
                    Complex f = new Complex();
                    f = n1 + n2;
                    return string.Format(new ComplexFormatter(), "{0:I0}", f);
                case "-":
                    Complex f2 = new Complex();
                    f2 = n1 - n2;
                    return string.Format(new ComplexFormatter(), "{0:I0}", f2);
                default:
                    return Convert.ToString(1);
            }
        }
        
        static string DoMulDiv(Match m)
        {
            dynamic n1 = default(Complex);
            Complex n2 = new Complex();
            n1 = GenerateComplexNumberFromString(m.Groups["No1"].Value);
            n2 = GenerateComplexNumberFromString(m.Groups["No2"].Value);
            switch (m.Groups["Operator"].Value) {
                case "/":
                    
                    return string.Format(new ComplexFormatter(), "{0:I0}", (n1 / n2));
                case "*":
                    return string.Format(new ComplexFormatter(), "{0:I0}", (n1 * n2));
                default:
                    return Convert.ToString(1);
            }
        }
        
        static string DoPower(Match m)
        {
            dynamic n1 = default(Complex);
            dynamic n2 = default(Complex);
            Complex n3 = new Complex();
            n1 = GenerateComplexNumberFromString(m.Groups["No1"].Value);
            n2 = GenerateComplexNumberFromString(m.Groups["No2"].Value);
            n3 = Complex.Pow(n1, n2);
            string s = string.Format(new ComplexFormatter(), "{0:I0}", n3);
            return "(" + s + ")";
        }
        
        static string DoFunc1(Match m)
        {
            // function argument is 2nd group.
            Complex n1 = new Complex();
            n1 = GenerateComplexNumberFromString(m.Groups["No1"].Value);
            // function name is 1st group.
            switch (m.Groups["Function"].Value.ToUpper()) {
                case "EXP":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Exp(n1));
                case "LOG":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Log(n1));
                case "LOG10":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Log10(n1));
                case "ABS":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Abs(n1));
                case "SQR":
                case "SQRT":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Sqrt(n1));
                case "SIN":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Sin(n1));
                case "COS":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Cos(n1));
                case "TAN":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Tan(n1));
                case "ASIN":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Asin(n1));
                case "ACOS":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Acos(n1));
                case "ATAN":
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Atan(n1));
                default:
                    return Convert.ToString(1);
            }
        }
        
        static string DoFuncN(Match m)
        {
            // function arguments are from group 2 onward.
            string[] args = {
                
            };
            ArrayList args2 = new ArrayList();
            //int i = 2;
            // Load all the arguments into the array.
            
            foreach (Capture h in m.Groups["Numbers"].Captures) {
                args = h.ToString().Split(Convert.ToChar(","));
            }
            
            foreach (string Str in args) {
                args2.Add(GenerateComplexNumberFromString(Str.Replace(',', ' ')));
            }
            
            //I cant sort complex numbers, you have a go ;)
            // function name is 1st group.
            switch (m.Groups["Function"].Value.ToUpper()) {
                case "MIN":
                    args2.Sort();
                    return string.Format(new ComplexFormatter(), "{0:I0}", args[0]);
                case "MAX":
                    args2.Sort();
                    //args(args.Count - 1).ToString
                    return string.Format(new ComplexFormatter(), "{0:I0}", args[args.Length - 1]);
                default:
                    return Convert.ToString(1);
            }
        }
        
        /// <summary>
        /// This is a bit unnecessary since Atan2 doesn't exist for 2 variables but it shows how to deal wit this kind of function
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        static string DoFunc2(Match m)
        {
            // function arguments are 2nd and 3rd group.
            dynamic n1 = default(Complex);
            Complex n2 = new Complex();
            n1 = GenerateComplexNumberFromString(m.Groups["No1"].Value);
            n2 = GenerateComplexNumberFromString(m.Groups["No2"].Value);
            // function name is 1st group.
            switch (m.Groups["Function"].Value.ToUpper()) {
                case "ATAN2":
                    //.ToString '.Atan2(n1, n2).ToString
                    return string.Format(new ComplexFormatter(), "{0:I0}", Complex.Atan(n1 / n2));
                default:
                    return Convert.ToString(1);
            }
        }
        
        // These functions evaluate the actual math operations.
        // In all cases the Match object on entry has groups that identify
        // the two operands and the operator.
        static string DoConstants(Match m)
        {
            switch (m.Groups["Const"].Value.ToUpper()) {
                case "PI":
                    return Math.PI.ToString().Replace(",", ".");
                case "E":
                    return Math.E.ToString().Replace(",", ".");
                default:
                    return Convert.ToString(1);
            }
        }
        
        static Complex GenerateComplexNumberFromString(string input)
        {
            input = input.Replace(" ", "");
            
            string Number = "((?<Real>(" + Real + "))|(?<Imag>(" + Img + ")))";
            double Re = 0;
            double Im = 0;
            Re = 0;
            Im = 0;
            
            
            foreach (Match Match in Regex.Matches(input, Number)) {
                if (!(Match.Groups["Real"].Value == string.Empty)) {
                    Re = double.Parse(Match.Groups["Real"].Value, CultureInfo.InvariantCulture);
                }
                
                if (!(Match.Groups["Imag"].Value == string.Empty)) {
                    if (Match.Groups["Imag"].Value.ToString().Replace(" ", "") == "-i") {
                        Im = double.Parse("-1", CultureInfo.InvariantCulture);
                    } else if (Match.Groups["Imag"].Value.ToString().Replace(" ", "") == "i") {
                        Im = double.Parse("1", CultureInfo.InvariantCulture);
                    } else {
                        Im = double.Parse(Match.Groups["Imag"].Value.ToString().Replace("i", ""), CultureInfo.InvariantCulture);
                    }
                }
            }
            
            Complex result = new Complex(Re, Im);
            return result;
        }
        
        private class ComplexFormatter : IFormatProvider, ICustomFormatter
        {
            
            public object GetFormat(Type formatType)
            {
                if (object.ReferenceEquals(formatType, typeof(ICustomFormatter))) {
                    return this;
                } else {
                    return null;
                }
            }
            
            public string Format(string fmt, object arg, IFormatProvider provider)
            {
                if (arg is Complex) {
                    Complex c1 = (Complex)arg;
                    // Check if the format string has a precision specifier.
                    int precision = 0;
                    string fmtString = string.Empty;
                    if (fmt.Length > 1) {
                        try {
                            precision = Int32.Parse(fmt.Substring(1));
                        } catch (FormatException) {
                            precision = 0;
                        }
                        fmtString = "N" + precision.ToString();
                    }
                    if (fmt.Substring(0, 1).Equals("I", StringComparison.OrdinalIgnoreCase)) {
                        string s = "";
                        if (c1.Imaginary == 0 & c1.Real == 0) {
                            s = "0";
                        } else if (c1.Imaginary == 0) {
                            s = c1.Real.ToString("r");
                        } else if (c1.Real == 0) {
                            s = c1.Imaginary.ToString("r") + "i";
                        } else {
                            if (c1.Imaginary >= 0) {
                                s = String.Format("{0}+{1}i", c1.Real.ToString("r"), c1.Imaginary.ToString("r"));
                            } else {
                                s = String.Format("{0}-{1}i", c1.Real.ToString("r"), Math.Abs(c1.Imaginary).ToString("r"));
                            }
                        }
                        return s.Replace(",", ".");
                    } else if (fmt.Substring(0, 1).Equals("J", StringComparison.OrdinalIgnoreCase)) {
                        return c1.Real.ToString(fmtString) + " + " + c1.Imaginary.ToString(fmtString) + "j";
                    } else {
                        return c1.ToString(fmt, provider);
                    }
                } else {
                    if (arg is IFormattable) {
                        try {
                            return ((IFormattable)arg).ToString(fmt, provider);
                        } catch (Exception) {
                            return arg.ToString();
                        }
                    } else if (arg != null) {
                        return arg.ToString();
                    } else {
                        return string.Empty;
                    }
                }
            }
        }
    }
}

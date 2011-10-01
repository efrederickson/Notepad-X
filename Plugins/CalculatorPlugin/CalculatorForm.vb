Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Numerics
Imports System.Windows.Forms

Public Class CalculatorForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'RegEx for Real and Complex numbers
    Dim Real As String = "(?<!([E][+-][0-9]+))([-]?\d+\.?\d*([E][+-][0-9]+)?(?!([i0-9.E]))|[-]?\d*\.?\d+([E][+-][0-9]+)?)(?![i0-9.E])"
    Dim Img As String = "(?<!([E][+-][0-9]+))([-]?\d+\.?\d*([E][+-][0-9]+)?(?![0-9.E])(?:i)|([-]?\d*\.?\d+)?([E][+-][0-9]+)?\s*(?:i)(?![0-9.E]))"

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        resultTextBox.Text = ""
        Calculate(OperationTextBox.Text)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        resultTextBox.Text = ""
        Calculate(OperationTextBox.Text)
        Clipboard.SetText(resultTextBox.Text)
    End Sub

    Private Sub exitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitButton.Click
        Me.Close()
    End Sub

    Private Sub Calculate(ByVal Formula As String)
        Dim input As String = Formula
        resultTextBox.Text &= EvaluateBrackets(Formula.Replace(" ", "")) & vbCrLf
    End Sub

    Function EvaluateBrackets(ByVal input As String) As String

        input = "(" & input & ")"

        Dim pattern As String = "(?>\( (?<LEVEL>)(?<CURRENT>)| (?=\))(?<LAST-CURRENT>)(?(?<=\(\k<LAST>)(?<-LEVEL> \)))|\[ (?<LEVEL>)(?<CURRENT>)|(?=\])(?<LAST-CURRENT>)(?(?<=\[\k<LAST>)(?<-LEVEL> \] ))|[^()\[\]]*)+(?(LEVEL)(?!))"

        Dim MAtchBracets As MatchCollection = Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace)

        Dim captures As CaptureCollection = MAtchBracets(0).Groups("LAST").Captures

        Dim ListOfPara As New List(Of String)

        For Each c As Capture In captures
            ListOfPara.Add(c.Value)
        Next

        Dim result As String = input

        Dim CalcList As New List(Of String)
        For i As Integer = 0 To ListOfPara.Count - 1
            If i = 0 Then
                CalcList.Add(Evaluate(ListOfPara(i)))
                result = CalcList(i)
            Else
                For j As Integer = i To ListOfPara.Count - 1
                    ListOfPara(j) = ListOfPara(j).Replace(ListOfPara(i - 1), CalcList(i - 1)).Replace(" ", "")
                Next
                result = Evaluate(ListOfPara(i)).Replace(" ", "")
                CalcList.Add(result)
            End If
        Next
        result = Evaluate(ListOfPara(ListOfPara.Count - 1))
        Return result
    End Function

    ''' <summary>
    ''' Source of code: Programming Visual Basic .NET Author: Franceisco Balena. The code is modified from the original, as this one can deal with complex numbers
    ''' </summary>
    ''' <param name="Input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Evaluate(ByVal Input As String) As String

        Dim NumType As String = "((?<Both>((" & Real & "\s*([+])*\s*" & Img & ")|(" & Img & "\s*([+])*\s*" & Real & ")))|(?<Real>(" & Real & "))|(?<Imag>(" & Img & ")))"
        Dim NumTypeSingle As String = "((?<Real>(" & Real & "))|(?<Imag>(" & Img & ")))"


        Const Func1 As String = "(exp|log|log10|abs|sqr|sqrt|sin|cos|tan|asin|acos|atan)"
        ' List of 2-operand functions.
        Const Func2 As String = "(atan2)"
        ' List of N-operand functions.
        Const FuncN As String = "(min|max)"

        ' List of predefined constants.
        Const Constants As String = "(e|pi)"

        Dim rePower As New Regex("\(?(?<No1>" & NumType & ")\)?" & "\s*(?<Operator>(\^))\s*\(?(?<No2>" & NumType & ")\)?")
        Dim rePower2 As New Regex("\(?(?<No1>" & NumType & ")\)?" & "\s*(?<Operator>(\^))\s*(?<No2>" & NumTypeSingle & ")")
        Dim rePowerSingle As New Regex("(?<No1>" & NumTypeSingle & ")" & "\s*(?<Operator>(\^))\s*(?<No2>" & NumTypeSingle & ")")
        Dim rePowerSingle2 As New Regex("(?<No1>" & NumTypeSingle & ")" & "\s*(?<Operator>(\^))\s*\(?(?<No2>" & NumType & ")\)?")

        Dim reMulDiv As New Regex("\(?\s*(?<No1>" & NumType & ")\)?" & "\s*(?<Operator>([*/]))\s*\(?(?<No2>" & NumType & ")\s*\)?\)?")
        Dim reMulDiv2 As New Regex("\(?\s*(?<No1>" & NumType & ")\)?" & "\s*(?<Operator>([*/]))\s*(?<No2>" & NumTypeSingle & ")")
        Dim reMulDivSingle As New Regex("\(?\s*(?<No1>" & NumTypeSingle & ")" & "\s*(?<Operator>([*/]))\s*(?<No2>" & NumTypeSingle & ")\s*\)?\)?")
        Dim reMulDivSingle2 As New Regex("\(?\s*(?<No1>" & NumTypeSingle & ")" & "\s*(?<Operator>([*/]))\s*\(?(?<No2>" & NumType & ")\s*\)?")

        Dim reAddSub As New Regex("\(?(?<No1>" & NumType & ")\)?" & "\s*(?<Operator>([-+]))\s*\(?(?<No2>" & NumType & ")\)?")

        Dim reFunc1 As New Regex("\s*(?<Function>" & Func1 & ")\(?\s*" & "(?<No1>" & NumType & ")" & "\s*\)?", RegexOptions.IgnoreCase)
        Dim reFunc2 As New Regex("\s*(?<Function>" & Func2 & ")\(\s*" & "(?<No1>" & NumType & ")" & "\s*,\s*" & "(?<No2>" & NumType & ")" & "\s*\)", RegexOptions.IgnoreCase)
        Dim reFuncN As New Regex("\s*(?<Function>" & FuncN & ")\((?<Numbers>(\s*" & NumType & "\s*,)+\s*" & NumType & ")\s*\)", RegexOptions.IgnoreCase)
        Dim reSign1 As New Regex("([-+/*^])\s*\+")

        ' This Regex object converts a double minus into a plus.
        Dim reSign2 As New Regex("\-\s*\-")

        ' This Regex object drops parenthesis around a number.
        ' (must not be preceded by an alphanum char (it might be a function name)
        'Dim rePar As New Regex("(?<![A-Za-z0-9])\(\s*" & NumType & "\s*\)")

        ' A Regex object that tells that the entire expression is a number
        Dim reNum As New Regex("^\s*" & NumType & "\s*$")

        ' The Regex object deals with constants. (Requires case insensitivity.)
        Dim reConst As New Regex("\s*(?<Const>" & Constants & ")\s*")

        ' This resolves predefined constants. (Can be kept out of the loop.)
        Input = reConst.Replace(Input, AddressOf DoConstants)

        Do Until reNum.IsMatch(Input)
            Dim saveExpr As String = Input

            While rePowerSingle.IsMatch(Input)
                Input = rePowerSingle.Replace(Input, AddressOf DoPower).ToString.Replace(" ", "")
            End While

            While rePowerSingle2.IsMatch(Input)
                Input = rePowerSingle2.Replace(Input, AddressOf DoPower).ToString.Replace(" ", "")
            End While

            While rePower2.IsMatch(Input)
                Input = rePower2.Replace(Input, AddressOf DoPower).ToString.Replace(" ", "")
            End While

            Do While reMulDivSingle.IsMatch(Input)
                Input = reMulDivSingle.Replace(Input, AddressOf DoMulDiv).ToString.Replace(" ", "")
            Loop

            Do While reMulDivSingle2.IsMatch(Input)
                Input = reMulDivSingle2.Replace(Input, AddressOf DoMulDiv).ToString.Replace(" ", "")
            Loop

            Do While reMulDiv.IsMatch(Input)
                Input = reMulDiv.Replace(Input, AddressOf DoMulDiv).ToString.Replace(" ", "")
            Loop

            Do While reMulDiv2.IsMatch(Input)
                Input = reMulDiv2.Replace(Input, AddressOf DoMulDiv).ToString.Replace(" ", "")
            Loop

            ' Perform functions with variable numbers of arguments. 
            Do While reFuncN.IsMatch(Input)
                Input = reFuncN.Replace(Input, AddressOf DoFuncN)
            Loop

            ' Perform functions with 2 arguments. 
            Do While reFunc2.IsMatch(Input)
                Input = reFunc2.Replace(Input, AddressOf DoFunc2)
            Loop

            Do While reFunc1.IsMatch(Input)
                Input = reFunc1.Replace(Input, AddressOf DoFunc1)
            Loop

            ' Discard + symbols (unary pluses)that follow another operator.
            Input = reSign1.Replace(Input, "$1")
            ' Simplify 2 consecutive minus signs into a plus sign.
            Input = reSign2.Replace(Input, "+")

            Dim saveAddSub As String = Input
            While reAddSub.IsMatch(Input) And Not Regex.Match(Input, NumType).Groups(0).Value = Input
                Input = reAddSub.Replace(Input, AddressOf DoAddSub).ToString.Replace(" ", "")
                If saveAddSub = Input Then
                    Exit While
                Else
                    saveAddSub = Input
                End If
            End While

            '  expr = rePar.Replace(expr, "$1")

            If Input = saveExpr Then
                Return Input
                ' if it didn't work, exit with syntax error exception.
                Throw New NotImplementedException 'SyntaxErrorException
            End If

        Loop

        Return Input
    End Function

    Function DoAddSub(ByVal m As Match) As String
        Dim n1, n2 As New Complex()
        n1 = GenerateComplexNumberFromString(m.Groups("No1").Value)
        n2 = GenerateComplexNumberFromString(m.Groups("No2").Value)

        Select Case m.Groups("Operator").Value
            Case "+"
                Dim f As New Complex
                f = n1 + n2
                Return String.Format(New ComplexFormatter(), "{0:I0}", f)
            Case "-"
                Dim f As New Complex
                f = n1 - n2
                Return String.Format(New ComplexFormatter(), "{0:I0}", f)
            Case Else
                Return CStr(1)
        End Select
    End Function

    Function DoMulDiv(ByVal m As Match) As String
        Dim n1, n2 As New Complex()
        n1 = GenerateComplexNumberFromString(m.Groups("No1").Value)
        n2 = GenerateComplexNumberFromString(m.Groups("No2").Value)
        Select Case m.Groups("Operator").Value
            Case "/"
                Return String.Format(New ComplexFormatter(), "{0:I0}", (n1 / n2))

            Case "*"
                Return String.Format(New ComplexFormatter(), "{0:I0}", (n1 * n2))
            Case Else
                Return CStr(1)
        End Select
    End Function

    Function DoPower(ByVal m As Match) As String
        Dim n1, n2, n3 As New Complex()
        n1 = GenerateComplexNumberFromString(m.Groups("No1").Value)
        n2 = GenerateComplexNumberFromString(m.Groups("No2").Value)
        n3 = Complex.Pow(n1, n2)
        Dim s As String = String.Format(New ComplexFormatter(), "{0:I0}", n3)
        Return "(" & s & ")"
    End Function

    Function DoFunc1(ByVal m As Match) As String
        ' function argument is 2nd group.
        Dim n1 As New Complex
        n1 = GenerateComplexNumberFromString(m.Groups("No1").Value)
        ' function name is 1st group.
        Select Case m.Groups("Function").Value.ToUpper
            Case "EXP"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Exp(n1))
            Case "LOG"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Log(n1))
            Case "LOG10"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Log10(n1))
            Case "ABS"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Abs(n1))
            Case "SQR", "SQRT"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Sqrt(n1))
            Case "SIN"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Sin(n1))
            Case "COS"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Cos(n1))
            Case "TAN"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Tan(n1))
            Case "ASIN"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Asin(n1))
            Case "ACOS"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Acos(n1))
            Case "ATAN"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Atan(n1))
            Case Else
                Return CStr(1)
        End Select
    End Function

    Function DoFuncN(ByVal m As Match) As String
        ' function arguments are from group 2 onward.
        Dim args As String() = {}
        Dim args2 As New ArrayList
        Dim i As Integer = 2
        ' Load all the arguments into the array.

        For Each h As Capture In m.Groups("Numbers").Captures
            args = h.ToString.Split(CChar(","))
        Next

        For Each Str As String In args
            args2.Add(GenerateComplexNumberFromString(Str.Replace(","c, " "c)))
        Next

        'I cant sort complex numbers, you have a go ;)
        ' function name is 1st group.
        Select Case m.Groups("Function").Value.ToUpper
            Case "MIN"
                args2.Sort()
                Return String.Format(New ComplexFormatter(), "{0:I0}", args(0))
            Case "MAX"
                args2.Sort()
                Return String.Format(New ComplexFormatter(), "{0:I0}", args(args.Count - 1)) 'args(args.Count - 1).ToString
            Case Else
                Return CStr(1)
        End Select
    End Function

    ''' <summary>
    ''' This is a bit unnecessary since Atan2 doesn't exist for 2 variables but it shows how to deal wit this kind of function
    ''' </summary>
    ''' <param name="m"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DoFunc2(ByVal m As Match) As String
        ' function arguments are 2nd and 3rd group.
        Dim n1, n2 As New Complex
        n1 = GenerateComplexNumberFromString(m.Groups("No1").Value)
        n2 = GenerateComplexNumberFromString(m.Groups("No2").Value)
        ' function name is 1st group.
        Select Case m.Groups("Function").Value.ToUpper
            Case "ATAN2"
                Return String.Format(New ComplexFormatter(), "{0:I0}", Complex.Atan(n1 / n2)) '.ToString '.Atan2(n1, n2).ToString
            Case Else
                Return CStr(1)
        End Select
    End Function

    ' These functions evaluate the actual math operations.
    ' In all cases the Match object on entry has groups that identify
    ' the two operands and the operator.
    Function DoConstants(ByVal m As Match) As String
        Select Case m.Groups("Const").Value.ToUpper
            Case "PI"
                Return Math.PI.ToString.Replace(",", ".")
            Case "E"
                Return Math.E.ToString.Replace(",", ".")
            Case Else
                Return CStr(1)
        End Select
    End Function

    Private Function GenerateComplexNumberFromString(ByVal input As String) As Complex
        input = input.Replace(" ", "")

        Dim Number As String = "((?<Real>(" & Real & "))|(?<Imag>(" & Img & ")))"
        Dim Re, Im As Double
        Re = 0
        Im = 0

        For Each Match As Match In Regex.Matches(input, Number)

            If Not Match.Groups("Real").Value = String.Empty Then
                Re = Double.Parse(Match.Groups("Real").Value, CultureInfo.InvariantCulture)
            End If

            If Not Match.Groups("Imag").Value = String.Empty Then
                If Match.Groups("Imag").Value.ToString.Replace(" ", "") = "-i" Then
                    Im = Double.Parse("-1", CultureInfo.InvariantCulture)
                ElseIf Match.Groups("Imag").Value.ToString.Replace(" ", "") = "i" Then
                    Im = Double.Parse("1", CultureInfo.InvariantCulture)
                Else
                    Im = Double.Parse(Match.Groups("Imag").Value.ToString.Replace("i", ""), CultureInfo.InvariantCulture)
                End If
            End If
        Next

        Dim result As New Complex(Re, Im)
        Return result
    End Function

    Public Class ComplexFormatter
        Implements IFormatProvider, ICustomFormatter

        Public Function GetFormat(ByVal formatType As Type) As Object _
                        Implements IFormatProvider.GetFormat
            If formatType Is GetType(ICustomFormatter) Then
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function Format(ByVal fmt As String, ByVal arg As Object,
                               ByVal provider As IFormatProvider) As String _
                        Implements ICustomFormatter.Format
            If TypeOf arg Is Complex Then
                Dim c1 As Complex = DirectCast(arg, Complex)
                ' Check if the format string has a precision specifier.
                Dim precision As Integer
                Dim fmtString As String = String.Empty
                If fmt.Length > 1 Then
                    Try
                        precision = Int32.Parse(fmt.Substring(1))
                    Catch e As FormatException
                        precision = 0
                    End Try
                    fmtString = "N" + precision.ToString()
                End If
                If fmt.Substring(0, 1).Equals("I", StringComparison.OrdinalIgnoreCase) Then
                    Dim s As String = ""
                    If c1.Imaginary = 0 And c1.Real = 0 Then
                        s = "0"
                    ElseIf c1.Imaginary = 0 Then
                        s = c1.Real.ToString("r")
                    ElseIf c1.Real = 0 Then
                        s = c1.Imaginary.ToString("r") & "i"
                    Else
                        If c1.Imaginary >= 0 Then
                            s = [String].Format("{0}+{1}i", c1.Real.ToString("r"), c1.Imaginary.ToString("r"))
                        Else
                            s = [String].Format("{0}-{1}i", c1.Real.ToString("r"), Math.Abs(c1.Imaginary).ToString("r"))
                        End If
                    End If
                    Return s.Replace(",", ".")
                ElseIf fmt.Substring(0, 1).Equals("J", StringComparison.OrdinalIgnoreCase) Then
                    Return c1.Real.ToString(fmtString) + " + " + c1.Imaginary.ToString(fmtString) + "j"
                Else
                    Return c1.ToString(fmt, provider)
                End If
            Else
                If TypeOf arg Is IFormattable Then
                    Try
                        Return DirectCast(arg, IFormattable).ToString(fmt, provider)
                    Catch ex As Exception
                        Return arg.ToString
                    End Try
                ElseIf arg IsNot Nothing Then
                    Return arg.ToString()
                Else
                    Return String.Empty
                End If
            End If
        End Function
    End Class

    Private Sub AlwaysOnTopCalculator_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub
End Class
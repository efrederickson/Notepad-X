Imports System.Collections.Generic
Imports System.Text

Public Partial Class Parser
    Public Function ParseChunk(ByVal input As ParserInput(Of Char), ByRef success As Boolean) As Chunk
        Me.SetInput(input)
        Dim chunk As Chunk = ParseChunk(success)
        If Me.Position < input.Length Then
            success = False
            [Error]("Failed to parse remained input.")
        End If
        Return chunk
    End Function

    Private Function ParseChunk(ByRef success As Boolean) As Chunk
        Dim reskey As New Tuple(Of Integer, String)(Position, "Chunk")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult As Tuple(Of Object, Boolean, Integer)= ParsingResults(reskey)
            success = CBool(parsingResult.Item2)
            Position = CInt(parsingResult.Item3)
            Return TryCast(parsingResult.Item1, Chunk)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim chunk As New Chunk()
        Dim start_position As Integer = position

        ParseSpOpt(success)

        While True
            While True
                Dim seq_start_position1 As Integer = position
                Dim statement As Statement = ParseStatement(success)
                If success Then
                    chunk.Statements.Add(statement)
                Else
                    Exit While
                End If

                While True
                    Dim seq_start_position2 As Integer = position
                    MatchTerminal(";"c, success)
                    If Not success Then
                        Exit While
                    End If

                    ParseSpOpt(success)
                    Exit While
                End While
                success = True
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(chunk, success, position)
        Return chunk
    End Function

    Private Function ParseStatement(ByRef success As Boolean) As Statement
        Dim reskey = New Tuple(Of Integer, String)(position, "Statement")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Statement)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim statement As Statement = Nothing

        statement = ParseAssignment(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseFunction(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseLocalVar(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseLocalFunc(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseReturnStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseBreakStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseDoStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseIfStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseForStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseForInStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseWhileStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseRepeatStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        statement = ParseExprStmt(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(statement, success, position)
            Return statement
        End If

        Return statement
    End Function

    Private Function ParseAssignment(ByRef success As Boolean) As Assignment
        Dim reskey = New Tuple(Of Integer, String)(position, "Assignment")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Assignment)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim assignment As New Assignment()
        Dim start_position As Integer = position

        assignment.VarList = ParseVarList(success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(assignment, success, position)
            Return assignment
        End If

        ParseSpOpt(success)

        MatchTerminal("="c, success)
        If Not success Then
            [Error]("Failed to parse '=' of Assignment.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(assignment, success, position)
            Return assignment
        End If

        ParseSpOpt(success)

        assignment.ExprList = ParseExprList(success)
        If Not success Then
            [Error]("Failed to parse ExprList of Assignment.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(assignment, success, position)
        Return assignment
    End Function

    Private Function ParseFunction(ByRef success As Boolean) As [Function]
        Dim reskey = New Tuple(Of Integer, String)(position, "Function")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, [Function])
        End If

        Dim errorCount As Integer = Errors.Count
        Dim [function] As New [Function]()
        Dim start_position As Integer = position

        MatchTerminalString("function", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)([function], success, position)
            Return [function]
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of Function.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)([function], success, position)
            Return [function]
        End If

        [function].Name = ParseFunctionName(success)
        If Not success Then
            [Error]("Failed to parse Name of Function.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)([function], success, position)
            Return [function]
        End If

        ParseSpOpt(success)

        [function].Body = ParseFunctionBody(success)
        If Not success Then
            [Error]("Failed to parse Body of Function.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)([function], success, position)
        Return [function]
    End Function

    Private Function ParseLocalVar(ByRef success As Boolean) As LocalVar
        Dim reskey = New Tuple(Of Integer, String)(position, "LocalVar")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, LocalVar)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim localVar As New LocalVar()
        Dim start_position As Integer = position

        MatchTerminalString("local", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localVar, success, position)
            Return localVar
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of LocalVar.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localVar, success, position)
            Return localVar
        End If

        localVar.NameList = ParseNameList(success)
        If Not success Then
            [Error]("Failed to parse NameList of LocalVar.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localVar, success, position)
            Return localVar
        End If

        ParseSpOpt(success)

        While True
            Dim seq_start_position1 As Integer = position
            MatchTerminal("="c, success)
            If Not success Then
                Exit While
            End If

            ParseSpOpt(success)

            localVar.ExprList = ParseExprList(success)
            If Not success Then
                [Error]("Failed to parse ExprList of LocalVar.")
                position = seq_start_position1
            End If
            Exit While
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localVar, success, position)
        Return localVar
    End Function

    Private Function ParseLocalFunc(ByRef success As Boolean) As LocalFunc
        Dim reskey = New Tuple(Of Integer, String)(position, "LocalFunc")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, LocalFunc)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim localFunc As New LocalFunc()
        Dim start_position As Integer = position

        MatchTerminalString("local", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
            Return localFunc
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of LocalFunc.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
            Return localFunc
        End If

        MatchTerminalString("function", success)
        If Not success Then
            [Error]("Failed to parse 'function' of LocalFunc.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
            Return localFunc
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of LocalFunc.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
            Return localFunc
        End If

        localFunc.Name = ParseName(success)
        If Not success Then
            [Error]("Failed to parse Name of LocalFunc.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
            Return localFunc
        End If

        ParseSpOpt(success)

        localFunc.Body = ParseFunctionBody(success)
        If Not success Then
            [Error]("Failed to parse Body of LocalFunc.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(localFunc, success, position)
        Return localFunc
    End Function

    Private Function ParseExprStmt(ByRef success As Boolean) As ExprStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "ExprStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ExprStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim exprStmt As New ExprStmt()

        exprStmt.Expr = ParseExpr(success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Expr of ExprStmt.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(exprStmt, success, position)
        Return exprStmt
    End Function

    Private Function ParseReturnStmt(ByRef success As Boolean) As ReturnStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "ReturnStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ReturnStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim returnStmt As New ReturnStmt()
        Dim start_position As Integer = position

        MatchTerminalString("return", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(returnStmt, success, position)
            Return returnStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ReturnStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(returnStmt, success, position)
            Return returnStmt
        End If

        returnStmt.ExprList = ParseExprList(success)
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(returnStmt, success, position)
        Return returnStmt
    End Function

    Private Function ParseBreakStmt(ByRef success As Boolean) As BreakStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "BreakStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, BreakStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim breakStmt As New BreakStmt()
        Dim start_position As Integer = position

        MatchTerminalString("break", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(breakStmt, success, position)
            Return breakStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of BreakStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(breakStmt, success, position)
        Return breakStmt
    End Function

    Private Function ParseDoStmt(ByRef success As Boolean) As DoStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "DoStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, DoStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim doStmt As New DoStmt()
        Dim start_position As Integer = position

        MatchTerminalString("do", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(doStmt, success, position)
            Return doStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of DoStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(doStmt, success, position)
            Return doStmt
        End If

        doStmt.Body = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Body of DoStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(doStmt, success, position)
            Return doStmt
        End If

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of DoStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(doStmt, success, position)
            Return doStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of DoStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(doStmt, success, position)
        Return doStmt
    End Function

    Private Function ParseIfStmt(ByRef success As Boolean) As IfStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "IfStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, IfStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim ifStmt As New IfStmt()
        Dim start_position As Integer = position

        MatchTerminalString("if", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        ifStmt.Condition = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Condition of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        MatchTerminalString("then", success)
        If Not success Then
            [Error]("Failed to parse 'then' of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        ifStmt.ThenBlock = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse ThenBlock of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        While True
            Dim elseifBlock As ElseifBlock = ParseElseifBlock(success)
            If success Then
                ifStmt.ElseifBlocks.Add(elseifBlock)
            Else
                Exit While
            End If
        End While
        success = True

        While True
            Dim seq_start_position1 As Integer = position
            MatchTerminalString("else", success)
            If Not success Then
                Exit While
            End If

            ParseSpReq(success)
            If Not success Then
                [Error]("Failed to parse SpReq of IfStmt.")
                position = seq_start_position1
                Exit While
            End If

            ifStmt.ElseBlock = ParseChunk(success)
            If Not success Then
                [Error]("Failed to parse ElseBlock of IfStmt.")
                position = seq_start_position1
            End If
            Exit While
        End While
        success = True

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of IfStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
            Return ifStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of IfStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(ifStmt, success, position)
        Return ifStmt
    End Function

    Private Function ParseElseifBlock(ByRef success As Boolean) As ElseifBlock
        Dim reskey = New Tuple(Of Integer, String)(position, "ElseifBlock")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ElseifBlock)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim elseifBlock As New ElseifBlock()
        Dim start_position As Integer = position

        MatchTerminalString("elseif", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
            Return elseifBlock
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ElseifBlock.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
            Return elseifBlock
        End If

        elseifBlock.Condition = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Condition of ElseifBlock.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
            Return elseifBlock
        End If

        MatchTerminalString("then", success)
        If Not success Then
            [Error]("Failed to parse 'then' of ElseifBlock.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
            Return elseifBlock
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ElseifBlock.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
            Return elseifBlock
        End If

        elseifBlock.ThenBlock = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse ThenBlock of ElseifBlock.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(elseifBlock, success, position)
        Return elseifBlock
    End Function

    Private Function ParseForStmt(ByRef success As Boolean) As ForStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "ForStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ForStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim forStmt As New ForStmt()
        Dim start_position As Integer = position

        MatchTerminalString("for", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        forStmt.VarName = ParseName(success)
        If Not success Then
            [Error]("Failed to parse VarName of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpOpt(success)

        MatchTerminal("="c, success)
        If Not success Then
            [Error]("Failed to parse '=' of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpOpt(success)

        forStmt.Start = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Start of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        MatchTerminal(","c, success)
        If Not success Then
            [Error]("Failed to parse ',' of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpOpt(success)

        forStmt.[End] = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse End of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        While True
            Dim seq_start_position1 As Integer = position
            MatchTerminal(","c, success)
            If Not success Then
                Exit While
            End If

            ParseSpOpt(success)

            forStmt.[Step] = ParseExpr(success)
            If Not success Then
                [Error]("Failed to parse Step of ForStmt.")
                position = seq_start_position1
            End If
            Exit While
        End While
        success = True

        MatchTerminalString("do", success)
        If Not success Then
            [Error]("Failed to parse 'do' of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        forStmt.Body = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Body of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of ForStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
            Return forStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forStmt, success, position)
        Return forStmt
    End Function

    Private Function ParseForInStmt(ByRef success As Boolean) As ForInStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "ForInStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ForInStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim forInStmt As New ForInStmt()
        Dim start_position As Integer = position

        MatchTerminalString("for", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        forInStmt.NameList = ParseNameList(success)
        If Not success Then
            [Error]("Failed to parse NameList of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        MatchTerminalString("in", success)
        If Not success Then
            [Error]("Failed to parse 'in' of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        forInStmt.ExprList = ParseExprList(success)
        If Not success Then
            [Error]("Failed to parse ExprList of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        MatchTerminalString("do", success)
        If Not success Then
            [Error]("Failed to parse 'do' of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        forInStmt.Body = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Body of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of ForInStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
            Return forInStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of ForInStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(forInStmt, success, position)
        Return forInStmt
    End Function

    Private Function ParseWhileStmt(ByRef success As Boolean) As WhileStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "WhileStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, WhileStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim whileStmt As New WhileStmt()
        Dim start_position As Integer = position

        MatchTerminalString("while", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        whileStmt.Condition = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Condition of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        MatchTerminalString("do", success)
        If Not success Then
            [Error]("Failed to parse 'do' of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        whileStmt.Body = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Body of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of WhileStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
            Return whileStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of WhileStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(whileStmt, success, position)
        Return whileStmt
    End Function

    Private Function ParseRepeatStmt(ByRef success As Boolean) As RepeatStmt
        Dim reskey = New Tuple(Of Integer, String)(position, "RepeatStmt")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, RepeatStmt)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim repeatStmt As New RepeatStmt()
        Dim start_position As Integer = position

        MatchTerminalString("repeat", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
            Return repeatStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of RepeatStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
            Return repeatStmt
        End If

        repeatStmt.Body = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Body of RepeatStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
            Return repeatStmt
        End If

        MatchTerminalString("until", success)
        If Not success Then
            [Error]("Failed to parse 'until' of RepeatStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
            Return repeatStmt
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of RepeatStmt.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
            Return repeatStmt
        End If

        repeatStmt.Condition = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Condition of RepeatStmt.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(repeatStmt, success, position)
        Return repeatStmt
    End Function

    Private Function ParseVarList(ByRef success As Boolean) As List(Of Var)
        Dim reskey = New Tuple(Of Integer, String)(position, "VarList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of Var))
        End If

        Dim errorCount As Integer = Errors.Count
        Dim list_Var As New List(Of Var)()
        Dim start_position As Integer = position

        Dim var As Var = ParseVar(success)
        If success Then
            list_Var.Add(var)
        Else
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Var, success, position)
            Return list_Var
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseSpOpt(success)

                MatchTerminal(","c, success)
                If Not success Then
                    [Error]("Failed to parse ',' of VarList.")
                    position = seq_start_position1
                    Exit While
                End If

                ParseSpOpt(success)

                var = ParseVar(success)
                If success Then
                    list_Var.Add(var)
                Else
                    [Error]("Failed to parse Var of VarList.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Var, success, position)
        Return list_Var
    End Function

    Private Function ParseExprList(ByRef success As Boolean) As List(Of Expr)
        Dim reskey = New Tuple(Of Integer, String)(position, "ExprList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of Expr))
        End If

        Dim errorCount As Integer = Errors.Count
        Dim list_Expr As New List(Of Expr)()
        Dim start_position As Integer = position

        Dim expr As Expr = ParseExpr(success)
        If success Then
            list_Expr.Add(expr)
        Else
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Expr, success, position)
            Return list_Expr
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseSpOpt(success)

                MatchTerminal(","c, success)
                If Not success Then
                    [Error]("Failed to parse ',' of ExprList.")
                    position = seq_start_position1
                    Exit While
                End If

                ParseSpOpt(success)

                expr = ParseExpr(success)
                If success Then
                    list_Expr.Add(expr)
                Else
                    [Error]("Failed to parse Expr of ExprList.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Expr, success, position)
        Return list_Expr
    End Function

    Private Function ParseExpr(ByRef success As Boolean) As Expr
        Dim reskey = New Tuple(Of Integer, String)(position, "Expr")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Expr)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim expr As Expr = Nothing

        expr = ParseOperatorExpr(success)
        If success Then
            Return expr.Simplify()
        End If

        expr = ParseTerm(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(expr, success, position)
            Return expr
        End If

        Return expr
    End Function

    Private Function ParseTerm(ByRef success As Boolean) As Term
        Dim reskey = New Tuple(Of Integer, String)(position, "Term")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Term)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim term As Term = Nothing

        term = ParseNilLiteral(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseBoolLiteral(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseNumberLiteral(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseStringLiteral(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseFunctionValue(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseTableConstructor(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParseVariableArg(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        term = ParsePrimaryExpr(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(term, success, position)
            Return term
        End If

        Return term
    End Function

    Private Function ParseNilLiteral(ByRef success As Boolean) As NilLiteral
        Dim reskey = New Tuple(Of Integer, String)(position, "NilLiteral")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, NilLiteral)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim nilLiteral As New NilLiteral()

        MatchTerminalString("nil", success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse 'nil' of NilLiteral.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nilLiteral, success, position)
        Return nilLiteral
    End Function

    Private Function ParseBoolLiteral(ByRef success As Boolean) As BoolLiteral
        Dim reskey = New Tuple(Of Integer, String)(position, "BoolLiteral")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, BoolLiteral)
        End If

        Dim errorCount As Integer = Errors.Count
        ErrorStatck.Push(errorCount)
        errorCount = Errors.Count
        Dim boolLiteral As New BoolLiteral()

        While True
            boolLiteral.Text = MatchTerminalString("true", success)
            If success Then
                ClearError(errorCount)
                Exit While
            End If

            boolLiteral.Text = MatchTerminalString("false", success)
            If success Then
                ClearError(errorCount)
                Exit While
            End If

            Exit While
        End While
        errorCount = ErrorStatck.Pop()
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Text of BoolLiteral.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(boolLiteral, success, position)
        Return boolLiteral
    End Function

    Private Function ParseNumberLiteral(ByRef success As Boolean) As NumberLiteral
        Dim reskey = New Tuple(Of Integer, String)(position, "NumberLiteral")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, NumberLiteral)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim numberLiteral As New NumberLiteral()

        numberLiteral.HexicalText = ParseHexicalNumber(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(numberLiteral, success, position)
            Return numberLiteral
        End If

        numberLiteral.Text = ParseFoatNumber(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(numberLiteral, success, position)
            Return numberLiteral
        End If

        Return numberLiteral
    End Function

    Private Function ParseStringLiteral(ByRef success As Boolean) As StringLiteral
        Dim reskey = New Tuple(Of Integer, String)(position, "StringLiteral")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, StringLiteral)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim stringLiteral As New StringLiteral()

        While True
            Dim seq_start_position1 As Integer = position
            MatchTerminal(""""c, success)
            If Not success Then
                Exit While
            End If

            stringLiteral.Text = ParseDoubleQuotedText(success)

            MatchTerminal(""""c, success)
            If Not success Then
                [Error]("Failed to parse '\""' of StringLiteral.")
                position = seq_start_position1
            End If
            Exit While
        End While
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(stringLiteral, success, position)
            Return stringLiteral
        End If

        While True
            Dim seq_start_position2 As Integer = position
            MatchTerminal("'"c, success)
            If Not success Then
                Exit While
            End If

            stringLiteral.Text = ParseSingleQuotedText(success)

            MatchTerminal("'"c, success)
            If Not success Then
                [Error]("Failed to parse ''' of StringLiteral.")
                position = seq_start_position2
            End If
            Exit While
        End While
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(stringLiteral, success, position)
            Return stringLiteral
        End If

        stringLiteral.Text = ParseLongString(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(stringLiteral, success, position)
            Return stringLiteral
        End If

        Return stringLiteral
    End Function

    Private Function ParseVariableArg(ByRef success As Boolean) As VariableArg
        Dim reskey = New Tuple(Of Integer, String)(position, "VariableArg")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, VariableArg)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim variableArg As New VariableArg()

        variableArg.Name = MatchTerminalString("...", success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Name of VariableArg.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(variableArg, success, position)
        Return variableArg
    End Function

    Private Function ParseFunctionValue(ByRef success As Boolean) As FunctionValue
        Dim reskey = New Tuple(Of Integer, String)(position, "FunctionValue")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, FunctionValue)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim functionValue As New FunctionValue()
        Dim start_position As Integer = position

        MatchTerminalString("function", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionValue, success, position)
            Return functionValue
        End If

        ParseSpOpt(success)

        functionValue.Body = ParseFunctionBody(success)
        If Not success Then
            [Error]("Failed to parse Body of FunctionValue.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionValue, success, position)
        Return functionValue
    End Function

    Private Function ParseFunctionBody(ByRef success As Boolean) As FunctionBody
        Dim reskey = New Tuple(Of Integer, String)(position, "FunctionBody")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, FunctionBody)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim functionBody As New FunctionBody()
        Dim start_position As Integer = position

        MatchTerminal("("c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionBody, success, position)
            Return functionBody
        End If

        ParseSpOpt(success)

        While True
            Dim seq_start_position1 As Integer = position
            functionBody.ParamList = ParseParamList(success)
            If Not success Then
                Exit While
            End If

            ParseSpOpt(success)
            Exit While
        End While
        success = True

        MatchTerminal(")"c, success)
        If Not success Then
            [Error]("Failed to parse ')' of FunctionBody.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionBody, success, position)
            Return functionBody
        End If

        functionBody.Chunk = ParseChunk(success)
        If Not success Then
            [Error]("Failed to parse Chunk of FunctionBody.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionBody, success, position)
            Return functionBody
        End If

        MatchTerminalString("end", success)
        If Not success Then
            [Error]("Failed to parse 'end' of FunctionBody.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionBody, success, position)
            Return functionBody
        End If

        ParseSpReq(success)
        If Not success Then
            [Error]("Failed to parse SpReq of FunctionBody.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionBody, success, position)
        Return functionBody
    End Function

    Private Function ParseAccess(ByRef success As Boolean) As Access
        Dim reskey = New Tuple(Of Integer, String)(position, "Access")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Access)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim access As Access = Nothing

        access = ParseNameAccess(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(access, success, position)
            Return access
        End If

        access = ParseKeyAccess(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(access, success, position)
            Return access
        End If

        access = ParseMethodCall(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(access, success, position)
            Return access
        End If

        access = ParseFunctionCall(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(access, success, position)
            Return access
        End If

        Return access
    End Function

    Private Function ParseBaseExpr(ByRef success As Boolean) As BaseExpr
        Dim reskey = New Tuple(Of Integer, String)(position, "BaseExpr")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, BaseExpr)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim baseExpr As BaseExpr = Nothing

        baseExpr = ParseGroupExpr(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(baseExpr, success, position)
            Return baseExpr
        End If

        baseExpr = ParseVarName(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(baseExpr, success, position)
            Return baseExpr
        End If

        Return baseExpr
    End Function

    Private Function ParseKeyAccess(ByRef success As Boolean) As KeyAccess
        Dim reskey = New Tuple(Of Integer, String)(position, "KeyAccess")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, KeyAccess)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim keyAccess As New KeyAccess()
        Dim start_position As Integer = position

        MatchTerminal("["c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyAccess, success, position)
            Return keyAccess
        End If

        ParseSpOpt(success)

        keyAccess.Key = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Key of KeyAccess.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyAccess, success, position)
            Return keyAccess
        End If

        MatchTerminal("]"c, success)
        If Not success Then
            [Error]("Failed to parse ']' of KeyAccess.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyAccess, success, position)
        Return keyAccess
    End Function

    Private Function ParseNameAccess(ByRef success As Boolean) As NameAccess
        Dim reskey = New Tuple(Of Integer, String)(position, "NameAccess")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, NameAccess)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim nameAccess As New NameAccess()
        Dim start_position As Integer = position

        MatchTerminal("."c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nameAccess, success, position)
            Return nameAccess
        End If

        ParseSpOpt(success)

        nameAccess.Name = ParseName(success)
        If Not success Then
            [Error]("Failed to parse Name of NameAccess.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nameAccess, success, position)
        Return nameAccess
    End Function

    Private Function ParseMethodCall(ByRef success As Boolean) As MethodCall
        Dim reskey = New Tuple(Of Integer, String)(position, "MethodCall")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, MethodCall)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim methodCall As New MethodCall()
        Dim start_position As Integer = position

        MatchTerminal(":"c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(methodCall, success, position)
            Return methodCall
        End If

        ParseSpOpt(success)

        methodCall.Method = ParseName(success)
        If Not success Then
            [Error]("Failed to parse Method of MethodCall.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(methodCall, success, position)
            Return methodCall
        End If

        ParseSpOpt(success)

        methodCall.Args = ParseArgs(success)
        If Not success Then
            [Error]("Failed to parse Args of MethodCall.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(methodCall, success, position)
        Return methodCall
    End Function

    Private Function ParseFunctionCall(ByRef success As Boolean) As FunctionCall
        Dim reskey = New Tuple(Of Integer, String)(position, "FunctionCall")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, FunctionCall)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim functionCall As New FunctionCall()

        functionCall.Args = ParseArgs(success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Args of FunctionCall.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionCall, success, position)
        Return functionCall
    End Function

    Private Function ParseVar(ByRef success As Boolean) As Var
        Dim reskey = New Tuple(Of Integer, String)(position, "Var")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Var)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim var As New Var()
        Dim start_position As Integer = position

        var.Base = ParseBaseExpr(success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(var, success, position)
            Return var
        End If

        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                While True
                    Dim seq_start_position1 As Integer = position
                    ParseSpOpt(success)

                    Dim nameAccess As NameAccess = ParseNameAccess(success)
                    If success Then
                        var.Accesses.Add(nameAccess)
                    Else
                        [Error]("Failed to parse NameAccess of Var.")
                        position = seq_start_position1
                    End If
                    Exit While
                End While
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                While True
                    Dim seq_start_position2 As Integer = position
                    ParseSpOpt(success)

                    Dim keyAccess As KeyAccess = ParseKeyAccess(success)
                    If success Then
                        var.Accesses.Add(keyAccess)
                    Else
                        [Error]("Failed to parse KeyAccess of Var.")
                        position = seq_start_position2
                    End If
                    Exit While
                End While
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(var, success, position)
        Return var
    End Function

    Private Function ParsePrimaryExpr(ByRef success As Boolean) As PrimaryExpr
        Dim reskey = New Tuple(Of Integer, String)(position, "PrimaryExpr")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, PrimaryExpr)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim primaryExpr As New PrimaryExpr()
        Dim start_position As Integer = position

        primaryExpr.Base = ParseBaseExpr(success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(primaryExpr, success, position)
            Return primaryExpr
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseSpOpt(success)

                Dim access As Access = ParseAccess(success)
                If success Then
                    primaryExpr.Accesses.Add(access)
                Else
                    [Error]("Failed to parse Access of PrimaryExpr.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(primaryExpr, success, position)
        Return primaryExpr
    End Function

    Private Function ParseVarName(ByRef success As Boolean) As VarName
        Dim reskey = New Tuple(Of Integer, String)(position, "VarName")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, VarName)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim varName As New VarName()

        varName.Name = ParseName(success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Name of VarName.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(varName, success, position)
        Return varName
    End Function

    Private Function ParseFunctionName(ByRef success As Boolean) As FunctionName
        Dim reskey = New Tuple(Of Integer, String)(position, "FunctionName")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, FunctionName)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim functionName As New FunctionName()
        Dim start_position As Integer = position

        functionName.FullName = ParseFullName(success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionName, success, position)
            Return functionName
        End If

        While True
            Dim seq_start_position1 As Integer = position
            ParseSpOpt(success)

            MatchTerminal(":"c, success)
            If Not success Then
                [Error]("Failed to parse ':' of FunctionName.")
                position = seq_start_position1
                Exit While
            End If

            ParseSpOpt(success)

            functionName.MethodName = ParseName(success)
            If Not success Then
                [Error]("Failed to parse MethodName of FunctionName.")
                position = seq_start_position1
            End If
            Exit While
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(functionName, success, position)
        Return functionName
    End Function

    Private Function ParseGroupExpr(ByRef success As Boolean) As GroupExpr
        Dim reskey = New Tuple(Of Integer, String)(position, "GroupExpr")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, GroupExpr)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim groupExpr As New GroupExpr()
        Dim start_position As Integer = position

        MatchTerminal("("c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(groupExpr, success, position)
            Return groupExpr
        End If

        ParseSpOpt(success)

        groupExpr.Expr = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Expr of GroupExpr.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(groupExpr, success, position)
            Return groupExpr
        End If

        MatchTerminal(")"c, success)
        If Not success Then
            [Error]("Failed to parse ')' of GroupExpr.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(groupExpr, success, position)
        Return groupExpr
    End Function

    Private Function ParseTableConstructor(ByRef success As Boolean) As TableConstructor
        Dim reskey = New Tuple(Of Integer, String)(position, "TableConstructor")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, TableConstructor)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim tableConstructor As New TableConstructor()
        Dim start_position As Integer = position

        MatchTerminal("{"c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(tableConstructor, success, position)
            Return tableConstructor
        End If

        ParseSpOpt(success)

        tableConstructor.FieldList = ParseFieldList(success)
        success = True

        MatchTerminal("}"c, success)
        If Not success Then
            [Error]("Failed to parse '}' of TableConstructor.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(tableConstructor, success, position)
        Return tableConstructor
    End Function

    Private Function ParseFieldList(ByRef success As Boolean) As List(Of Field)
        Dim reskey = New Tuple(Of Integer, String)(position, "FieldList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of Field))
        End If

        Dim errorCount As Integer = Errors.Count
        Dim list_Field As New List(Of Field)()
        Dim start_position As Integer = position

        Dim field As Field = ParseField(success)
        If success Then
            list_Field.Add(field)
        Else
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Field, success, position)
            Return list_Field
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseFieldSep(success)
                If Not success Then
                    Exit While
                End If

                ParseSpOpt(success)

                field = ParseField(success)
                If success Then
                    list_Field.Add(field)
                Else
                    [Error]("Failed to parse Field of FieldList.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        While True
            Dim seq_start_position2 As Integer = position
            ParseFieldSep(success)
            If Not success Then
                Exit While
            End If

            ParseSpOpt(success)
            Exit While
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Field, success, position)
        Return list_Field
    End Function

    Private Function ParseField(ByRef success As Boolean) As Field
        Dim reskey = New Tuple(Of Integer, String)(position, "Field")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Field)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim field As Field = Nothing

        field = ParseKeyValue(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(field, success, position)
            Return field
        End If

        field = ParseNameValue(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(field, success, position)
            Return field
        End If

        field = ParseItemValue(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(field, success, position)
            Return field
        End If

        Return field
    End Function

    Private Function ParseKeyValue(ByRef success As Boolean) As KeyValue
        Dim reskey = New Tuple(Of Integer, String)(position, "KeyValue")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, KeyValue)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim keyValue As New KeyValue()
        Dim start_position As Integer = position

        MatchTerminal("["c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyValue, success, position)
            Return keyValue
        End If

        ParseSpOpt(success)

        keyValue.Key = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Key of KeyValue.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyValue, success, position)
            Return keyValue
        End If

        MatchTerminal("]"c, success)
        If Not success Then
            [Error]("Failed to parse ']' of KeyValue.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyValue, success, position)
            Return keyValue
        End If

        ParseSpOpt(success)

        MatchTerminal("="c, success)
        If Not success Then
            [Error]("Failed to parse '=' of KeyValue.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyValue, success, position)
            Return keyValue
        End If

        ParseSpOpt(success)

        keyValue.Value = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Value of KeyValue.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(keyValue, success, position)
        Return keyValue
    End Function

    Private Function ParseNameValue(ByRef success As Boolean) As NameValue
        Dim reskey = New Tuple(Of Integer, String)(position, "NameValue")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, NameValue)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim nameValue As New NameValue()
        Dim start_position As Integer = position

        nameValue.Name = ParseName(success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nameValue, success, position)
            Return nameValue
        End If

        ParseSpOpt(success)

        MatchTerminal("="c, success)
        If Not success Then
            [Error]("Failed to parse '=' of NameValue.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nameValue, success, position)
            Return nameValue
        End If

        ParseSpOpt(success)

        nameValue.Value = ParseExpr(success)
        If Not success Then
            [Error]("Failed to parse Value of NameValue.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(nameValue, success, position)
        Return nameValue
    End Function

    Private Function ParseItemValue(ByRef success As Boolean) As ItemValue
        Dim reskey = New Tuple(Of Integer, String)(position, "ItemValue")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ItemValue)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim itemValue As New ItemValue()

        itemValue.Value = ParseExpr(success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse Value of ItemValue.")
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(itemValue, success, position)
        Return itemValue
    End Function

    Private Function ParseOperatorExpr(ByRef success As Boolean) As OperatorExpr
        Dim reskey = New Tuple(Of Integer, String)(position, "OperatorExpr")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, OperatorExpr)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim operatorExpr As New OperatorExpr()
        Dim start_position As Integer = position

        While True
            Dim seq_start_position1 As Integer = position
            Dim unaryOper As String = ParseUnaryOperator(success)
            If success Then
                operatorExpr.Add(unaryOper)
            Else
                Exit While
            End If

            ParseSpOpt(success)
            Exit While
        End While
        success = True

        Dim firstTerm As Term = ParseTerm(success)
        If success Then
            operatorExpr.Add(firstTerm)
        Else
            [Error]("Failed to parse firstTerm of OperatorExpr.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(operatorExpr, success, position)
            Return operatorExpr
        End If

        ParseSpOpt(success)

        While True
            While True
                Dim seq_start_position2 As Integer = position
                Dim binaryOper As String = ParseBinaryOperator(success)
                If success Then
                    operatorExpr.Add(binaryOper)
                Else
                    Exit While
                End If

                ParseSpOpt(success)

                Dim nextTerm As Term = ParseTerm(success)
                If success Then
                    operatorExpr.Add(nextTerm)
                Else
                    [Error]("Failed to parse nextTerm of OperatorExpr.")
                    position = seq_start_position2
                    Exit While
                End If

                ParseSpOpt(success)
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(operatorExpr, success, position)
        Return operatorExpr
    End Function

    Private Function ParseArgs(ByRef success As Boolean) As Args
        Dim reskey = New Tuple(Of Integer, String)(position, "Args")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, Args)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim args As New Args()

        args.ArgList = ParseArgList(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(args, success, position)
            Return args
        End If

        args.[String] = ParseStringLiteral(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(args, success, position)
            Return args
        End If

        args.Table = ParseTableConstructor(success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(args, success, position)
            Return args
        End If

        Return args
    End Function

    Private Function ParseArgList(ByRef success As Boolean) As List(Of Expr)
        Dim reskey = New Tuple(Of Integer, String)(position, "ArgList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of Expr))
        End If

        Dim errorCount As Integer = Errors.Count
        Dim list_Expr As New List(Of Expr)()
        Dim start_position As Integer = position

        MatchTerminal("("c, success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Expr, success, position)
            Return list_Expr
        End If

        ParseSpOpt(success)

        While True
            Dim seq_start_position1 As Integer = position
            list_Expr = ParseExprList(success)
            If Not success Then
                Exit While
            End If

            ParseSpOpt(success)
            Exit While
        End While
        success = True

        MatchTerminal(")"c, success)
        If Not success Then
            [Error]("Failed to parse ')' of ArgList.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_Expr, success, position)
        Return list_Expr
    End Function

    Private Function ParseParamList(ByRef success As Boolean) As ParamList
        Dim reskey = New Tuple(Of Integer, String)(position, "ParamList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, ParamList)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim paramList As New ParamList()

        While True
            Dim seq_start_position1 As Integer = position
            paramList.NameList = ParseNameList(success)
            If Not success Then
                Exit While
            End If

            While True
                Dim seq_start_position2 As Integer = position
                MatchTerminal(","c, success)
                If Not success Then
                    Exit While
                End If

                ParseSpOpt(success)

                MatchTerminalString("...", success)
                If Not success Then
                    [Error]("Failed to parse '...' of ParamList.")
                    position = seq_start_position2
                End If
                Exit While
            End While
            paramList.HasVarArg = success
            success = True
            Exit While
        End While
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(paramList, success, position)
            Return paramList
        End If

        paramList.IsVarArg = MatchTerminalString("...", success)
        If success Then
            ClearError(errorCount)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(paramList, success, position)
            Return paramList
        End If

        Return paramList
    End Function

    Private Function ParseFullName(ByRef success As Boolean) As List(Of String)
        Dim reskey = New Tuple(Of Integer, String)(position, "FullName")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of String))
        End If

        Dim list_string As New List(Of String)()
        Dim start_position As Integer = position

        Dim str As String = ParseName(success)
        If success Then
            list_string.Add(str)
        Else
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_string, success, position)
            Return list_string
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseSpOpt(success)

                MatchTerminal("."c, success)
                If Not success Then
                    [Error]("Failed to parse '.' of FullName.")
                    position = seq_start_position1
                    Exit While
                End If

                ParseSpOpt(success)

                str = ParseName(success)
                If success Then
                    list_string.Add(str)
                Else
                    [Error]("Failed to parse Name of FullName.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_string, success, position)
        Return list_string
    End Function

    Private Function ParseNameList(ByRef success As Boolean) As List(Of String)
        Dim reskey = New Tuple(Of Integer, String)(position, "NameList")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, List(Of String))
        End If

        Dim list_string As New List(Of String)()
        Dim start_position As Integer = position

        Dim str As String = ParseName(success)
        If success Then
            list_string.Add(str)
        Else
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_string, success, position)
            Return list_string
        End If

        While True
            While True
                Dim seq_start_position1 As Integer = position
                ParseSpOpt(success)

                MatchTerminal(","c, success)
                If Not success Then
                    [Error]("Failed to parse ',' of NameList.")
                    position = seq_start_position1
                    Exit While
                End If

                ParseSpOpt(success)

                str = ParseName(success)
                If success Then
                    list_string.Add(str)
                Else
                    [Error]("Failed to parse Name of NameList.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(list_string, success, position)
        Return list_string
    End Function

    Private Function ParseName(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "Name")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()
        Dim start_position As Integer = position

        Dim not_start_position1 As Integer = position
        While True
            ParseKeyword(success)
            If Not success Then
                Exit While
            End If

            ParseSpReq(success)
            Exit While
        End While
        position = not_start_position1
        success = Not success
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Dim ch As Char = ParseLetter(success)
        If success Then
            text.Append(ch)
        Else
            [Error]("Failed to parse Letter of Name.")
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                ch = ParseLetter(success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                ch = ParseDigit(success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Function ParseFoatNumber(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "FoatNumber")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()
        Dim start_position As Integer = position

        Dim counter As Integer = 0
        While True
            Dim ch As Char = ParseDigit(success)
            If success Then
                text.Append(ch)
            Else
                Exit While
            End If
            counter += 1
        End While
        If counter > 0 Then
            success = True
        End If
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        While True
            Dim seq_start_position1 As Integer = position
            Dim ch As Char = MatchTerminal("."c, success)
            If success Then
                text.Append(ch)
            Else
                Exit While
            End If

            counter = 0
            While True
                ch = ParseDigit(success)
                If success Then
                    text.Append(ch)
                Else
                    Exit While
                End If
                counter += 1
            End While
            If counter > 0 Then
                success = True
            End If
            If Not success Then
                [Error]("Failed to parse (Digit)+ of FoatNumber.")
                position = seq_start_position1
            End If
            Exit While
        End While
        success = True

        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            Dim seq_start_position2 As Integer = position
            While True
                Dim ch As Char = MatchTerminal("e"c, success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                ch = MatchTerminal("E"c, success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If

            counter = 0
            While True
                Dim ch As Char = ParseDigit(success)
                If success Then
                    text.Append(ch)
                Else
                    Exit While
                End If
                counter += 1
            End While
            If counter > 0 Then
                success = True
            End If
            If Not success Then
                [Error]("Failed to parse (Digit)+ of FoatNumber.")
                position = seq_start_position2
            End If
            Exit While
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Function ParseHexicalNumber(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "HexicalNumber")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()
        Dim start_position As Integer = position

        MatchTerminalString("0x", success)
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Dim counter As Integer = 0
        While True
            Dim ch As Char = ParseHexDigit(success)
            If success Then
                text.Append(ch)
            Else
                Exit While
            End If
            counter += 1
        End While
        If counter > 0 Then
            success = True
        End If
        If Not success Then
            [Error]("Failed to parse (HexDigit)+ of HexicalNumber.")
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Function ParseSingleQuotedText(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "SingleQuotedText")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()

        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                Dim ch As Char = MatchTerminalSet("'\", True, success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                ch = ParseEscapeChar(success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
        End While
        success = True
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Function ParseDoubleQuotedText(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "DoubleQuotedText")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()

        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                Dim ch As Char = MatchTerminalSet("""\", True, success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                ch = ParseEscapeChar(success)
                If success Then
                    ClearError(errorCount)
                    text.Append(ch)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
        End While
        success = True
        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Function ParseLongString(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "LongString")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim text As New StringBuilder()
        Dim start_position As Integer = position

        Dim opening As New List(Of Char)()
        While True
            Dim seq_start_position1 As Integer = position
            Dim ch As Char = MatchTerminal("["c, success)
            If success Then
                opening.Add(ch)
            Else
                Exit While
            End If

            While True
                ch = MatchTerminal("="c, success)
                If success Then
                    opening.Add(ch)
                Else
                    Exit While
                End If
            End While
            success = True

            ch = MatchTerminal("["c, success)
            If success Then
                opening.Add(ch)
            Else
                [Error]("Failed to parse '[' of LongString.")
                position = seq_start_position1
            End If
            Exit While
        End While
        If Not success Then
            position = start_position
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ParseEol(success)
        Dim closing As String = New String(opening.ToArray()).Replace("["c, "]"c)
        success = True

        While True
            MatchTerminalString(closing, success)
            If success Then
                Exit While
            End If
            Dim ch As Char = MatchTerminalSet("", True, success)
            If success Then
                text.Append(ch)
            Else
                Exit While
            End If
        End While
        success = True

        ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
        Return text.ToString()
    End Function

    Private Sub ParseKeyword(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        MatchTerminalString("and", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("break", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("do", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("elseif", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("else", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("end", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("false", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("for", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("function", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("if", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("in", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("local", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("nil", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("not", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("or", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("repeat", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("return", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("then", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("true", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("until", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminalString("while", success)
        If success Then
            ClearError(errorCount)
            Return
        End If

    End Sub

    Private Function ParseDigit(ByRef success As Boolean) As Char
        Dim errorCount As Integer = Errors.Count
        Dim ch As Char = MatchTerminalRange("0"c, "9"c, success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse '0'...'9' of Digit.")
        End If

        Return ch
    End Function

    Private Function ParseHexDigit(ByRef success As Boolean) As Char
        Dim errorCount As Integer = Errors.Count
        Dim ch As Char = MatchTerminalSet("0123456789ABCDEFabcdef", False, success)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse ""0123456789ABCDEFabcdef"" of HexDigit.")
        End If

        Return ch
    End Function

    Private Function ParseLetter(ByRef success As Boolean) As Char
        Dim errorCount As Integer = Errors.Count
        Dim ch As Char = ControlChars.NullChar

        ch = MatchTerminalRange("A"c, "Z"c, success)
        If success Then
            ClearError(errorCount)
            Return ch
        End If

        ch = MatchTerminalRange("a"c, "z"c, success)
        If success Then
            ClearError(errorCount)
            Return ch
        End If

        ch = MatchTerminal("_"c, success)
        If success Then
            ClearError(errorCount)
            Return ch
        End If

        Return ch
    End Function

    Private Function ParseUnaryOperator(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "UnaryOperator")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()

        Dim ch As Char = MatchTerminal("#"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("-"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Dim str As String = MatchTerminalString("not", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Return text.ToString()
    End Function

    Private Function ParseBinaryOperator(ByRef success As Boolean) As String
        Dim reskey = New Tuple(Of Integer, String)(position, "BinaryOperator")
        If ParsingResults.ContainsKey(reskey) Then
            Dim parsingResult = ParsingResults(reskey)
            success = parsingResult.Item2
            position = parsingResult.Item3
            Return TryCast(parsingResult.Item1, String)
        End If

        Dim errorCount As Integer = Errors.Count
        Dim text As New StringBuilder()

        Dim ch As Char = MatchTerminal("+"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("-"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("*"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("/"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("%"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("^"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Dim str As String = MatchTerminalString("..", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString("==", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString("~=", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString("<=", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString(">=", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal("<"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        ch = MatchTerminal(">"c, success)
        If success Then
            ClearError(errorCount)
            text.Append(ch)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString("and", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        str = MatchTerminalString("or", success)
        If success Then
            ClearError(errorCount)
            text.Append(str)
            ParsingResults(reskey) = New Tuple(Of Object, Boolean, Integer)(text.ToString(), success, position)
            Return text.ToString()
        End If

        Return text.ToString()
    End Function

    Private Sub ParseFieldSep(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        MatchTerminal(","c, success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminal(";"c, success)
        If success Then
            ClearError(errorCount)
            Return
        End If

    End Sub

    Private Sub ParseSpReq(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        Dim counter As Integer = 0
        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                MatchTerminalSet(" " & vbTab & vbCr & vbLf, False, success)
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                ParseComment(success)
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
            counter += 1
        End While
        If counter > 0 Then
            success = True
        End If
        If success Then
            ClearError(errorCount)
            Return
        End If

        While True
            Dim seq_start_position1 As Integer = position
            ParseSpOpt(success)

            ParseEof(success)
            If Not success Then
                [Error]("Failed to parse Eof of SpReq.")
                position = seq_start_position1
            End If
            Exit While
        End While
        If success Then
            ClearError(errorCount)
            Return
        End If

    End Sub

    Private Sub ParseSpOpt(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        While True
            ErrorStatck.Push(errorCount)
            errorCount = Errors.Count
            While True
                MatchTerminalSet(" " & vbTab & vbCr & vbLf, False, success)
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                ParseComment(success)
                If success Then
                    ClearError(errorCount)
                    Exit While
                End If

                Exit While
            End While
            errorCount = ErrorStatck.Pop()
            If Not success Then
                Exit While
            End If
        End While
        success = True
    End Sub

    Private Sub ParseComment(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        Dim start_position As Integer = position

        MatchTerminalString("--", success)
        If Not success Then
            position = start_position
            Return
        End If

        ErrorStatck.Push(errorCount)
        errorCount = Errors.Count
        While True
            ParseLongString(success)
            If success Then
                ClearError(errorCount)
                Exit While
            End If

            While True
                Dim seq_start_position1 As Integer = position
                While True
                    MatchTerminalSet(vbCr & vbLf, True, success)
                    If Not success Then
                        Exit While
                    End If
                End While
                success = True

                ErrorStatck.Push(errorCount)
                errorCount = Errors.Count
                While True
                    ParseEol(success)
                    If success Then
                        ClearError(errorCount)
                        Exit While
                    End If

                    ParseEof(success)
                    If success Then
                        ClearError(errorCount)
                        Exit While
                    End If

                    Exit While
                End While
                errorCount = ErrorStatck.Pop()
                If Not success Then
                    [Error]("Failed to parse (Eol / Eof) of Comment.")
                    position = seq_start_position1
                End If
                Exit While
            End While
            If success Then
                ClearError(errorCount)
                Exit While
            End If

            Exit While
        End While
        errorCount = ErrorStatck.Pop()
        If Not success Then
            [Error](String.Format("Failed to parse (LongString / (-""{0}{1}"")* (Eol / Eof)) of Comment.", vbCr, vbLf))
            position = start_position
        End If

        If success Then
            ClearError(errorCount)
        End If
    End Sub

    Private Sub ParseEol(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        MatchTerminalString(vbCr & vbLf, success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminal(ControlChars.Lf, success)
        If success Then
            ClearError(errorCount)
            Return
        End If

        MatchTerminal(ControlChars.Cr, success)
        If success Then
            ClearError(errorCount)
            Return
        End If

    End Sub

    Private Sub ParseEof(ByRef success As Boolean)
        Dim errorCount As Integer = Errors.Count
        success = Not Input.HasInput(position)
        If success Then
            ClearError(errorCount)
        Else
            [Error]("Failed to parse end of Eof.")
        End If
    End Sub

    Private Function ParseEscapeChar(ByRef success As Boolean) As Char
        'Dim errorCount As Integer = Errors.Count
        Dim ch As Char = ControlChars.NullChar

        MatchTerminalString("\\", success)
        If success Then
            Return "\"c
        End If

        MatchTerminalString("\'", success)
        If success Then
            Return "'"c
        End If

        MatchTerminalString("\""", success)
        If success Then
            Return """"c
        End If

        MatchTerminalString("\r", success)
        If success Then
            Return ControlChars.Cr
        End If

        MatchTerminalString("\n", success)
        If success Then
            Return ControlChars.Lf
        End If

        MatchTerminalString("\t", success)
        If success Then
            Return ControlChars.Tab
        End If

        MatchTerminalString("\v", success)
        If success Then
            Return ControlChars.VerticalTab
        End If

        MatchTerminalString("\a", success)
        If success Then
            Return ChrW(7)
        End If

        MatchTerminalString("\b", success)
        If success Then
            Return ControlChars.Back
        End If

        MatchTerminalString("\f", success)
        If success Then
            Return ControlChars.FormFeed
        End If

        MatchTerminalString("\0", success)
        If success Then
            Return ControlChars.NullChar
        End If

        Return ch
    End Function

End Class

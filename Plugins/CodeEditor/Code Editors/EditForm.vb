Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Alsing.SourceCode
Imports Alsing.Windows.Forms

''' <summary>
''' Summary description for EditForm.
''' </summary>
Public Class EditForm
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    Implements IExtendFramework.Text.ITextEditor

    WithEvents components As IContainer
    Public Doc As Document
    Public WithEvents sBox As SyntaxBoxControl
    Public WithEvents sDoc As SyntaxDocument
    WithEvents statusBar1 As StatusBar
    WithEvents statusBarPanel1 As StatusBarPanel
    WithEvents statusBarPanel2 As StatusBarPanel
    WithEvents statusBarPanel3 As StatusBarPanel
    WithEvents syntaxDocument1 As SyntaxDocument
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimeDateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowTABCharactersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowTabGuidesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowEOLMarkersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GotoLineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowScopeIndicatorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DocumentSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IntelliMouseControl1 As Alsing.Windows.Forms.CoreLib.IntelliMouseControl
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowFaoldingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    'create an EditForm and attach our opened document and tell the parser to use the given syntax.
    Public Sub New(ByVal title As String, ByVal path As String, ByVal documentText As String, ByVal SyntaxDefinition As SyntaxDefinition)
        InitializeComponent()

        Me.Doc = New Document(title, path, documentText)
        sBox.Document = sDoc
        sBox.Document.Parser.Init(SyntaxDefinition)
        sBox.Document.Text = Doc.Document
        Me.Text = "Notepad X - " & Doc.Title
    End Sub

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing) Then
            If (components IsNot Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'event fired when the caret moves

    'event fired when the modified flag of the document changes (eg if you undo every change , the modified flag will be reset
    Sub sDoc_ModifiedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles sDoc.ModifiedChanged
        Try
            Dim s As String = ""
            If (sDoc.Modified) Then
                s = " *"
            End If
            Me.Text = "Notepad X -  " & Doc.Title & s
            statusBarPanel1.Text = "Undo buffer :" & vbTab & sDoc.UndoStep
            'show number of steps in the undostack in one of the panels in the statusbar
        Catch ex As Exception
            'hmmm....
        End Try
    End Sub

    'event fired when the content is modified
    Sub sDoc_Change(ByVal sender As Object, ByVal e As EventArgs) Handles sDoc.Change
        statusBarPanel1.Text = "Undo buffer :" & vbTab & sDoc.UndoStep
    End Sub

    'save the content of the editor
    Public Sub SaveAs(ByVal FileName As String)
        Try
            Dim fs As New StreamWriter(FileName, False, Encoding.Default)
            fs.Write(sDoc.Text)
            fs.Flush()
            fs.Close()
        Catch x As Exception
            MessageBox.Show(x.Message)
        End Try
        sDoc.Modified = False
        Doc.Title = IO.Path.GetFileName(Doc.Path)
        Me.Text = "Notepad X - " & Doc.Title
    End Sub

    'occurs when a form is about to be closed
    Sub EditForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs) Handles Me.Closing
        If (sDoc.Modified) Then
            Dim res As DialogResult = MessageBox.Show(String.Format("Save changes to {0}?", Doc.Title), "Notepad X",
          MessageBoxButtons.YesNoCancel)
            If (res = DialogResult.Cancel) Then

                e.Cancel = True
                Return
            End If
            If (res = DialogResult.No) Then
                e.Cancel = False
                Return
            End If
            If (res = DialogResult.Yes) Then

                If (Doc.Path <> "") Then
                    SaveAs(Doc.Path)
                Else
                    Dim savedialog As New SaveFileDialog
                    If savedialog.ShowDialog() = DialogResult.OK Then
                        SaveAs(savedialog.FileName)
                    End If
                End If
                e.Cancel = False
                Return
            End If
        End If
    End Sub

#Region " Windows Form Designer generated code "

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditForm))
        Me.statusBar1 = New System.Windows.Forms.StatusBar()
        Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.statusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.statusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.sDoc = New Alsing.SourceCode.SyntaxDocument(Me.components)
        Me.sBox = New Alsing.Windows.Forms.SyntaxBoxControl()
        Me.syntaxDocument1 = New Alsing.SourceCode.SyntaxDocument(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimeDateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GotoLineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowTABCharactersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowTabGuidesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowFaoldingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowEOLMarkersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowScopeIndicatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.IntelliMouseControl1 = New Alsing.Windows.Forms.CoreLib.IntelliMouseControl()
        CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'statusBar1
        '
        Me.statusBar1.Location = New System.Drawing.Point(0, 471)
        Me.statusBar1.Name = "statusBar1"
        Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1, Me.statusBarPanel2, Me.statusBarPanel3})
        Me.statusBar1.ShowPanels = True
        Me.statusBar1.Size = New System.Drawing.Size(504, 22)
        Me.statusBar1.TabIndex = 1
        '
        'statusBarPanel1
        '
        Me.statusBarPanel1.Name = "statusBarPanel1"
        Me.statusBarPanel1.Width = 200
        '
        'statusBarPanel2
        '
        Me.statusBarPanel2.Name = "statusBarPanel2"
        Me.statusBarPanel2.Width = 200
        '
        'statusBarPanel3
        '
        Me.statusBarPanel3.Name = "statusBarPanel3"
        '
        'sDoc
        '
        Me.sDoc.Lines = New String() {"abc"}
        Me.sDoc.MaxUndoBufferSize = 1000
        Me.sDoc.Modified = False
        Me.sDoc.UndoStep = 0
        '
        'sBox
        '
        Me.sBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight
        Me.sBox.AutoListPosition = Nothing
        Me.sBox.AutoListSelectedText = "a123"
        Me.sBox.AutoListVisible = False
        Me.sBox.BackColor = System.Drawing.Color.White
        Me.sBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None
        Me.sBox.ChildBorderColor = System.Drawing.Color.White
        Me.sBox.ChildBorderStyle = Alsing.Windows.Forms.BorderStyle.None
        Me.sBox.CopyAsRTF = True
        Me.sBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sBox.Document = Me.sDoc
        Me.sBox.FontName = "Courier new"
        Me.sBox.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sBox.InfoTipCount = 1
        Me.sBox.InfoTipPosition = Nothing
        Me.sBox.InfoTipSelectedIndex = 1
        Me.sBox.InfoTipVisible = False
        Me.sBox.Location = New System.Drawing.Point(0, 24)
        Me.sBox.LockCursorUpdate = False
        Me.sBox.Name = "sBox"
        Me.sBox.ScopeIndicatorColor = System.Drawing.Color.Black
        Me.sBox.Size = New System.Drawing.Size(504, 447)
        Me.sBox.SmoothScroll = False
        Me.sBox.SplitviewH = -4
        Me.sBox.SplitviewV = -4
        Me.sBox.TabGuideColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.sBox.TabIndex = 3
        Me.sBox.Text = "syntaxBoxControl1"
        Me.sBox.TooltipDelay = 100
        Me.sBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark
        '
        'syntaxDocument1
        '
        Me.syntaxDocument1.Lines = New String() {""}
        Me.syntaxDocument1.MaxUndoBufferSize = 1000
        Me.syntaxDocument1.Modified = False
        Me.syntaxDocument1.UndoStep = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ExitToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(504, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripSeparator1, Me.PrintSetupToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.OpenToolStripMenuItem.Text = "&Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(183, 6)
        '
        'PrintSetupToolStripMenuItem
        '
        Me.PrintSetupToolStripMenuItem.Name = "PrintSetupToolStripMenuItem"
        Me.PrintSetupToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.PrintSetupToolStripMenuItem.Text = "Print Preview"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.PrintToolStripMenuItem.Text = "&Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(183, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.ToolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripSeparator5, Me.SelectAllToolStripMenuItem, Me.TimeDateToolStripMenuItem, Me.GotoLineToolStripMenuItem, Me.ToolStripSeparator6, Me.FindToolStripMenuItem, Me.ReplaceToolStripMenuItem, Me.ToolStripSeparator4, Me.ShowTABCharactersToolStripMenuItem, Me.ShowTabGuidesToolStripMenuItem, Me.ShowFaoldingToolStripMenuItem, Me.ShowEOLMarkersToolStripMenuItem, Me.ShowScopeIndicatorToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.UndoToolStripMenuItem.Text = "Undo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(185, 6)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(185, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        '
        'TimeDateToolStripMenuItem
        '
        Me.TimeDateToolStripMenuItem.Name = "TimeDateToolStripMenuItem"
        Me.TimeDateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.TimeDateToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.TimeDateToolStripMenuItem.Text = "Time/Date"
        '
        'GotoLineToolStripMenuItem
        '
        Me.GotoLineToolStripMenuItem.Name = "GotoLineToolStripMenuItem"
        Me.GotoLineToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.GotoLineToolStripMenuItem.Text = "Goto Line"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(185, 6)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.FindToolStripMenuItem.Text = "Find"
        '
        'ReplaceToolStripMenuItem
        '
        Me.ReplaceToolStripMenuItem.Name = "ReplaceToolStripMenuItem"
        Me.ReplaceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ReplaceToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ReplaceToolStripMenuItem.Text = "Replace"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(185, 6)
        '
        'ShowTABCharactersToolStripMenuItem
        '
        Me.ShowTABCharactersToolStripMenuItem.Name = "ShowTABCharactersToolStripMenuItem"
        Me.ShowTABCharactersToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowTABCharactersToolStripMenuItem.Text = "Show TAB Characters"
        '
        'ShowTabGuidesToolStripMenuItem
        '
        Me.ShowTabGuidesToolStripMenuItem.Name = "ShowTabGuidesToolStripMenuItem"
        Me.ShowTabGuidesToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowTabGuidesToolStripMenuItem.Text = "Show Tab Guides"
        '
        'ShowFaoldingToolStripMenuItem
        '
        Me.ShowFaoldingToolStripMenuItem.Checked = True
        Me.ShowFaoldingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowFaoldingToolStripMenuItem.Name = "ShowFaoldingToolStripMenuItem"
        Me.ShowFaoldingToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowFaoldingToolStripMenuItem.Text = "Show Folding"
        '
        'ShowEOLMarkersToolStripMenuItem
        '
        Me.ShowEOLMarkersToolStripMenuItem.Name = "ShowEOLMarkersToolStripMenuItem"
        Me.ShowEOLMarkersToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowEOLMarkersToolStripMenuItem.Text = "Show EOL Markers"
        '
        'ShowScopeIndicatorToolStripMenuItem
        '
        Me.ShowScopeIndicatorToolStripMenuItem.Name = "ShowScopeIndicatorToolStripMenuItem"
        Me.ShowScopeIndicatorToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ShowScopeIndicatorToolStripMenuItem.Text = "Show Scope Indicator"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentSettingsToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        Me.ToolsToolStripMenuItem.Visible = False
        '
        'DocumentSettingsToolStripMenuItem
        '
        Me.DocumentSettingsToolStripMenuItem.Name = "DocumentSettingsToolStripMenuItem"
        Me.DocumentSettingsToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.DocumentSettingsToolStripMenuItem.Text = "Document Settings"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'IntelliMouseControl1
        '
        Me.IntelliMouseControl1.Image = Nothing
        Me.IntelliMouseControl1.Location = New System.Drawing.Point(152, 117)
        Me.IntelliMouseControl1.Name = "IntelliMouseControl1"
        Me.IntelliMouseControl1.Size = New System.Drawing.Size(32, 32)
        Me.IntelliMouseControl1.TabIndex = 5
        Me.IntelliMouseControl1.Text = "IntelliMouseControl1"
        Me.IntelliMouseControl1.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'EditForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 493)
        Me.Controls.Add(Me.IntelliMouseControl1)
        Me.Controls.Add(Me.sBox)
        Me.Controls.Add(Me.statusBar1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EditForm"
        Me.Text = "Edit form"
        CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.statusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.statusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        sBox.Text = ""
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        sBox.Undo()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        sBox.Cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        sBox.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        sBox.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        sBox.SelectAll()
    End Sub

    Private Sub TimeDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeDateToolStripMenuItem.Click
        'sBox.Caret.Position 
        Dim pos As TextPoint = sBox.Caret.Position
        sDoc.InsertText(DateTime.Now.ToString, pos.X, pos.Y)
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        sBox.ShowFind()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim o As New OpenFileDialog
        If o.ShowDialog = DialogResult.OK Then
            sBox.Text = IO.File.ReadAllText(o.FileName)
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If Doc.Path = "" Then
            Dim s As New SaveFileDialog
            If s.ShowDialog = DialogResult.OK Then
                Doc.Path = s.FileName
            End If
        End If
        SaveAs(Doc.Path)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        Doc.Path = ""
        SaveToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PrintSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSetupToolStripMenuItem.Click
        Dim pd = New SourceCodePrintDocument(sDoc)
        Dim dlgPrintPreview As New PrintPreviewDialog
        dlgPrintPreview.Document = pd
        dlgPrintPreview.ShowDialog(Me)
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Dim pd = New SourceCodePrintDocument(sDoc)
        Dim dlgPrint As New PrintDialog
        dlgPrint.Document = pd
        If (dlgPrint.ShowDialog(Me) = DialogResult.OK) Then
            pd.Print()
        End If
    End Sub

    Private Sub ShowTABCharactersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTABCharactersToolStripMenuItem.Click
        sBox.ShowWhitespace = Not sBox.ShowWhitespace
        ShowTABCharactersToolStripMenuItem.Checked = sBox.ShowWhitespace
    End Sub

    Private Sub ShowTabGuidesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTabGuidesToolStripMenuItem.Click
        sBox.ShowTabGuides = Not sBox.ShowTabGuides
        ShowTabGuidesToolStripMenuItem.Checked = sBox.ShowTabGuides
    End Sub

    Private Sub ShowFaoldingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFaoldingToolStripMenuItem.Click
        sDoc.Folding = Not sDoc.Folding
        ShowFaoldingToolStripMenuItem.Checked = sDoc.Folding
    End Sub

    Private Sub ShowEOLMarkersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowEOLMarkersToolStripMenuItem.Click
        sBox.ShowEOLMarker = Not sBox.ShowEOLMarker
        ShowEOLMarkersToolStripMenuItem.Checked = sBox.ShowEOLMarker
    End Sub

    Private Sub GotoLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoLineToolStripMenuItem.Click
        sBox.ShowGotoLine()
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceToolStripMenuItem.Click
        sBox.ShowReplace()
    End Sub

    Private Sub ShowScopeIndicatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowScopeIndicatorToolStripMenuItem.Click
        sBox.ShowScopeIndicator = Not sBox.ShowScopeIndicator
        ShowScopeIndicatorToolStripMenuItem.Checked = sBox.ShowScopeIndicator
    End Sub

    Private Sub EditForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MenuStrip1.SendToBack()
    End Sub

    Private Sub DocumentSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentSettingsToolStripMenuItem.Click
        'Dim settings As New SettingsForm(sBox)
        'settings.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Public ReadOnly Property DockingPanel() As WeifenLuo.WinFormsUI.Docking.DockContent Implements IExtendFramework.Text.ITextEditor.DockingPanel
        Get
            Return Me
        End Get
    End Property
    
    Public ReadOnly Property Extension() As IExtendFramework.Text.IFileExtension Implements IExtendFramework.Text.ITextEditor.Extension
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    
    Public Property Filename() As String Implements IExtendFramework.Text.ITextEditor.Filename
        Get
            Throw New NotImplementedException()
        End Get
        Set
            Throw New NotImplementedException()
        End Set
    End Property
    
    Public ReadOnly Property CurrentDocument() As IExtendFramework.Text.IDocument Implements IExtendFramework.Text.ITextEditor.CurrentDocument
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    
    Public ReadOnly Property UndoBuffer() As Integer Implements IExtendFramework.Text.ITextEditor.UndoBuffer
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    
    Public Function Create(FileName As String) As IExtendFramework.Text.ITextEditor Implements IExtendFramework.Text.ITextEditor.Create
        Throw New NotImplementedException()
    End Function
    
    Public Sub Undo() Implements IExtendFramework.Text.ITextEditor.Undo
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Redo() Implements IExtendFramework.Text.ITextEditor.Redo
        Throw New NotImplementedException()
    End Sub
    
    Public Sub SaveAs() Implements IExtendFramework.Text.ITextEditor.SaveAs
        Throw New NotImplementedException()
    End Sub
    
    Public Sub PrintPreview() Implements IExtendFramework.Text.ITextEditor.PrintPreview
        Throw New NotImplementedException()
    End Sub
    
    Public Sub PrintSetup() Implements IExtendFramework.Text.ITextEditor.PrintSetup
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Cut() Implements IExtendFramework.Text.ITextEditor.Cut
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Copy() Implements IExtendFramework.Text.ITextEditor.Copy
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Paste() Implements IExtendFramework.Text.ITextEditor.Paste
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Insert(index As Integer, text As String) Implements IExtendFramework.Text.ITextEditor.Insert
        Throw New NotImplementedException()
    End Sub
    
    Public Sub ChangeFont(newFont As System.Drawing.Font) Implements IExtendFramework.Text.ITextEditor.ChangeFont
        Throw New NotImplementedException()
    End Sub
    
    Public Sub ChangeColor(newColor As System.Drawing.Color) Implements IExtendFramework.Text.ITextEditor.ChangeColor
        Throw New NotImplementedException()
    End Sub
    
    Public Sub Open(filename As String) Implements IExtendFramework.Text.ITextEditor.Open
        Throw New NotImplementedException()
    End Sub
    
    Public Sub SelectAll() Implements IExtendFramework.Text.ITextEditor.SelectAll
        Throw New NotImplementedException()
    End Sub
End Class
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Alsing.SourceCode

''' <summary>
''' Summary description for LanguageForm.
''' </summary>
Public Class LanguageForm
    Inherits Form

    WithEvents btnCancel As Button
    WithEvents btnOK As Button
    WithEvents components As IContainer
    Public EditForm As editForm
    WithEvents imlIcons As ImageList
    WithEvents lvFileTypes As ListView
    WithEvents trvFileTypes As TreeView

    Public Sub New()
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()
        '
        ' TODO: Add any constructor code after InitializeComponent call
        '
    End Sub

    Public Sub New(ByVal LangList As CodeEditor.SyntaxDefinitionList)

        InitializeComponent()
        trvFileTypes.Nodes.Clear()
        For Each syntax As SyntaxDefinition In LangList.GetSyntaxDefinitions()
            Dim tn As TreeNode = trvFileTypes.Nodes.Add(syntax.Name)
            tn.Tag = syntax
        Next
        trvFileTypes.SelectedNode = trvFileTypes.Nodes(0)
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

    Private Sub LanguageForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        OK()
    End Sub

    Private Sub trvFileTypes_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles trvFileTypes.AfterSelect

        Dim syntax = CType(e.Node.Tag, SyntaxDefinition)
        lvFileTypes.Items.Clear()
        For Each ft In syntax.FileTypes
            Dim lvi As ListViewItem = lvFileTypes.Items.Add(String.Format("{0}   ({1})", ft.Name, ft.Extension))
            lvi.Tag = ft
            lvi.ImageIndex = 0
        Next
    End Sub

    Private Sub OK()
        If (lvFileTypes.SelectedItems.Count = 0) Then
            lvFileTypes.Items(0).Selected = True
        End If

        Dim syntax As SyntaxDefinition = CType(trvFileTypes.SelectedNode.Tag, SyntaxDefinition)
        Dim ft = CType(lvFileTypes.SelectedItems(0).Tag, FileType)
        Dim doc = New Document("", "", "")
        doc.Title = ("Untitled" + ft.Extension)
        doc.Path = NotepadX.Main.DefaultSaveLocation & "\"
        Dim DocName As String = String.Empty
        While String.IsNullOrEmpty(DocName)
            DocName = InputBox("Document Name (cannot be empty)")
            Dim found As Boolean = False
            For Each illegalChar In IO.Path.GetInvalidPathChars
                If DocName.Contains(illegalChar) Then found = True
            Next
            If DocName.Contains("\") Or DocName.Contains("/") _
                Or DocName.Contains(":") Then found = True
            If found Then
                MsgBox("Document Name contains illegal character!")
                DocName = ""
            End If
        End While
        doc.Document = ""
        doc.Path = doc.Path & DocName & ft.Extension
        doc.Title = doc.Path
        Dim ef As New EditForm(doc.Title, doc.Path, doc.Document, syntax)
        EditForm = ef
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lvFileTypes_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvFileTypes.DoubleClick
        OK()
    End Sub

#Region " Windows Form Designer generated code "

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of me method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LanguageForm))
        Me.lvFileTypes = New System.Windows.Forms.ListView()
        Me.imlIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.trvFileTypes = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'lvFileTypes
        '
        Me.lvFileTypes.HideSelection = False
        Me.lvFileTypes.LargeImageList = Me.imlIcons
        Me.lvFileTypes.Location = New System.Drawing.Point(176, 8)
        Me.lvFileTypes.Name = "lvFileTypes"
        Me.lvFileTypes.Size = New System.Drawing.Size(272, 345)
        Me.lvFileTypes.SmallImageList = Me.imlIcons
        Me.lvFileTypes.TabIndex = 0
        Me.lvFileTypes.UseCompatibleStateImageBehavior = False
        '
        'imlIcons
        '
        Me.imlIcons.ImageStream = CType(resources.GetObject("imlIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.imlIcons.Images.SetKeyName(0, "")
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(285, 359)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(373, 359)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'trvFileTypes
        '
        Me.trvFileTypes.HideSelection = False
        Me.trvFileTypes.Location = New System.Drawing.Point(8, 8)
        Me.trvFileTypes.Name = "trvFileTypes"
        Me.trvFileTypes.Size = New System.Drawing.Size(168, 345)
        Me.trvFileTypes.TabIndex = 3
        '
        'LanguageForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(458, 394)
        Me.ControlBox = False
        Me.Controls.Add(Me.trvFileTypes)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lvFileTypes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "LanguageForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select a Language"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
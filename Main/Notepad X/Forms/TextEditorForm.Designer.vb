<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextEditor
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    	Me.components = New System.ComponentModel.Container()
    	Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TextEditor))
    	Me.rightClickContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
    	Me.UndoToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.SelectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
    	Me.CopyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.CutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.PasteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
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
    	Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
    	Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ASCIIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.XorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.RijndaelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DESToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.TripleDESToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.AESToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.RC2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.RSAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.DecryptyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.OpenFileToReadBytecodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.SaveFileAsBytecodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ViewEncryptionSamplesFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.TextFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.EncryptedTextFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.PDFFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.MacrosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.FormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.currentFileStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
    	Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
    	Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    	Me.totalCharactersStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
    	Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
    	Me.selectedCharactersCountStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
    	Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
    	Me.FontDialog1 = New System.Windows.Forms.FontDialog()
    	Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
    	Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    	Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
    	Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
    	Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
    	Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
    	Me.notifyIconContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
    	Me.BringToFrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    	Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.SaveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.PrintToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    	Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
    	Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
    	Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
    	Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
    	Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
    	Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
    	Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
    	Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
    	Me.AutoSaveTimer = New System.Windows.Forms.Timer(Me.components)
    	Me.timeBackgroundWorker = New System.ComponentModel.BackgroundWorker()
    	Me.TextBox1 = New System.Windows.Forms.RichTextBox()
    	Me.rightClickContextMenuStrip.SuspendLayout
    	Me.MenuStrip1.SuspendLayout
    	Me.StatusStrip1.SuspendLayout
    	Me.notifyIconContextMenu.SuspendLayout
    	Me.ToolStrip1.SuspendLayout
    	Me.SuspendLayout
    	'
    	'rightClickContextMenuStrip
    	'
    	Me.rightClickContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem1, Me.SelectAllToolStripMenuItem1, Me.ToolStripSeparator4, Me.CopyToolStripMenuItem1, Me.CutToolStripMenuItem1, Me.PasteToolStripMenuItem1})
    	Me.rightClickContextMenuStrip.Name = "rightClickContextMenuStrip"
    	Me.rightClickContextMenuStrip.Size = New System.Drawing.Size(123, 120)
    	'
    	'UndoToolStripMenuItem1
    	'
    	Me.UndoToolStripMenuItem1.Name = "UndoToolStripMenuItem1"
    	Me.UndoToolStripMenuItem1.Size = New System.Drawing.Size(122, 22)
    	Me.UndoToolStripMenuItem1.Text = "Undo"
    	'
    	'SelectAllToolStripMenuItem1
    	'
    	Me.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1"
    	Me.SelectAllToolStripMenuItem1.Size = New System.Drawing.Size(122, 22)
    	Me.SelectAllToolStripMenuItem1.Text = "Select All"
    	'
    	'ToolStripSeparator4
    	'
    	Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
    	Me.ToolStripSeparator4.Size = New System.Drawing.Size(119, 6)
    	'
    	'CopyToolStripMenuItem1
    	'
    	Me.CopyToolStripMenuItem1.Name = "CopyToolStripMenuItem1"
    	Me.CopyToolStripMenuItem1.Size = New System.Drawing.Size(122, 22)
    	Me.CopyToolStripMenuItem1.Text = "Copy"
    	'
    	'CutToolStripMenuItem1
    	'
    	Me.CutToolStripMenuItem1.Name = "CutToolStripMenuItem1"
    	Me.CutToolStripMenuItem1.Size = New System.Drawing.Size(122, 22)
    	Me.CutToolStripMenuItem1.Text = "Cut"
    	'
    	'PasteToolStripMenuItem1
    	'
    	Me.PasteToolStripMenuItem1.Name = "PasteToolStripMenuItem1"
    	Me.PasteToolStripMenuItem1.Size = New System.Drawing.Size(122, 22)
    	Me.PasteToolStripMenuItem1.Text = "Paste"
    	'
    	'MenuStrip1
    	'
    	Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.FormatToolStripMenuItem})
    	Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    	Me.MenuStrip1.Name = "MenuStrip1"
    	Me.MenuStrip1.Size = New System.Drawing.Size(610, 24)
    	Me.MenuStrip1.TabIndex = 1
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
    	Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift)  _
    	    	    	Or System.Windows.Forms.Keys.S),System.Windows.Forms.Keys)
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
    	Me.PrintSetupToolStripMenuItem.Text = "Print Setup"
    	'
    	'PrintToolStripMenuItem
    	'
    	Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
    	Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
    	Me.PrintToolStripMenuItem.Text = "&Print"
    	AddHandler Me.PrintToolStripMenuItem.Click, AddressOf Me.PrintToolStripMenuItem_Click
    	'
    	'ToolStripSeparator2
    	'
    	Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    	Me.ToolStripSeparator2.Size = New System.Drawing.Size(183, 6)
    	'
    	'ExitToolStripMenuItem
    	'
    	Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
    	Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4),System.Windows.Forms.Keys)
    	Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
    	Me.ExitToolStripMenuItem.Text = "E&xit"
    	'
    	'EditToolStripMenuItem
    	'
    	Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.ToolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripSeparator5, Me.SelectAllToolStripMenuItem, Me.TimeDateToolStripMenuItem, Me.ToolStripSeparator9, Me.FindToolStripMenuItem, Me.ReplaceToolStripMenuItem})
    	Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
    	Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
    	Me.EditToolStripMenuItem.Text = "&Edit"
    	'
    	'UndoToolStripMenuItem
    	'
    	Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
    	Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z),System.Windows.Forms.Keys)
    	Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.UndoToolStripMenuItem.Text = "Undo"
    	'
    	'ToolStripSeparator3
    	'
    	Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
    	Me.ToolStripSeparator3.Size = New System.Drawing.Size(161, 6)
    	'
    	'CutToolStripMenuItem
    	'
    	Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
    	Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X),System.Windows.Forms.Keys)
    	Me.CutToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.CutToolStripMenuItem.Text = "Cut"
    	'
    	'CopyToolStripMenuItem
    	'
    	Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
    	Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
    	Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.CopyToolStripMenuItem.Text = "Copy"
    	'
    	'PasteToolStripMenuItem
    	'
    	Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
    	Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V),System.Windows.Forms.Keys)
    	Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.PasteToolStripMenuItem.Text = "Paste"
    	'
    	'ToolStripSeparator5
    	'
    	Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
    	Me.ToolStripSeparator5.Size = New System.Drawing.Size(161, 6)
    	'
    	'SelectAllToolStripMenuItem
    	'
    	Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
    	Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A),System.Windows.Forms.Keys)
    	Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.SelectAllToolStripMenuItem.Text = "Select All"
    	'
    	'TimeDateToolStripMenuItem
    	'
    	Me.TimeDateToolStripMenuItem.Name = "TimeDateToolStripMenuItem"
    	Me.TimeDateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
    	Me.TimeDateToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.TimeDateToolStripMenuItem.Text = "Time/Date"
    	'
    	'ToolStripSeparator9
    	'
    	Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
    	Me.ToolStripSeparator9.Size = New System.Drawing.Size(161, 6)
    	'
    	'FindToolStripMenuItem
    	'
    	Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
    	Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F),System.Windows.Forms.Keys)
    	Me.FindToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.FindToolStripMenuItem.Text = "Find"
    	'
    	'ReplaceToolStripMenuItem
    	'
    	Me.ReplaceToolStripMenuItem.Name = "ReplaceToolStripMenuItem"
    	Me.ReplaceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R),System.Windows.Forms.Keys)
    	Me.ReplaceToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    	Me.ReplaceToolStripMenuItem.Text = "Replace"
    	'
    	'ToolStripMenuItem1
    	'
    	Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ASCIIToolStripMenuItem, Me.XorToolStripMenuItem, Me.RijndaelToolStripMenuItem, Me.DESToolStripMenuItem, Me.TripleDESToolStripMenuItem, Me.AESToolStripMenuItem, Me.RC2ToolStripMenuItem, Me.RSAToolStripMenuItem})
    	Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    	Me.ToolStripMenuItem1.Size = New System.Drawing.Size(76, 20)
    	Me.ToolStripMenuItem1.Text = "En&cryption"
    	'
    	'ASCIIToolStripMenuItem
    	'
    	Me.ASCIIToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem, Me.DecryptToolStripMenuItem})
    	Me.ASCIIToolStripMenuItem.Name = "ASCIIToolStripMenuItem"
    	Me.ASCIIToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.ASCIIToolStripMenuItem.Text = "ASCII"
    	'
    	'EncryptToolStripMenuItem
    	'
    	Me.EncryptToolStripMenuItem.Name = "EncryptToolStripMenuItem"
    	Me.EncryptToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem
    	'
    	Me.DecryptToolStripMenuItem.Name = "DecryptToolStripMenuItem"
    	Me.DecryptToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem.Text = "Decrypt"
    	'
    	'XorToolStripMenuItem
    	'
    	Me.XorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem1, Me.DecryptToolStripMenuItem1})
    	Me.XorToolStripMenuItem.Name = "XorToolStripMenuItem"
    	Me.XorToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.XorToolStripMenuItem.Text = "Xor"
    	'
    	'EncryptToolStripMenuItem1
    	'
    	Me.EncryptToolStripMenuItem1.Name = "EncryptToolStripMenuItem1"
    	Me.EncryptToolStripMenuItem1.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem1.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem1
    	'
    	Me.DecryptToolStripMenuItem1.Name = "DecryptToolStripMenuItem1"
    	Me.DecryptToolStripMenuItem1.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem1.Text = "Decrypt"
    	'
    	'RijndaelToolStripMenuItem
    	'
    	Me.RijndaelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem2, Me.DecryptToolStripMenuItem2})
    	Me.RijndaelToolStripMenuItem.Name = "RijndaelToolStripMenuItem"
    	Me.RijndaelToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.RijndaelToolStripMenuItem.Text = "Rijndael"
    	'
    	'EncryptToolStripMenuItem2
    	'
    	Me.EncryptToolStripMenuItem2.Name = "EncryptToolStripMenuItem2"
    	Me.EncryptToolStripMenuItem2.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem2.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem2
    	'
    	Me.DecryptToolStripMenuItem2.Name = "DecryptToolStripMenuItem2"
    	Me.DecryptToolStripMenuItem2.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem2.Text = "Decrypt"
    	'
    	'DESToolStripMenuItem
    	'
    	Me.DESToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem3, Me.DecryptToolStripMenuItem3})
    	Me.DESToolStripMenuItem.Name = "DESToolStripMenuItem"
    	Me.DESToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.DESToolStripMenuItem.Text = "DES"
    	'
    	'EncryptToolStripMenuItem3
    	'
    	Me.EncryptToolStripMenuItem3.Name = "EncryptToolStripMenuItem3"
    	Me.EncryptToolStripMenuItem3.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem3.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem3
    	'
    	Me.DecryptToolStripMenuItem3.Name = "DecryptToolStripMenuItem3"
    	Me.DecryptToolStripMenuItem3.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem3.Text = "Decrypt"
    	'
    	'TripleDESToolStripMenuItem
    	'
    	Me.TripleDESToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem4, Me.DecryptToolStripMenuItem4})
    	Me.TripleDESToolStripMenuItem.Name = "TripleDESToolStripMenuItem"
    	Me.TripleDESToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.TripleDESToolStripMenuItem.Text = "Triple DES"
    	'
    	'EncryptToolStripMenuItem4
    	'
    	Me.EncryptToolStripMenuItem4.Name = "EncryptToolStripMenuItem4"
    	Me.EncryptToolStripMenuItem4.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem4.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem4
    	'
    	Me.DecryptToolStripMenuItem4.Name = "DecryptToolStripMenuItem4"
    	Me.DecryptToolStripMenuItem4.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem4.Text = "Decrypt"
    	'
    	'AESToolStripMenuItem
    	'
    	Me.AESToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem5, Me.DecryptToolStripMenuItem5})
    	Me.AESToolStripMenuItem.Name = "AESToolStripMenuItem"
    	Me.AESToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.AESToolStripMenuItem.Text = "AES"
    	'
    	'EncryptToolStripMenuItem5
    	'
    	Me.EncryptToolStripMenuItem5.Name = "EncryptToolStripMenuItem5"
    	Me.EncryptToolStripMenuItem5.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem5.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem5
    	'
    	Me.DecryptToolStripMenuItem5.Name = "DecryptToolStripMenuItem5"
    	Me.DecryptToolStripMenuItem5.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem5.Text = "Decrypt"
    	'
    	'RC2ToolStripMenuItem
    	'
    	Me.RC2ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem6, Me.DecryptToolStripMenuItem6})
    	Me.RC2ToolStripMenuItem.Name = "RC2ToolStripMenuItem"
    	Me.RC2ToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.RC2ToolStripMenuItem.Text = "RC2"
    	'
    	'EncryptToolStripMenuItem6
    	'
    	Me.EncryptToolStripMenuItem6.Name = "EncryptToolStripMenuItem6"
    	Me.EncryptToolStripMenuItem6.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem6.Text = "Encrypt"
    	'
    	'DecryptToolStripMenuItem6
    	'
    	Me.DecryptToolStripMenuItem6.Name = "DecryptToolStripMenuItem6"
    	Me.DecryptToolStripMenuItem6.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptToolStripMenuItem6.Text = "Decrypt"
    	'
    	'RSAToolStripMenuItem
    	'
    	Me.RSAToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EncryptToolStripMenuItem7, Me.DecryptyToolStripMenuItem})
    	Me.RSAToolStripMenuItem.Name = "RSAToolStripMenuItem"
    	Me.RSAToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    	Me.RSAToolStripMenuItem.Text = "RSA"
    	'
    	'EncryptToolStripMenuItem7
    	'
    	Me.EncryptToolStripMenuItem7.Name = "EncryptToolStripMenuItem7"
    	Me.EncryptToolStripMenuItem7.Size = New System.Drawing.Size(115, 22)
    	Me.EncryptToolStripMenuItem7.Text = "Encrypt"
    	'
    	'DecryptyToolStripMenuItem
    	'
    	Me.DecryptyToolStripMenuItem.Name = "DecryptyToolStripMenuItem"
    	Me.DecryptyToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
    	Me.DecryptyToolStripMenuItem.Text = "Decrypt"
    	'
    	'ToolStripMenuItem2
    	'
    	Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenFileToReadBytecodeToolStripMenuItem, Me.SaveFileAsBytecodeToolStripMenuItem, Me.ViewEncryptionSamplesFormToolStripMenuItem, Me.ExportToolStripMenuItem, Me.MacrosToolStripMenuItem})
    	Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    	Me.ToolStripMenuItem2.Size = New System.Drawing.Size(48, 20)
    	Me.ToolStripMenuItem2.Text = "&Tools"
    	'
    	'OpenFileToReadBytecodeToolStripMenuItem
    	'
    	Me.OpenFileToReadBytecodeToolStripMenuItem.Name = "OpenFileToReadBytecodeToolStripMenuItem"
    	Me.OpenFileToReadBytecodeToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
    	Me.OpenFileToReadBytecodeToolStripMenuItem.Text = "O&pen File to Read Bytecode"
    	'
    	'SaveFileAsBytecodeToolStripMenuItem
    	'
    	Me.SaveFileAsBytecodeToolStripMenuItem.Name = "SaveFileAsBytecodeToolStripMenuItem"
    	Me.SaveFileAsBytecodeToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
    	Me.SaveFileAsBytecodeToolStripMenuItem.Text = "&Save File as Bytecode"
    	'
    	'ViewEncryptionSamplesFormToolStripMenuItem
    	'
    	Me.ViewEncryptionSamplesFormToolStripMenuItem.Name = "ViewEncryptionSamplesFormToolStripMenuItem"
    	Me.ViewEncryptionSamplesFormToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
    	Me.ViewEncryptionSamplesFormToolStripMenuItem.Text = "&View Encryption Samples Form"
    	'
    	'ExportToolStripMenuItem
    	'
    	Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextFileToolStripMenuItem, Me.EncryptedTextFileToolStripMenuItem, Me.PDFFileToolStripMenuItem})
    	Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
    	Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
    	Me.ExportToolStripMenuItem.Text = "Export"
    	'
    	'TextFileToolStripMenuItem
    	'
    	Me.TextFileToolStripMenuItem.Name = "TextFileToolStripMenuItem"
    	Me.TextFileToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    	Me.TextFileToolStripMenuItem.Text = "Text File"
    	'
    	'EncryptedTextFileToolStripMenuItem
    	'
    	Me.EncryptedTextFileToolStripMenuItem.Name = "EncryptedTextFileToolStripMenuItem"
    	Me.EncryptedTextFileToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    	Me.EncryptedTextFileToolStripMenuItem.Text = "Encrypted Text File"
    	'
    	'PDFFileToolStripMenuItem
    	'
    	Me.PDFFileToolStripMenuItem.Name = "PDFFileToolStripMenuItem"
    	Me.PDFFileToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
    	Me.PDFFileToolStripMenuItem.Text = "PDF File"
    	'
    	'MacrosToolStripMenuItem
    	'
    	Me.MacrosToolStripMenuItem.Name = "MacrosToolStripMenuItem"
    	Me.MacrosToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
    	Me.MacrosToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
    	Me.MacrosToolStripMenuItem.Text = "Macros"
    	'
    	'FormatToolStripMenuItem
    	'
    	Me.FormatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3, Me.FontToolStripMenuItem, Me.ColorToolStripMenuItem})
    	Me.FormatToolStripMenuItem.Name = "FormatToolStripMenuItem"
    	Me.FormatToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
    	Me.FormatToolStripMenuItem.Text = "F&ormat"
    	'
    	'ToolStripMenuItem3
    	'
    	Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    	Me.ToolStripMenuItem3.Size = New System.Drawing.Size(162, 22)
    	Me.ToolStripMenuItem3.Text = "&Word Wrap (Off)"
    	'
    	'FontToolStripMenuItem
    	'
    	Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
    	Me.FontToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
    	Me.FontToolStripMenuItem.Text = "&Font"
    	'
    	'ColorToolStripMenuItem
    	'
    	Me.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem"
    	Me.ColorToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
    	Me.ColorToolStripMenuItem.Text = "&Color"
    	'
    	'currentFileStatusLabel
    	'
    	Me.currentFileStatusLabel.Name = "currentFileStatusLabel"
    	Me.currentFileStatusLabel.Size = New System.Drawing.Size(119, 17)
    	Me.currentFileStatusLabel.Text = "Current File: Untitled "
    	'
    	'ToolStripStatusLabel1
    	'
    	Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
    	Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(10, 17)
    	Me.ToolStripStatusLabel1.Text = "|"
    	'
    	'StatusStrip1
    	'
    	Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.currentFileStatusLabel, Me.ToolStripStatusLabel1, Me.totalCharactersStatusLabel, Me.ToolStripStatusLabel2, Me.selectedCharactersCountStatusLabel})
    	Me.StatusStrip1.Location = New System.Drawing.Point(0, 362)
    	Me.StatusStrip1.Name = "StatusStrip1"
    	Me.StatusStrip1.ShowItemToolTips = true
    	Me.StatusStrip1.Size = New System.Drawing.Size(610, 22)
    	Me.StatusStrip1.TabIndex = 2
    	Me.StatusStrip1.Text = "StatusStrip1"
    	'
    	'totalCharactersStatusLabel
    	'
    	Me.totalCharactersStatusLabel.Name = "totalCharactersStatusLabel"
    	Me.totalCharactersStatusLabel.Size = New System.Drawing.Size(127, 17)
    	Me.totalCharactersStatusLabel.Text = "Characters: 0. Words: 0"
    	'
    	'ToolStripStatusLabel2
    	'
    	Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
    	Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(10, 17)
    	Me.ToolStripStatusLabel2.Text = "|"
    	'
    	'selectedCharactersCountStatusLabel
    	'
    	Me.selectedCharactersCountStatusLabel.Name = "selectedCharactersCountStatusLabel"
    	Me.selectedCharactersCountStatusLabel.Size = New System.Drawing.Size(122, 17)
    	Me.selectedCharactersCountStatusLabel.Text = "Selected Characters: 0"
    	'
    	'OpenFileDialog1
    	'
    	Me.OpenFileDialog1.FileName = "OpenFileDialog1"
    	'
    	'PrintDialog1
    	'
    	Me.PrintDialog1.UseEXDialog = true
    	'
    	'NotifyIcon1
    	'
    	Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
    	Me.NotifyIcon1.BalloonTipText = "Notepad X Is Runing"
    	Me.NotifyIcon1.BalloonTipTitle = "Notepad X"
    	Me.NotifyIcon1.ContextMenuStrip = Me.notifyIconContextMenu
    	Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"),System.Drawing.Icon)
    	Me.NotifyIcon1.Text = "Notepad X"
    	Me.NotifyIcon1.Visible = true
    	'
    	'notifyIconContextMenu
    	'
    	Me.notifyIconContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BringToFrontToolStripMenuItem, Me.ExitToolStripMenuItem1, Me.SaveToolStripMenuItem1, Me.PrintToolStripMenuItem1})
    	Me.notifyIconContextMenu.Name = "notifyIconContextMenu"
    	Me.notifyIconContextMenu.Size = New System.Drawing.Size(148, 92)
    	'
    	'BringToFrontToolStripMenuItem
    	'
    	Me.BringToFrontToolStripMenuItem.Name = "BringToFrontToolStripMenuItem"
    	Me.BringToFrontToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
    	Me.BringToFrontToolStripMenuItem.Text = "Bring to Front"
    	'
    	'ExitToolStripMenuItem1
    	'
    	Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
    	Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(147, 22)
    	Me.ExitToolStripMenuItem1.Text = "Exit"
    	'
    	'SaveToolStripMenuItem1
    	'
    	Me.SaveToolStripMenuItem1.Name = "SaveToolStripMenuItem1"
    	Me.SaveToolStripMenuItem1.Size = New System.Drawing.Size(147, 22)
    	Me.SaveToolStripMenuItem1.Text = "Save"
    	'
    	'PrintToolStripMenuItem1
    	'
    	Me.PrintToolStripMenuItem1.Name = "PrintToolStripMenuItem1"
    	Me.PrintToolStripMenuItem1.Size = New System.Drawing.Size(147, 22)
    	Me.PrintToolStripMenuItem1.Text = "Print"
    	'
    	'BackgroundWorker1
    	'
    	Me.BackgroundWorker1.WorkerReportsProgress = true
    	'
    	'ToolStrip1
    	'
    	Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator7, Me.HelpToolStripButton, Me.ToolStripSeparator8, Me.ToolStripButton1, Me.ToolStripButton2})
    	Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
    	Me.ToolStrip1.Name = "ToolStrip1"
    	Me.ToolStrip1.Size = New System.Drawing.Size(610, 25)
    	Me.ToolStrip1.TabIndex = 3
    	Me.ToolStrip1.Text = "ToolStrip1"
    	'
    	'NewToolStripButton
    	'
    	Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"),System.Drawing.Image)
    	Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.NewToolStripButton.Name = "NewToolStripButton"
    	Me.NewToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.NewToolStripButton.Text = "&New"
    	'
    	'OpenToolStripButton
    	'
    	Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"),System.Drawing.Image)
    	Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.OpenToolStripButton.Name = "OpenToolStripButton"
    	Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.OpenToolStripButton.Text = "&Open"
    	'
    	'SaveToolStripButton
    	'
    	Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"),System.Drawing.Image)
    	Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.SaveToolStripButton.Name = "SaveToolStripButton"
    	Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.SaveToolStripButton.Text = "&Save"
    	'
    	'PrintToolStripButton
    	'
    	Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"),System.Drawing.Image)
    	Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.PrintToolStripButton.Name = "PrintToolStripButton"
    	Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.PrintToolStripButton.Text = "&Print"
    	'
    	'toolStripSeparator
    	'
    	Me.toolStripSeparator.Name = "toolStripSeparator"
    	Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
    	'
    	'CutToolStripButton
    	'
    	Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"),System.Drawing.Image)
    	Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.CutToolStripButton.Name = "CutToolStripButton"
    	Me.CutToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.CutToolStripButton.Text = "C&ut"
    	'
    	'CopyToolStripButton
    	'
    	Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.CopyToolStripButton.Image = CType(resources.GetObject("CopyToolStripButton.Image"),System.Drawing.Image)
    	Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.CopyToolStripButton.Name = "CopyToolStripButton"
    	Me.CopyToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.CopyToolStripButton.Text = "&Copy"
    	'
    	'PasteToolStripButton
    	'
    	Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.PasteToolStripButton.Image = CType(resources.GetObject("PasteToolStripButton.Image"),System.Drawing.Image)
    	Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.PasteToolStripButton.Name = "PasteToolStripButton"
    	Me.PasteToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.PasteToolStripButton.Text = "&Paste"
    	'
    	'toolStripSeparator7
    	'
    	Me.toolStripSeparator7.Name = "toolStripSeparator7"
    	Me.toolStripSeparator7.Size = New System.Drawing.Size(6, 25)
    	'
    	'HelpToolStripButton
    	'
    	Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    	Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"),System.Drawing.Image)
    	Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.HelpToolStripButton.Name = "HelpToolStripButton"
    	Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
    	Me.HelpToolStripButton.Text = "He&lp"
    	'
    	'ToolStripSeparator8
    	'
    	Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
    	Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
    	'
    	'ToolStripButton1
    	'
    	Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    	Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"),System.Drawing.Image)
    	Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.ToolStripButton1.Name = "ToolStripButton1"
    	Me.ToolStripButton1.Size = New System.Drawing.Size(126, 22)
    	Me.ToolStripButton1.Text = "Set Selected Text Font"
    	'
    	'ToolStripButton2
    	'
    	Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    	Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"),System.Drawing.Image)
    	Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    	Me.ToolStripButton2.Name = "ToolStripButton2"
    	Me.ToolStripButton2.Size = New System.Drawing.Size(131, 22)
    	Me.ToolStripButton2.Text = "Set Selected Text Color"
    	'
    	'AutoSaveTimer
    	'
    	Me.AutoSaveTimer.Enabled = true
    	Me.AutoSaveTimer.Interval = 5000
    	'
    	'timeBackgroundWorker
    	'
    	Me.timeBackgroundWorker.WorkerReportsProgress = true
    	'
    	'TextBox1
    	'
    	Me.TextBox1.AcceptsTab = true
    	Me.TextBox1.AutoWordSelection = true
    	Me.TextBox1.ContextMenuStrip = Me.rightClickContextMenuStrip
    	Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
    	Me.TextBox1.Location = New System.Drawing.Point(0, 49)
    	Me.TextBox1.Name = "TextBox1"
    	Me.TextBox1.Size = New System.Drawing.Size(610, 313)
    	Me.TextBox1.TabIndex = 4
    	Me.TextBox1.Text = ""
    	Me.TextBox1.WordWrap = false
    	'
    	'TextEditor
    	'
    	Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    	Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    	Me.ClientSize = New System.Drawing.Size(610, 384)
    	Me.Controls.Add(Me.TextBox1)
    	Me.Controls.Add(Me.ToolStrip1)
    	Me.Controls.Add(Me.StatusStrip1)
    	Me.Controls.Add(Me.MenuStrip1)
    	Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
    	Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
    	Me.MainMenuStrip = Me.MenuStrip1
    	Me.Name = "TextEditor"
    	Me.Text = "Notepad X - Untitled"
    	Me.rightClickContextMenuStrip.ResumeLayout(false)
    	Me.MenuStrip1.ResumeLayout(false)
    	Me.MenuStrip1.PerformLayout
    	Me.StatusStrip1.ResumeLayout(false)
    	Me.StatusStrip1.PerformLayout
    	Me.notifyIconContextMenu.ResumeLayout(false)
    	Me.ToolStrip1.ResumeLayout(false)
    	Me.ToolStrip1.PerformLayout
    	Me.ResumeLayout(false)
    	Me.PerformLayout
    End Sub
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
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ASCIIToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents currentFileStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents notifyIconContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BringToFrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rightClickContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UndoToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileToReadBytecodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileAsBytecodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RijndaelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DESToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TripleDESToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewEncryptionSamplesFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptedTextFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDFFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AESToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RC2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RSAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EncryptToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecryptyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents totalCharactersStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents selectedCharactersCountStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MacrosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoSaveTimer As System.Windows.Forms.Timer
    Friend WithEvents timeBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents TextBox1 As System.Windows.Forms.RichTextBox

End Class

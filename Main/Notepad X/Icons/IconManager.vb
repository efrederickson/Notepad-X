Public Class IconManager
    Public Icons As New Hashtable
    Public Info As New Hashtable

    Sub LoadIcons(ByVal iniFile As String)
        If Not IO.File.Exists(iniFile) Then Return
        Dim IcoIni As New INIFile()
        IcoIni.Load(iniFile)
        Dim infoSec As INIFile.INISection = IcoIni.GetSection("Info")
        Dim IconPath As String
        Dim icoSec As INIFile.INISection = IcoIni.GetSection("Icons")
        Info("Author") = infoSec.GetKey("Author").Value
        Info("Version") = infoSec.GetKey("Version").Value
        Info("Name") = infoSec.GetKey("Name").Value
        Info("Description") = infoSec.GetKey("Description").Value
        IconPath = IO.Path.GetDirectoryName(iniFile) & "\" & infoSec.GetKey("Path").Value

        For Each key As INIFile.INISection.INIKey In icoSec.Keys
            Dim img As Image = Image.FromFile(IconPath & key.Value)
            Icons(key.Name.ToLower) = img
            Log.WriteLine("IconManager: Adding " & key.Name & " the value " & IconPath & key.Value)
        Next
    End Sub

    Function Check(ByVal Form As MDIParent) As MDIParent
        On Error Resume Next
        For Each _item As ToolStripMenuItem In Form.MenuStrip.Items
            Scan(_item)
        Next
        Return Form
    End Function

    Function Check(ByVal Form As Form1) As Form1
        On Error Resume Next
        For Each _item As ToolStripMenuItem In Form.MenuStrip1.Items
            Scan(_item)
        Next
        Return Form
    End Function

    Function Check(ByVal form As EditForm) As EditForm
        On Error Resume Next
        For Each _item As ToolStripMenuItem In form.MenuStrip1.Items
            Scan(_item)
        Next
        Return form
    End Function

    Function CheckIcon(ByRef item As ToolStripMenuItem) As ToolStripMenuItem
        Dim ItemText As String = item.Text
        ItemText = ItemText.Replace(" ", "")
        ItemText = ItemText.Replace("&", "")
        Log.WriteLine("IconManager: Scanning for " & ItemText)
        If Icons.Contains(ItemText.ToLower) Then
            item.Image = CType(Icons(ItemText.ToLower), Image)
            Log.WriteLine(String.Format("IconManager: Setting {0} the icon from {1}", item.Text, ItemText))
        End If
        Return item
    End Function

    Sub Scan(ByVal item As ToolStripMenuItem)
        On Error Resume Next
        For Each _item In item.DropDownItems
            _item = CheckIcon(_item)
            Scan(_item)
        Next
    End Sub
End Class

Namespace Plugins
    ''' <summary>
    ''' Forms implementing this should also inherit from WeifenLuo.WinFormsUI.Docking.DockContent
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITextEditorForm
        Sub InsertText(ByVal text As String)
        Sub OpenDocument(ByVal name As String)
        Sub Print()
        Sub ShowPrintPreview()
        Sub ShowPrintSetup()
        Sub Save()
        Sub SaveAs()

    End Interface
End Namespace
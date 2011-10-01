form = WinForms.Form{
    Text="Main Form", Height=200, StartPosition="CenterScreen",
    WinForms.Label{ Text="Hello!", Name="lable", Width=80, Height=17, Top=9, Left=12 },
    WinForms.Button{ Text="Click", Width=80, Height=23, Top=30, Left=12,
        Click=function(sender,e) script.MessageBox(lable.Text,"Clicked") end },
}
script.Run(form)
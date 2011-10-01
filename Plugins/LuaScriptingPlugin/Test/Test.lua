assembly = script.GetLongAssembly("c:\\users\\elijah.kingdom\\desktop\\notepad x\\notepad x\\bin\\debug\\SnapOn.exe")
form = script.Create("SnapOn.Form1", assembly)
form.FormBorderStyle="FixedToolWindow"
form.StartPosition = "CenterParent"
btn = WinForms.Button{Text="click"}
btn.Parent = form
btn.Top = 40
btn.Left = 40
form.Text = Console.Read()
btn.Click = function (sender, e)
				script.MessageBox("hi!")
			end
btn.BringToFront()
if form == nil then
	script.MessageBox("Form is nil")
else
	--script.MessageBox("Form isn't nil")
	script.Run(form)
end
Console.WriteLine("\n------END OF SCRIPT--------")
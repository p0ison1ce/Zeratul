Imports System.IO

Public Class zForm

    Public WithEvents Proc As Process

    Dim iVersion As String = "0.2 (Sn0w Rabbit)"

#Region "Outputs from iRecovery.exe" ' (In order by iRecovery.exe)
    Dim iRecoveryInfo As String = "iRecovery - Recovery Utility" & vbCrLf & "by westbaer" & vbCrLf & "Thanks to pod2g, tom3q, planetbeing, geohot and posixninja."
    Dim old_Found As String = "Found iPhone/iPod in Recovery mode" & vbCrLf & "Closing USB connection..."
    Dim irecv_upload As String = "irecv_upload: "
    Dim irecv_exploit As String = "irecv_exploit: "
    Dim irecv_list As String = "irecv_list: "
    Dim irecv_list_Sending As String = "irecv_list: sending> "
    Dim not_found As String = "No iPhone/iPod found."
#End Region

#Region "Various of outputs"
    Dim parser As String = "sending command: "
    Dim ZeratulConsole As String = TimeOfDay.Minute & ":" & TimeOfDay.Second & " | Zeratul Console " & parser
    Dim new_Found As String = "iPhone/iPod Touch/iPad was found. Now closing the USB connection.."
    Dim new_UploadFile As String = "Uploading file"
    Dim new_ExecutingScript As String = "Executing script"
    Dim new_ResettingUSB As String = "Resetting USB..."
    Dim new_InvalidStatus As String = "Invalid execution status."
    Dim new_not_found = "No iPhone/iPod Touch found." & vbCrLf
#End Region

#Region "  Right Click Menu stuff"
    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        textBox2.Cut()
    End Sub
    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        textBox2.Copy()
    End Sub
    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        textBox2.Paste()
    End Sub
#End Region

#Region "  Extra functions"

#Region " Input / Output Functions"
    Public Function GetFileContents(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String = ""
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()

        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return strContents
    End Function

    Public Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        Dim bAns As Boolean = False, objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return bAns
    End Function
#End Region

#End Region

#Region "Stuff for reading iRecovery.exe"
    Private Sub Proc_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles Proc.OutputDataReceived
        Try : Me.Invoke(New UpdateDelegate(AddressOf UpdateGUI), e.Data) : Catch : End Try
    End Sub
    Private Delegate Sub UpdateDelegate(ByVal Output As String)
    Private Sub UpdateGUI(ByVal Output As String)
        If Output.Length > 0 Then textBox2.AppendText(Output & Environment.NewLine)
    End Sub
#End Region

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        button1.Text = "Sending.." : button1.Enabled = False


        If RadioButton1.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -c " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console -> " & textBox1.Text
        End If

        If RadioButton2.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -f " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Uploading) -> " & textBox1.Text
        End If

        If RadioButton3.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -k " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Exploiting) -> " & textBox1.Text
        End If

        If RadioButton4.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -l " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Executing Script) -> " & textBox1.Text
        End If

        button1.Text = "Send" : button1.Enabled = True


        textBox2.SelectionStart = Len(textBox2.Text)

    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        button2.Text = "Resetting.." : button2.Enabled = False
        Proc.StandardInput.WriteLine("irecovery.exe -r")
        button2.Text = "Reset USB" : button2.Enabled = True
    End Sub


    Private Sub zForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Zeratul -- Version: " & iVersion & " -- By: Fallensn0w"

        If System.IO.File.Exists("Zeratul.bat") = False Then
            BatData = _
                            ": Zeratul.bat" & vbCrLf & ": Purpose: Used for fixing the startup bug!" & vbCrLf & "" & vbCrLf & "@echo off" & vbCrLf & _
                            ":Fallensn0w" & vbCrLf & "set /p ""CMD=" & vbCrLf & "%CMD%" & vbCrLf & "CLS" & vbCrLf & "GoTo Fallensn0w" & vbCrLf

            SaveTextToFile(BatData, "Zeratul.bat")

        End If

        Proc = New Process
        Proc.StartInfo.FileName = "zeratul.bat"
        Proc.StartInfo.RedirectStandardInput = True : Proc.StartInfo.RedirectStandardOutput = True
        Proc.StartInfo.UseShellExecute = False : Proc.StartInfo.CreateNoWindow = True
        Proc.Start.ToString()

        Proc.BeginOutputReadLine()




    End Sub

    Private Sub textBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then button1_Click(sender, e)
    End Sub

    Private Sub textBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles textBox2.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            textBox2.ContextMenuStrip = textBox2_Menu
        End If
    End Sub

    Private Sub textBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox2.TextChanged

        ' Cleaning stuff from iRecovery that doesn't give any useful information... / Making everything look CLEANER.
        textBox2.Text = textBox2.Text.Replace("", "")

        textBox2.Text = textBox2.Text.Replace("irecovery.exe -c", Nothing)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -k", Nothing)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -f", new_UploadFile)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -l", new_ExecutingScript)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -r", new_ResettingUSB)

        textBox2.Text = Replace(textBox2.Text, iRecoveryInfo, Nothing)
        textBox2.Text = Replace(textBox2.Text, Chr(34) & vbCrLf, Nothing)
        textBox2.Text = Replace(textBox2.Text, Chr(34), Nothing)
        textBox2.Text = Replace(textBox2.Text, Split(old_Found, vbCrLf)(1), Nothing)

        textBox2.Text = Replace(textBox2.Text, old_Found, new_Found)
        textBox2.Text = Replace(textBox2.Text, Split(old_Found, vbCrLf)(0), new_Found)
        textBox2.Text = Replace(textBox2.Text, not_found, new_not_found)
        textBox2.Text = Replace(textBox2.Text, irecv_upload, Nothing)
        textBox2.Text = Replace(textBox2.Text, irecv_exploit, Nothing)
        textBox2.Text = Replace(textBox2.Text, irecv_list, ZeratulConsole)
        textBox2.Text = Replace(textBox2.Text, irecv_list_Sending, ZeratulConsole)

    End Sub

    Private Sub Website_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click
        Process.Start(LinkLabel1.Text)
    End Sub

    Private Sub LockTheOutputTextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockTheOutputTextToolStripMenuItem.Click
        If textBox2.ReadOnly = False Then
            textBox2.ReadOnly = True
            LockTheOutputTextToolStripMenuItem.Checked = True
        Else
            textBox2.ReadOnly = False
            LockTheOutputTextToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub zForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width <= 615 Then
            Me.Width = 615
        End If
    End Sub

    Private Sub textBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox1.TextChanged
        If InStr(textBox1.Text, "%desktop%") Then textBox1.Text = Replace(textBox1.Text, "%desktop%", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) : textBox1.SelectionStart = Len(textBox1.Text)
    End Sub


End Class

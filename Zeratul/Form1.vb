'######################################
'##                                                                    ##
'##   Zeratul -- By: Fallensn0w                              ##                   
'##   """"""""""""""""""""""""""""                              ##
'##                                                                    ##
'##   This program is open-source                       ##
'##   And available at github.                               ##
'##                                                                    ##
'##   Goal of this program is to                           ##
'##   make a fast and easy gui for                       ##
'##   irecovery so normal users can                     ##
'##   use irecovery very easily.                             ##
'##                                                                    ##
'######################################

Imports System.IO

Public Class zForm

    ' Purpose: If any changes, it can be easily made here.
#Region " Outputs from iRecovery.exe "

    Public WithEvents Proc As Process
    Public iVersion As String = "0.2.5.7 (Dark Sn0w Rabbit)"

    Dim iRecoveryInfo As String = "iRecovery - Recovery Utility" & vbCrLf & "by westbaer" & vbCrLf & "Thanks to pod2g, tom3q, planetbeing, geohot and posixninja."
    Dim old_Found As String = "Found iPhone/iPod in Recovery mode" & vbCrLf & "Closing USB connection..."
    Dim irecv_upload As String = "irecv_upload: "
    Dim irecv_exploit As String = "irecv_exploit: "
    Dim irecv_list As String = "irecv_list: "
    Dim irecv_list_Sending As String = "irecv_list: sending> "
    Dim not_found As String = "No iPhone/iPod found."
    Dim parser As String = "sending command: "

    Dim ZeratulConsole As String = TimeOfDay.Minute & ":" & TimeOfDay.Second & " | Zeratul Console " & parser
    Dim new_Found As String = "iPhone/iPod Touch/iPad was found. Now closing the USB connection.."
    Dim new_UploadFile As String = "Uploading file"
    Dim new_ExecutingScript As String = "Executing script"
    Dim new_ResettingUSB As String = "Resetting USB..."
    Dim new_InvalidStatus As String = "Invalid execution status."
    Dim new_not_found = "No iPhone/iPod Touch found." & vbCrLf
#End Region

#Region " Right Click Menu stuff "
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

#Region " Input / Output Functions "

    ' Purpose: Used for gathering data from any file
    Public Function GetFileContents(ByVal FullPath As String, Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String = "", objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return strContents
    End Function

    ' Purpose: Used for saving any data to any file.
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

#Region " Stuff for reading iRecovery.exe "

    ' Purpose: When we receive output data we need to invoke it.
    Private Sub Proc_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles Proc.OutputDataReceived
        Try : Me.Invoke(New UpdateDelegate(AddressOf UpdateGUI), e.Data) : Catch : End Try
    End Sub

    Private Delegate Sub UpdateDelegate(ByVal Output As String)
    ' Purpose: Used for updating textBox.
    Private Sub UpdateGUI(ByVal Output As String)
        If Output.Length > 0 Then textBox2.AppendText(Output & Environment.NewLine)
    End Sub
#End Region


    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        button1.Text = "Sending.." : button1.Enabled = False


        If RadioButton1.Checked Then
            'Proc.StandardInput.WriteLine(textBox1.Text)
            Proc.StandardInput.WriteLine("irecovery.exe -c " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Command) -> " & textBox1.Text
        End If

        If RadioButton2.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -f " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Uploading) -> " & textBox1.Text
        End If

        If RadioButton3.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -k " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Exploit) -> " & textBox1.Text
        End If

        If RadioButton4.Checked Then
            Proc.StandardInput.WriteLine("irecovery.exe -l " & Chr(34) & textBox1.Text & Chr(34))
            textBox2.Text += "Zeratul Console (Executing Script) -> " & textBox1.Text
        End If

        button1.Text = "Send" : button1.Enabled = True

    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        button2.Text = "Resetting.." : button2.Enabled = False
        Proc.StandardInput.WriteLine("irecovery.exe -r")
        button2.Text = "Reset USB" : button2.Enabled = True
    End Sub


    Private Sub zForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Proc.StandardInput.WriteLine("exit")
        End
    End Sub


    Private Sub zLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "Zeratul -- Version: " & iVersion & " -- By: Fallensn0w"

        If System.IO.File.Exists("Zeratul.bat") = False Then

            BatData = _
                            ": Zeratul.bat" & vbCrLf & ": Purpose: Used for fixing the startup bug!" & vbCrLf & "" & vbCrLf & "@echo off" & vbCrLf & _
                            ":Fallensn0w" & vbCrLf & "set /p ""CMD=" & vbCrLf & "%CMD%" & vbCrLf & "CLS" & vbCrLf & "GoTo Fallensn0w" & vbCrLf

            SaveTextToFile(BatData, "Zeratul.bat")

        End If

        Proc = New Process
        Proc.StartInfo.FileName = "Zeratul.bat"

        Proc.StartInfo.RedirectStandardInput = True : Proc.StartInfo.RedirectStandardOutput = True
        Proc.StartInfo.UseShellExecute = False : Proc.StartInfo.CreateNoWindow = True
        Proc.Start.ToString()

        Proc.BeginOutputReadLine()


    End Sub

    Private Sub textBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then _
            button1_Click(sender, e)
    End Sub

    Private Sub textBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles textBox2.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then _
            textBox2.ContextMenuStrip = textBox2_Menu
    End Sub

    Private Sub zCheck()

        ZeratulVersion = "http://fallensn0w.host22.com/Zeratul_Version"
        ZeratulZip = "http://fallensn0w.host22.com/Zeratul.zip"
        ZeratulNew = "http://fallensn0w.host22.com/Zeratul_New"

        If System.IO.File.Exists("temp") Then System.IO.File.Delete("temp")
        If System.IO.File.Exists("new") Then System.IO.File.Delete("new")


        '  On Error Resume Next
        On Error GoTo errz
        My.Computer.Network.DownloadFile(ZeratulVersion, "temp")
        My.Computer.Network.DownloadFile(ZeratulNew, "new")

        temp = GetFileContents("temp")
        tempdata = GetFileContents("new")

        If iVersion = temp Then
            Me.Button3.Text = "No new updates"
            Me.Button3.Enabled = False
            Exit Sub
        End If

        zupdate = MessageBox.Show( _
                                                                "A new update for Zeratul was found!" & vbCrLf & vbCrLf & _
                                                                "Current Version: " & iVersion & vbCrLf & _
                                                                "Available for download: " & temp & vbCrLf & vbCrLf & _
                                                                "Would you like to download the new version?", _
        Me.Text, MessageBoxButtons.YesNo)



        If zupdate = vbYes Then
            My.Computer.Network.DownloadFile(ZeratulZip, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Zeratul.zip")
            MsgBox("Zeratul Version " & temp & " is now downloaded and located at your desktop." & vbCrLf & vbCrLf & _
                         "New stuff for " & temp & ":" & vbCrLf & vbCrLf & tempdata, MsgBoxStyle.Information, Me.Text)
            End
        End If

        Exit Sub

errz:
        MessageBox.Show("ERROR: You need internet access for updating Zeratul!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Exit Sub

    End Sub

    Private Sub RemoveStuff(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox2.TextChanged

        ' Cleaning stuff from iRecovery that doesn't give any useful information... / Making everything look CLEANER.

        textBox2.Text = textBox2.Text.Replace("", "") ' ive seriously no idea why it pop-ups a female icon lol
        '                                                                       guess its vbCrLf as an icon for the font i'm using on textBox2

        textBox2.Text = textBox2.Text.Replace("irecovery.exe -c", Nothing)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -k", Nothing)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -f", new_UploadFile)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -l", new_ExecutingScript)
        textBox2.Text = textBox2.Text.Replace("irecovery.exe -r", new_ResettingUSB)

        textBox2.Text = Replace(textBox2.Text, iRecoveryInfo, Nothing)
        textBox2.Text = Replace(textBox2.Text, Split(old_Found, vbCrLf)(1), Nothing)

        textBox2.Text = Replace(textBox2.Text, old_Found, new_Found)
        textBox2.Text = Replace(textBox2.Text, Split(old_Found, vbCrLf)(0), new_Found)
        textBox2.Text = Replace(textBox2.Text, not_found, new_not_found)
        textBox2.Text = Replace(textBox2.Text, vbCrLf & vbCrLf & new_not_found, vbCrLf & new_not_found)

        textBox2.Text = Replace(textBox2.Text, irecv_upload, Nothing)
        textBox2.Text = Replace(textBox2.Text, irecv_exploit, Nothing)
        textBox2.Text = Replace(textBox2.Text, irecv_list, ZeratulConsole)
        textBox2.Text = Replace(textBox2.Text, irecv_list_Sending, ZeratulConsole)


        textBox2.SelectionStart = Len(textBox2.Text)
        textBox2.ScrollToCaret()

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
        'If Me.Width <= 615 Then
        'Me.Width = 615
        'End If
    End Sub

    Private Sub textBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox1.TextChanged
        If InStr(textBox1.Text, "$d") Then _
            textBox1.Text = Replace(textBox1.Text, "$d", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) : textBox1.SelectionStart = Len(textBox1.Text)
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        zCheck()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://fallensn0w.host22.com")
    End Sub

End Class

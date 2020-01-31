Imports System.Windows.Forms

Public Class Window2
    Dim Pathtoextract As String
    Private FilesOK As Boolean
    Dim File As String
    Dim [Object] As Vortexdll.Vorlib
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Title = "Reading..."
        IsEnabled = False
        Filelist.Items.Add("..All")
        Dim openFileDialog = New OpenFileDialog()
        openFileDialog.Filter = "vor Archive|*.vor"
        If openFileDialog.ShowDialog() = Forms.DialogResult.OK Then
            FilesOK = True
            File = openFileDialog.FileName
            archivepathtxt.Text = File
            [Object] = MessagePack.MessagePackSerializer.Deserialize(Of Vortexdll.Vorlib)(LZ4.LZ4Codec.Unwrap(IO.File.ReadAllBytes(File)))
            For i = 0 To [Object].Names.Count - 1
                Filelist.Items.Add([Object].Names(i) & "." & [Object].Formats(i))

            Next
        End If
        IsEnabled = True
        Title = "Extract Archive"
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub checkboxx_Checked(sender As Object, e As RoutedEventArgs) Handles checkboxx.Checked

        passwordtxt.IsEnabled = True

    End Sub
    Private Sub checkboxx_unChecked(sender As Object, e As RoutedEventArgs) Handles checkboxx.Unchecked

        passwordtxt.IsEnabled = False
            passwordtxt.Text = ""

    End Sub


    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        If pathtoextracttxt.Text <> "" AndAlso archivepathtxt.Text <> "" Then
            If checkboxx.IsChecked Then
                Select Case Filelist.SelectedIndex
                    Case 0
                        Title = "Extracting..."
                        IsEnabled = False

                        For i = 0 To [Object].Files.Count - 1
                            IO.File.WriteAllBytes(IO.Path.Combine(Pathtoextract, [Object].Names(i) & [Object].Formats(i)), (Crypto.AES.AES.DecryptBytes(passwordtxt.Text, [Object].Files(i))))
                        Next

                                                  Case Else

                        Dim i = Filelist.SelectedIndex - 1
                        IO.File.WriteAllBytes(IO.Path.Combine(Pathtoextract, [Object].Names(i) & [Object].Formats(i)), (Crypto.AES.AES.DecryptBytes(passwordtxt.Text, [Object].Files(i))))
                End Select
            Else
                Select Case Filelist.SelectedIndex
                    Case 0
                        Title = "Extracting..."
                        IsEnabled = False
                        For i = 0 To [Object].Files.Count - 1
                            IO.File.WriteAllBytes(IO.Path.Combine(Pathtoextract, [Object].Names(i) & [Object].Formats(i)), ([Object].Files(i)))
                        Next
                    Case Else

                        Dim i = Filelist.SelectedIndex - 1
                        IO.File.WriteAllBytes(IO.Path.Combine(Pathtoextract, [Object].Names(i) & [Object].Formats(i)), ([Object].Files(i)))
                End Select
            End If

            Title = "Extract Archive"
            IsEnabled = True
            MsgBox("Extracted!", 64)
            GC.Collect()
        End If
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim dialog = New FolderBrowserDialog()
        If dialog.ShowDialog = Forms.DialogResult.OK Then
            Pathtoextract = dialog.SelectedPath
            pathtoextracttxt.Text = Pathtoextract
        End If
    End Sub
End Class

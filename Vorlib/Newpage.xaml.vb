Imports Microsoft.Win32

Public Class Window1
    Dim Savepath As String
    Dim Files As String()
    Dim FilesOK As Boolean = False
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        openFileDialog.Multiselect = True
        If openFileDialog.ShowDialog() = True Then
            FilesOK = True
            Files = openFileDialog.FileNames
            For i = 0 To Files.Count - 1
                Filestxt.Text = Filestxt.Text & Files(i) & ","
            Next
        End If

    End Sub

    Private Sub encryptfileschbox_Checked(sender As Object, e As RoutedEventArgs) Handles encryptfileschbox.Checked
        passwordtxt.IsEnabled = True

    End Sub

    Private Sub encryptfileschbox_unChecked(sender As Object, e As RoutedEventArgs) Handles encryptfileschbox.Unchecked

        passwordtxt.Text = ""
            passwordtxt.IsEnabled = False

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        If Filestxt.Text <> "" And Saveto.Text <> "" Then
            Title = "Compressing..."
            IsEnabled = False

            Dim Archive As New Vortexdll.Vorlib

            If encryptfileschbox.IsChecked Then
                For i = 0 To Files.Count - 1
                    Archive.Names.Add(IO.Path.GetFileNameWithoutExtension(Files(i)))
                    Archive.Formats.Add(IO.Path.GetExtension(Files(i)))
                    Archive.Files.Add((Crypto.AES.AES.EncryptBytes(passwordtxt.Text, IO.File.ReadAllBytes(Files(i)))))
                Next
            Else
                For i = 0 To Files.Count - 1
                    Archive.Names.Add(IO.Path.GetFileNameWithoutExtension(Files(i)))
                    Archive.Formats.Add(IO.Path.GetExtension(Files(i)))
                    Archive.Files.Add((IO.File.ReadAllBytes(Files(i))))
                Next
            End If

            IO.File.WriteAllBytes(Savepath, LZ4.LZ4Codec.Wrap((MessagePack.MessagePackSerializer.Serialize(Archive))))
            Title = "New Archive"
            IsEnabled = True
            MsgBox("Archive Created!", 64)
            GC.Collect()
        End If
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New SaveFileDialog
        openFileDialog.Filter = "vor package|*.vor"
        If openFileDialog.ShowDialog Then
            Savepath = openFileDialog.FileName
            Saveto.Text = Savepath
        End If
    End Sub
End Class

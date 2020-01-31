Class MainWindow
    Private Sub Main_ui_load(sender As Object, e As RoutedEventArgs) Handles Main_ui.Loaded

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim x As New Window1
        x.Show()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        End
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim win As New Window2
        win.Show()
    End Sub

    Private Sub Button_ContextMenuClosing(sender As Object, e As ContextMenuEventArgs)

    End Sub
End Class

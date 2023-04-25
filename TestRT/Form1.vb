Public Class Form1

    Private RT As Realtime

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RT = New Realtime
        Await RT.Subscribe()
    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Await RT.InsertRow()
    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Await RT.InsertRandomRow()
    End Sub


End Class

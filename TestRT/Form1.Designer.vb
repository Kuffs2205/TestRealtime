<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button2 = New Button()
        Button3 = New Button()
        SuspendLayout()
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(23, 26)
        Button2.Name = "Button2"
        Button2.Size = New Size(175, 23)
        Button2.TabIndex = 1
        Button2.Text = "Insert Subscribed Row"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(23, 76)
        Button3.Name = "Button3"
        Button3.Size = New Size(175, 23)
        Button3.TabIndex = 2
        Button3.Text = "Insert Random Row"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(224, 134)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
    End Sub
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class

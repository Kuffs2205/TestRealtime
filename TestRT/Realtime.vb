Imports System.Net
Imports Postgrest.Attributes
Imports Postgrest.Models
Imports Supabase.Realtime
Imports Supabase.Realtime.Channel
Imports Supabase.Realtime.PostgresChanges
Imports Supabase.Realtime.Socket
Imports Websocket.Client.Logging
Public Class Realtime

    Private parentID As Guid = Guid.Parse("16cc5c6e-8658-4094-852e-cf1564a08f6e")

    Private WithEvents TestChannel As RealtimeChannel
    Private client As Supabase.Client

    ' alter table "TestData" replica identity full has been run

    Public Async Function Subscribe() As Task
        client = Await SupabaseManager.GetInstance.GetClient

        TestChannel = client.Realtime.Channel($"MyChannel-{parentID}")
        TestChannel.Register(New PostgresChangesOptions("public",
                                                        "TestData",
                                                        PostgresChangesOptions.ListenType.Updates,
                                                        $"parent=eq.{parentID}"))

        Await TestChannel.Subscribe

    End Function

    Private Sub TestChannel_OnPostgresChange(sender As Object, e As PostgresChangesEventArgs) Handles TestChannel.OnPostgresChange

        Dim model = e.Response.Model(Of TestData)

        Debug.WriteLine($"POSTGRES: {e.Response.Payload}")
    End Sub

    Public Async Function InsertRow() As Task

        Dim X = New TestData With {.id = Guid.NewGuid, .parent = parentID, .X = 99, .Y = 99}
        Await client.From(Of TestData).Insert(X)

    End Function

    Public Async Function InsertRandomRow() As Task

        Dim X = New TestData With {.id = Guid.NewGuid, .parent = Guid.NewGuid, .X = 1, .Y = 1}
        Await client.From(Of TestData).Insert(X)

    End Function

    Private Sub TestChannel_OnInsert(sender As Object, e As PostgresChangesEventArgs) Handles TestChannel.OnInsert

        Dim model = e.Response.Model(Of TestData)

        Debug.WriteLine($"INSERT: {model}")
    End Sub

    Private Sub TestChannel_OnDelete(sender As Object, e As PostgresChangesEventArgs) Handles TestChannel.OnDelete
        Dim model = e.Response.OldModel(Of TestData)
        Debug.WriteLine($"DELETE: {model}")
    End Sub

    Private Sub TestChannel_OnUpdate(sender As Object, e As PostgresChangesEventArgs) Handles TestChannel.OnUpdate
        Dim model = e.Response.Model(Of TestData)
        Debug.WriteLine($"UPDATE: {model}")
    End Sub
End Class

Public Class TestData
    Inherits BaseModel

    <PrimaryKey("id", True)>
    Public Property id As Guid
    Public Property parent As Guid
    Public Property X As Integer
    Public Property Y As Integer

    Public Overrides Function ToString() As String
        Return $"Model: {id}, {If(parent.Equals(Guid.Parse("16cc5c6e-8658-4094-852e-cf1564a08f6e")), "Known", "Random")}, X={X}, Y={Y}"
    End Function

    '  create table
    '  Public.TestData (
    '  parent uuid null Default uuid_generate_v4 (),
    '  X integer null default 0,
    '  Y integer null default 0,
    '  id uuid Not null Default uuid_generate_v4 (),
    '  constraint TestData_pkey primary key (id)
    ') tablespace pg_default;


End Class


Public Class SupabaseManager
    Private Shared instance As SupabaseManager
    Private ReadOnly Property Client As Supabase.Client
    Private Const Url As String = "https://XXX.supabase.co"
    Private Const AdminKey As String = "secret key"

    Private Sub New()
    End Sub
    Public Shared ReadOnly Property GetInstance As SupabaseManager
        Get
            If instance Is Nothing Then instance = New SupabaseManager
            Return instance
        End Get
    End Property
    Public Async Function GetClient() As Task(Of Supabase.Client)
        Try
            If Client IsNot Nothing Then Return Client
            Dim c = New Supabase.Client(Url, AdminKey, New Supabase.SupabaseOptions With {.AutoConnectRealtime = True})
            Dim x = Await c.InitializeAsync
            _Client = c
            Return _Client
        Catch ex As Exception
            Debug.WriteLine("Client Fail")
            Return Nothing
        End Try
    End Function
    Public ReadOnly Property IsConnected As Boolean
        Get
            Return Client IsNot Nothing
        End Get
    End Property
    Public Sub Disconnect()
        _Client = Nothing
    End Sub

End Class

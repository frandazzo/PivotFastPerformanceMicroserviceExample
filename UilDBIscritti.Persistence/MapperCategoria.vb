Imports UilDBIscritti.Domain

Public Class MapperCategoria
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from fenealweb_categories  order by description"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from fenealweb_categories where Id = @Id  order by alias"
    End Function

    Protected Overrides Function InsertStatement() As String
        Throw New Exception("Not implemented method")
    End Function

    Protected Overrides Function UpdateStatement() As String
        Throw New Exception("Not implemented method")
    End Function

    Protected Overrides Function DeleteStatement() As String
        Throw New Exception("Not implemented method")
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Throw New Exception("Not implemented method")
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Categoria)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Categoria As New Categoria


            Categoria.Descrizione = rs("description")
            Categoria.Alias = rs("alias")



            Return Categoria

        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto categoria con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function


    Protected Overrides Function CreateKey(rs As Hashtable) As Key
        Dim id As String = rs("id").ToString()

        Return New Key(Integer.Parse(id))

    End Function

    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand

    '    'Return New SqlClient1.SqlCommand(CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region




    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Throw New Exception("Not implemented method")
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Throw New Exception("Not implemented method")
    End Sub

End Class

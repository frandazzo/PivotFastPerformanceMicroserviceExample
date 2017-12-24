Imports UilDBIscritti.Domain

Public Class MapperTerritorio
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "SELECT c.*, p.Descrizione as 'province' FROM fenealweb_company c inner join fenealweb_companies_provinces a on a.companyId = c.id inner join tb_provincie p on p.ID = a.provinceId order by province"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return ""
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

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Territorio)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Territorio As New Territorio


            Territorio.Descrizione = rs("description")
            Territorio.Alias = rs("alias")
            Territorio.Province = rs("province")
            Territorio.Key = Key

            Return Territorio

        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Territorio con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
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


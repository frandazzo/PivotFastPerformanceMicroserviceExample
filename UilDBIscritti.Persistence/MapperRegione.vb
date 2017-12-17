Imports WIN.BASEREUSE
Imports WIN.TECHNICAL.PERSISTENCE

Public Class MapperRegione
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from TB_REGIONI  order by Descrizione"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from TB_REGIONI where Id = @Id  order by Descrizione"
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

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Regione)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim Descrizione As String = rs("DESCRIZIONE")
            Dim reg As Regione = New Regione(Key, Descrizione)
            reg.ListaComuni = New LaziLoadComuni(Key.LongValue, LaziLoadComuni.Type.Regioni, Registro)
            reg.ListaProvincie = New LazyLoadProvincie(Key.LongValue, Registro)
            Return reg
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto conto con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function
    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand

    '    ' Return New SqlClient1.SqlCommand(CommandText, DBConnectionManager.Instance.GetCurrentConnection)
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

Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Public Class MapperProvincia
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from TB_PROVINCIE  order by Descrizione"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from TB_PROVINCIE where Id = @Id  order by Descrizione"
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

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Provincia)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)


            Dim Descrizione As String = rs("DESCRIZIONE")
            Dim Sigla As String
            Dim IdRegione As Int32


            If Not IsDBNull(rs("ID_TB_REGIONI")) Then
                IdRegione = rs("ID_TB_REGIONI")
            Else
                IdRegione = -1
            End If
            Sigla = rs("SIGLA") & ""


            Dim prov As Provincia = New Provincia(Key, Descrizione, IdRegione, Sigla)
            prov.ListaComuni = New LaziLoadComuni(Key.LongValue, LaziLoadComuni.Type.Provincie, Registro)


            Return prov
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto provincia con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
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

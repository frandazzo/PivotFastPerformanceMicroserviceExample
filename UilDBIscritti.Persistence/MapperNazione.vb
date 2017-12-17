Imports WIN.BASEREUSE
Imports WIN.TECHNICAL.PERSISTENCE

Public Class MapperNazione
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from TB_NAZIONI order by Descrizione"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from TB_NAZIONI where Id = @Id order by Descrizione"
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
        Try
            Return DirectCast(MyBase.FindByKey(New Key(Id)), Nazione)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim Descrizione As String = rs("DESCRIZIONE")
            Dim IdRAZZE As Int32
            Dim CF As String = rs("CODICE_CF") & ""
            Dim CI As String = rs("CODICE_ISS") & ""
            If Not IsDBNull(rs("ID_TB_RAZZE")) Then
                IdRAZZE = rs("ID_TB_PROVINCIE")
            Else
                IdRAZZE = -1
            End If
            Dim naz As Nazione = New Nazione(Key, Descrizione, IdRAZZE, CF, CI)
            Return naz
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto nazione con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
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

Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Public Class MapperComune
    Inherits AbstractRDBMapper

    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(False)
    End Sub

#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from TB_COMUNI  order by Descrizione"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from TB_COMUNI where Id = @Id  order by Descrizione"
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
            Return DirectCast(MyBase.FindByKey(New Key(Id)), Comune)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim IdProvincia As Int32
            Dim IdRegione As Int32
            Dim Cap As String
            Dim CF As String
            Dim CI As String

            Dim Descrizione As String = rs("DESCRIZIONE")
            If Not IsDBNull(rs(UCase("Id_Tb_Provincie"))) Then
                IdProvincia = rs(UCase("Id_Tb_Provincie"))
            Else
                IdProvincia = -1
            End If
            If Not IsDBNull(rs(UCase("Id_Tb_Regioni"))) Then
                IdRegione = rs(UCase("Id_Tb_Regioni"))
            Else
                IdRegione = -1
            End If
            If Not IsDBNull(rs("CAP")) Then
                Cap = rs("CAP")
            Else
                Cap = ""
            End If
            CF = rs(UCase("Codice_Fiscale")) & ""
            CI = rs(UCase("Codice_Istat")) & ""
            Dim com As Comune = New Comune(Key, Descrizione, IdProvincia, IdRegione, Cap, CF, CI)
            Return com
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto comune con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
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

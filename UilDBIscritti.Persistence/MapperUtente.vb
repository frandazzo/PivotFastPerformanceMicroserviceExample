Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Imports UilDBIscritti.Domain.Security
Imports WIN.TECHNICAL.SECURITY_NEW.RoleManagement
Imports UilDBIscritti.Domain

Public Class MapperUtente
    Inherits AbstractRDBMapper


    Private MapperRuoli As IRoleProvider


    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from fenealweb_users"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from fenealweb_users where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return ""
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return ""
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return ""
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Utente)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim User As New Utente
            User.Key = Key
            User.UserName = rs.Item("username")
            User.Password = rs.Item("decPass")
            User.Nome = rs.Item("name")
            User.Cognome = rs.Item("surname")
            User.Mail = rs.Item("mail")
            User.Locked = IIf(rs.Item("active") = 1, False, True)

            User.PasswordData = New Date(2050, 1, 1)


            Dim MapperCategoria As MapperCategoria = Registro.GetMapperByName("MapperCategoria")

            Dim Id_Categoria As Int32 = IIf(rs.Item("categoryId") IsNot Nothing, rs.Item("categoryId"), -1)
            Dim Categoria As Categoria = IIf(Id_Categoria = -1, Nothing, MapperCategoria.FindObjectById(Id_Categoria))


            User.Categoria = Categoria


            User.Roles = New List(Of WIN.TECHNICAL.SECURITY_NEW.RoleManagement.Role)
            Return User
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Utente con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function



#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)


    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)

    End Sub
End Class

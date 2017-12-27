Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE
Imports UilDBIscritti.Domain.Workers
Imports UilDBIscritti.Domain

Public Class MapperExport
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from importazioni"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from importazioni where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into importazioni (Id_Provincia, " _
                        & "NomeProvincia, Id_Regione, NomeRegione, " _
                        & "DataEsportazione,  Responsabile, " _
                        & " Anno, categoryId, companyId, UltimaModifica) values " _
                        & "(@idp, @nmp, @idr, @nmr, @dte, " _
                        & "@res, @ann, @catid, @comid, @ult)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE importazioni SET " _
                        & "Id_Provincia = @idp, " _
                        & "NomeProvincia = @nmp, " _
                        & "Id_Regione = @idr, " _
                        & "NomeRegione = @nmr, " _
                        & "DataEsportazione = @dte, " _
                        & "Responsabile = @res, " _
                        & "Anno = @ann, " _
                        & "categoryId = @catid, " _
                        & "companyId = @comid, " _
                        & "UltimaModifica = @ult " _
                        & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from importazioni where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Export)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try

            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim exp As New Export
            exp.Key = Key

            'exp.Structure = IIf(rs.Item("Struttura") IsNot Nothing, rs.Item("Struttura"), "")
            'exp.Area = IIf(rs.Item("Area") IsNot Nothing, rs.Item("Area"), "")


            exp.ExporterName = rs.Item("Responsabile")
            exp.ExportDate = rs.Item("DataEsportazione")
            exp.UltimaModifica = rs.Item("UltimaModifica")
            exp.Anno = rs.Item("Anno")

            'exp.DataModifica = rs.Item("UltimaModifica")

            Dim MapperCategoria As MapperCategoria = Registro.GetMapperByName("MapperCategoria")

            Dim Id_Categoria As Int32 = IIf(rs.Item("categoryId") IsNot Nothing, rs.Item("categoryId"), -1)
            Dim Categoria As Categoria = IIf(Id_Categoria = -1, Nothing, MapperCategoria.FindObjectById(Id_Categoria))


            exp.Categoria = Categoria


            Dim MapperTerritorio As MapperTerritorio = Registro.GetMapperByName("MapperTerritorio")

            Dim Id_Territorio As Int32 = IIf(rs.Item("companyId") IsNot Nothing, rs.Item("companyId"), -1)
            Dim Territorio As Territorio = IIf(Id_Territorio = -1, Nothing, MapperTerritorio.FindObjectById(Id_Territorio))


            exp.Territorio = Territorio



            'recupero la regione
            Dim MapperRegione As MapperRegione = Registro.GetMapperByName("MapperRegione")

            Dim Id_Regione As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim Regione As Regione = IIf(Id_Regione = -1, Nothing, MapperRegione.FindObjectById(Id_Regione))
            If TypeOf (Regione) Is RegioneNulla Then
                Regione = Nothing
            End If

            exp.Regione = Regione

            'recupero la provincia
            Dim MapperProvincia As MapperProvincia = Registro.GetMapperByName("MapperProvincia")

            Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            Dim PROVINCIA As Provincia = IIf(ID_PROVINCIA = -1, Nothing, MapperProvincia.FindObjectById(ID_PROVINCIA))
            If TypeOf (PROVINCIA) Is ProvinciaNulla Then
                PROVINCIA = Nothing
            End If

            exp.Province = PROVINCIA


            Return exp
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Export con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As Export = DirectCast(Item, Export)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = exp.Province.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = exp.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = exp.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = exp.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dte"
            param.Value = exp.ExportDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@res"
            param.Value = exp.ExporterName
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = exp.Anno
            Cmd.Parameters.Add(param)





            param = Cmd.CreateParameter
            param.ParameterName = "@catid"
            param.Value = CLng(exp.Categoria.Id)
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@comid"
            param.Value = CLng(exp.Territorio.Id)
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ult"
            param.Value = DateTime.Now
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Export." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim exp As Export = DirectCast(Item, Export)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = exp.Province.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = exp.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = exp.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = exp.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@dte"
            param.Value = exp.ExportDate.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@res"
            param.Value = exp.ExporterName
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = exp.Anno
            Cmd.Parameters.Add(param)





            param = Cmd.CreateParameter
            param.ParameterName = "@catid"
            param.Value = CLng(exp.Categoria.Id)
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@comid"
            param.Value = CLng(exp.Territorio.Id)
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ult"
            param.Value = DateTime.Now
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = exp.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Export." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class

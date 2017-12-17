
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Imports WIN.BASEREUSE.AbstractPersona
Imports UilDBIscritti.Domain.Workers
Imports UilDBIscritti.Domain

Public Class MapperSubscription
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from iscrizioni"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from iscrizioni where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into iscrizioni (Id_Lavoratore, " _
                & "Id_Importazione, Id_Provincia, NomeProvincia, " _
                & "Id_Regione, NomeRegione, Anno, nazionaLavoratore, " _
                & "nomeCategoria, categoryId, territorioId) " _
                & "values " _
                & "(@idl, @idi, @idp, @nmp, @idr, " _
                & "@nmr,  @ann,  @nomnaz, @nomcat, @catid, @comid )"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE iscrizioni SET " _
                & "Id_Lavoratore = @idl, " _
                & "Id_Importazione = @idi, " _
                & "Id_Provincia = @idp, " _
                & "NomeProvincia = @nmp, " _
                & "Id_Regione = @idr, " _
                & "NomeRegione = @nmr, " _
                & "Anno = @ann, " _
                & "nazionaLavoratore = @nomnaz, " _
                & "nomeCategoria = @nomcat, " _
                & "categoryId = @catid, " _
                & "territorioId = @comid " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from iscrizioni where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Subscription)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            'recupero il registro dei mapper
            Dim Registro As PersistenceMapperRegistry = New PersistenceMapperRegistry
            Registro.SetPersistentService(m_PersistentService)

            Dim iscrizione As New Subscription
            iscrizione.Key = Key





            'recupero l'utente
            Dim MapperWorker As MapperWorker = Registro.GetMapperByName("MapperWorker")

            Dim ID_WORKER As Int32 = IIf(rs.Item("Id_Lavoratore") IsNot Nothing, rs.Item("Id_Lavoratore"), -1)
            Dim WORKER As Worker = IIf(ID_WORKER = -1, Nothing, MapperWorker.FindObjectById(ID_WORKER))



            'recupero l'importazione di riferimento
            Dim MapperExport As MapperExport = Registro.GetMapperByName("MapperExport")

            Dim ID_Import As Int32 = IIf(rs.Item("Id_Importazione") IsNot Nothing, rs.Item("Id_Importazione"), -1)
            Dim Export As Export = IIf(ID_Import = -1, Nothing, MapperExport.FindObjectById(ID_Import))


            Dim MapperCategoria As MapperCategoria = Registro.GetMapperByName("MapperCategoria")

            Dim Id_Categoria As Int32 = IIf(rs.Item("categoryId") IsNot Nothing, rs.Item("categoryId"), -1)
            Dim Categoria As Categoria = IIf(Id_Categoria = -1, Nothing, MapperCategoria.FindObjectById(Id_Categoria))




            Dim MapperTerritorio As MapperTerritorio = Registro.GetMapperByName("MapperCategoria")

            Dim Id_Territorio As Int32 = IIf(rs.Item("companyId") IsNot Nothing, rs.Item("companyId"), -1)
            Dim Territorio As Territorio = IIf(Id_Territorio = -1, Nothing, MapperTerritorio.FindObjectById(Id_Territorio))


            iscrizione.Territorio = Territorio
            iscrizione.Categoria = Categoria
            iscrizione.ParentExport = Export
            iscrizione.Worker = WORKER
            iscrizione.DenormalizedData.NomeNazioneNascita = IIf(rs.Item("nazionaLavoratore") IsNot Nothing, rs.Item("nazionaLavoratore"), "")




            'materializzo la nazione in questo modo per 
            'evitare numerose query e velocizzare il processo di materializzazione




            Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            Dim Nome_Provincia As String = IIf(rs.Item("NomeProvincia") IsNot Nothing, rs.Item("NomeProvincia"), "")

            Dim PROVINCIA As Provincia

            If (ID_PROVINCIA = -1) Then
                PROVINCIA = New ProvinciaNulla
            Else
                PROVINCIA = New Provincia
                PROVINCIA.Key = New Key(ID_PROVINCIA)
                PROVINCIA.Descrizione = Nome_Provincia
            End If



            Dim ID_REGIONE As Int32 = IIf(rs.Item("Id_Regione") IsNot Nothing, rs.Item("Id_Regione"), -1)
            Dim Nome_Regione As String = IIf(rs.Item("NomeRegione") IsNot Nothing, rs.Item("NomeRegione"), "")

            Dim Regione As Regione

            If (ID_REGIONE = -1) Then
                Regione = New RegioneNulla
            Else
                Regione = New Regione
                Regione.Key = New Key(ID_REGIONE)
                Regione.Descrizione = Nome_Regione
            End If





            iscrizione.Anno = IIf(rs.Item("Anno") IsNot Nothing, rs.Item("ANNO"), 0)
            iscrizione.Territorio = Territorio
            iscrizione.Categoria = Categoria
            iscrizione.ParentExport = Export
            iscrizione.Worker = WORKER
            iscrizione.DenormalizedData.NomeNazioneNascita = IIf(rs.Item("nazionaLavoratore") IsNot Nothing, rs.Item("nazionaLavoratore"), "")
            iscrizione.Regione = Regione
            iscrizione.Province = PROVINCIA



            Return iscrizione
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Iscrizione con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function


    'Protected Overrides Function LoadHashTableDataFromDatareader(ByVal rs As IDataReader) As Hashtable

    '    Dim DataHash As New Hashtable
    '    Dim int As Integer = 0
    '    For I As Int32 = 0 To rs.FieldCount - 1
    '        Dim name As String = rs.GetName(I)
    '        Dim Value As Object = Nothing


    '        'a causa di un bug nella libreria mysqldrivercs per i campi decimal e double sono
    '        'costretto a questo artifizio poichè non vengono lette le cifre dopo la virgola

    '        If name = "Quota" Then
    '            Value = New Decimal(rs.GetDecimal(int)) / 100
    '        Else
    '            Value = IIf(IsDBNull(rs(name)), Nothing, rs(name))
    '        End If

    '        DataHash.Add(name, Value)
    '        int = int + 1
    '    Next
    '    Return DataHash

    'End Function






    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim iscrizione As Subscription = DirectCast(Item, Subscription)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = iscrizione.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = iscrizione.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = iscrizione.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = iscrizione.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = iscrizione.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = iscrizione.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = ""
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@nomnaz"
            param.Value = iscrizione.DenormalizedData.NomeNazioneNascita
            Cmd.Parameters.Add(param)

            '& "nazionaLavoratore = @nomnaz, " _
            '  & "nomeCategoria = @nomcat, " _
            '  & "categoryId = @catid, " _
            '  & "territorioId = @comid " _

            param = Cmd.CreateParameter
            param.ParameterName = "@nomcat"
            param.Value = iscrizione.Categoria.Alias
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@catid"
            param.Value = CLng(iscrizione.Categoria.Id)
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@comid"
            param.Value = CLng(iscrizione.Territorio.Id)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto iscrizione." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim iscrizione As Subscription = DirectCast(Item, Subscription)




            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@idl"
            param.Value = iscrizione.Worker.Id
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@idi"
            param.Value = iscrizione.ParentExport.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idp"
            param.Value = iscrizione.Province.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmp"
            param.Value = iscrizione.Province.Descrizione
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idr"
            param.Value = iscrizione.Regione.Id
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@nmr"
            param.Value = iscrizione.Regione.Descrizione
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ann"
            param.Value = ""
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@nomnaz"
            param.Value = iscrizione.DenormalizedData.NomeNazioneNascita
            Cmd.Parameters.Add(param)

            '& "nazionaLavoratore = @nomnaz, " _
            '  & "nomeCategoria = @nomcat, " _
            '  & "categoryId = @catid, " _
            '  & "territorioId = @comid " _

            param = Cmd.CreateParameter
            param.ParameterName = "@nomcat"
            param.Value = iscrizione.Categoria.Alias
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@catid"
            param.Value = CLng(iscrizione.Categoria.Id)
            Cmd.Parameters.Add(param)



            param = Cmd.CreateParameter
            param.ParameterName = "@comid"
            param.Value = CLng(iscrizione.Territorio.Id)
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = iscrizione.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto iscrizione." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class



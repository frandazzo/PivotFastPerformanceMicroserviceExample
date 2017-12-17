
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Imports WIN.BASEREUSE.AbstractPersona
Imports UilDBIscritti.Domain.Workers

Public Class MapperWorker
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from lavoratori"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from lavoratori where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into lavoratori (CodiceFiscale, Nome, Cognome, Id_Nazione, NomeNazione, Telefono, Mail, " _
                & "UltimaModifica, UltimaProvinciaAdAggiornare) " _
                & "values (@cdf, @nom, @cog, @idn, @nmn,  @tel, @mai, @ulm, @ulp)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE lavoratori SET " _
                & "CodiceFiscale = @cdf, " _
                & "Nome = @nom, " _
                & "Cognome = @cog, " _
                & "Id_Nazione = @idn, " _
                & "NomeNazione = @nmn, " _
                & "Telefono = @tel, " _
                & "Mail = @mai, " _
                & "UltimaModifica = @ulm, " _
                & "UltimaProvinciaAdAggiornare = @ulp " _
                & "WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from lavoratori where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), Worker)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try
            Dim utente As New Worker
            utente.Key = Key

            utente.CodiceFiscale = rs.Item("CodiceFiscale")
            utente.Nome = IIf(rs.Item("Nome") IsNot Nothing, rs.Item("Nome"), "")
            utente.Cognome = rs.Item("Cognome")
            'utente.Sesso = IIf(rs.Item("Sesso").ToString = "FEMMINA", Sex.Femmina, Sex.Maschio)


            'utente.DataNascita = IIf(rs.Item("DataNascita") IsNot Nothing, rs.Item("DataNascita"), New DateTime(1800, 1, 1))


            'utente.Residenza.Via = IIf(rs.Item("Indirizzo") IsNot Nothing, rs.Item("Indirizzo"), "")
            'utente.Residenza.Cap = IIf(rs.Item("Cap") IsNot Nothing, rs.Item("Cap"), "")
            utente.Comunicazione.Cellulare1 = IIf(rs.Item("Telefono") IsNot Nothing, rs.Item("Telefono"), "")
            utente.Comunicazione.Mail = IIf(rs.Item("Mail") IsNot Nothing, rs.Item("Mail"), "")

            utente.ModificatoDa = IIf(rs.Item("UltimaProvinciaAdAggiornare") IsNot Nothing, rs.Item("UltimaProvinciaAdAggiornare"), "")
            utente.DataModifica = IIf(rs.Item("UltimaModifica") IsNot Nothing, rs.Item("UltimaModifica"), DateTime.Now)


            'materializzo la nazione in questo modo per 
            'evitare numerose query e velocizzare il processo di materializzazione


            Dim ID_NAZIONE As Int32 = IIf(rs.Item("Id_Nazione") IsNot Nothing, rs.Item("Id_Nazione"), -1)
            Dim Nome_Nazione As String = IIf(rs.Item("NomeNazione") IsNot Nothing, rs.Item("NomeNazione"), "")

            Dim NAZIONE As Nazione

            If (ID_NAZIONE = -1) Then
                NAZIONE = New NazioneNulla
            Else
                NAZIONE = New Nazione
                NAZIONE.Key = New Key(ID_NAZIONE)
                NAZIONE.Descrizione = Nome_Nazione
            End If


            'Dim ID_PROVINCIA As Int32 = IIf(rs.Item("Id_Provincia") IsNot Nothing, rs.Item("Id_Provincia"), -1)
            'Dim Nome_Provincia As String = IIf(rs.Item("NomeProvincia") IsNot Nothing, rs.Item("NomeProvincia"), "")

            'Dim PROVINCIA As Provincia

            'If (ID_PROVINCIA = -1) Then
            '    PROVINCIA = New ProvinciaNulla
            'Else
            '    PROVINCIA = New Provincia
            '    PROVINCIA.Key = New Key(ID_PROVINCIA)
            '    PROVINCIA.Descrizione = Nome_Provincia
            'End If



            'Dim ID_COMUNE As Int32 = IIf(rs.Item("Id_Comune") IsNot Nothing, rs.Item("Id_Comune"), -1)
            'Dim Nome_Comune As String = IIf(rs.Item("NomeComune") IsNot Nothing, rs.Item("NomeComune"), "")

            'Dim COMUNE As Comune

            'If (ID_COMUNE = -1) Then
            '    COMUNE = New ComuneNullo
            'Else
            '    COMUNE = New Comune
            '    COMUNE.Key = New Key(ID_COMUNE)
            '    COMUNE.Descrizione = Nome_Comune
            'End If




            'Dim ID_PROVINCIA_R As Int32 = IIf(rs.Item("Id_Provincia_Residenza") IsNot Nothing, rs.Item("Id_Provincia_Residenza"), -1)
            'Dim Nome_Provincia_r As String = IIf(rs.Item("NomeProvinciaResidenza") IsNot Nothing, rs.Item("NomeProvinciaResidenza"), "")



            'Dim PROVINCIA_R As Provincia

            'If (ID_PROVINCIA_R = -1) Then
            '    PROVINCIA_R = New ProvinciaNulla
            'Else
            '    PROVINCIA_R = New Provincia
            '    PROVINCIA_R.Key = New Key(ID_PROVINCIA_R)
            '    PROVINCIA_R.Descrizione = Nome_Provincia_r
            'End If



            'Dim ID_COMUNE_R As Int32 = IIf(rs.Item("Id_Comune_Residenza") IsNot Nothing, rs.Item("Id_Comune_Residenza"), -1)
            'Dim Nome_Comune_r As String = IIf(rs.Item("NomeComuneResidenza") IsNot Nothing, rs.Item("NomeComuneResidenza"), "")

            'Dim COMUNE_R As Comune

            'If (ID_COMUNE_R = -1) Then
            '    COMUNE_R = New ComuneNullo
            'Else
            '    COMUNE_R = New Comune
            '    COMUNE_R.Key = New Key(ID_COMUNE_R)
            '    COMUNE_R.Descrizione = Nome_Comune_r
            'End If


            utente.Nazionalita = NAZIONE
            'utente.ProvinciaNascita = PROVINCIA
            'utente.ComuneNascita = COMUNE
            'utente.Residenza.Comune = COMUNE_R
            'utente.Residenza.Provincia = PROVINCIA_R


            Return utente
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto Lavoratore con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As Worker = DirectCast(Item, Worker)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@cdf"
            param.Value = lavoratore.CodiceFiscale.ToUpper()
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nom"
            If String.IsNullOrEmpty(lavoratore.Nome) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Nome
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cog"
            param.Value = lavoratore.Cognome
            Cmd.Parameters.Add(param)

            'param = Cmd.CreateParameter
            'param.ParameterName = "@nco"
            'param.Value = lavoratore.CompleteName
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@dan"
            'If lavoratore.DataNascita = Date.MinValue Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.DataNascita.Date
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@ses"
            'param.Value = lavoratore.Sesso.ToString
            'Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idn"
            If lavoratore.Nazionalita.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = lavoratore.Nazionalita.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmn"
            If lavoratore.Nazionalita.Id = -1 Then
                param.Value = ""
            Else
                param.Value = lavoratore.Nazionalita.Descrizione
            End If
            Cmd.Parameters.Add(param)




            'param = Cmd.CreateParameter
            'param.ParameterName = "@idp"
            'If lavoratore.ProvinciaNascita.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.ProvinciaNascita.Id
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmp"
            'If lavoratore.ProvinciaNascita.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.ProvinciaNascita.Descrizione
            'End If
            'Cmd.Parameters.Add(param)




            'param = Cmd.CreateParameter
            'param.ParameterName = "@idc"
            'If lavoratore.ComuneNascita.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.ComuneNascita.Id
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmc"
            'If lavoratore.ComuneNascita.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.ComuneNascita.Descrizione
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@idpr"
            'If lavoratore.Residenza.Provincia.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.Residenza.Provincia.Id
            'End If
            'Cmd.Parameters.Add(param)

            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmpr"
            'If lavoratore.Residenza.Provincia.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Provincia.Descrizione
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@idcr"
            'If lavoratore.Residenza.Comune.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.Residenza.Comune.Id
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmcr"
            'If lavoratore.Residenza.Comune.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Comune.Descrizione
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@ind"
            'If String.IsNullOrEmpty(lavoratore.Residenza.Via) Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Via
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@cap"
            'If String.IsNullOrEmpty(lavoratore.Residenza.Cap) Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Cap
            'End If
            'Cmd.Parameters.Add(param)






            param = Cmd.CreateParameter
            param.ParameterName = "@tel"
            If String.IsNullOrEmpty(lavoratore.Comunicazione.Cellulare1) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Comunicazione.Cellulare1
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            If String.IsNullOrEmpty(lavoratore.Comunicazione.Mail) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Comunicazione.Mail
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ulm"
            If lavoratore.DataModifica = Date.MinValue Then
                param.Value = DateTime.Now
            Else
                param.Value = lavoratore.DataModifica
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ulp"
            If String.IsNullOrEmpty(lavoratore.ModificatoDa) Then
                param.Value = ""
            Else
                param.Value = lavoratore.ModificatoDa
            End If
            Cmd.Parameters.Add(param)


        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto Lavoratore." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim lavoratore As Worker = DirectCast(Item, Worker)





            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@cdf"
            param.Value = lavoratore.CodiceFiscale.ToUpper()
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nom"
            If String.IsNullOrEmpty(lavoratore.Nome) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Nome
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cog"
            param.Value = lavoratore.Cognome
            Cmd.Parameters.Add(param)

            'param = Cmd.CreateParameter
            'param.ParameterName = "@nco"
            'param.Value = lavoratore.CompleteName
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@dan"
            'If lavoratore.DataNascita = Date.MinValue Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.DataNascita.Date
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@ses"
            'param.Value = lavoratore.Sesso.ToString
            'Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@idn"
            If lavoratore.Nazionalita.Id = -1 Then
                param.Value = DBNull.Value
            Else
                param.Value = lavoratore.Nazionalita.Id
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@nmn"
            If lavoratore.Nazionalita.Id = -1 Then
                param.Value = ""
            Else
                param.Value = lavoratore.Nazionalita.Descrizione
            End If
            Cmd.Parameters.Add(param)




            'param = Cmd.CreateParameter
            'param.ParameterName = "@idp"
            'If lavoratore.ProvinciaNascita.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.ProvinciaNascita.Id
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmp"
            'If lavoratore.ProvinciaNascita.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.ProvinciaNascita.Descrizione
            'End If
            'Cmd.Parameters.Add(param)




            'param = Cmd.CreateParameter
            'param.ParameterName = "@idc"
            'If lavoratore.ComuneNascita.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.ComuneNascita.Id
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmc"
            'If lavoratore.ComuneNascita.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.ComuneNascita.Descrizione
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@idpr"
            'If lavoratore.Residenza.Provincia.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.Residenza.Provincia.Id
            'End If
            'Cmd.Parameters.Add(param)

            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmpr"
            'If lavoratore.Residenza.Provincia.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Provincia.Descrizione
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@idcr"
            'If lavoratore.Residenza.Comune.Id = -1 Then
            '    param.Value = DBNull.Value
            'Else
            '    param.Value = lavoratore.Residenza.Comune.Id
            'End If
            'Cmd.Parameters.Add(param)



            'param = Cmd.CreateParameter
            'param.ParameterName = "@nmcr"
            'If lavoratore.Residenza.Comune.Id = -1 Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Comune.Descrizione
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@ind"
            'If String.IsNullOrEmpty(lavoratore.Residenza.Via) Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Via
            'End If
            'Cmd.Parameters.Add(param)


            'param = Cmd.CreateParameter
            'param.ParameterName = "@cap"
            'If String.IsNullOrEmpty(lavoratore.Residenza.Cap) Then
            '    param.Value = ""
            'Else
            '    param.Value = lavoratore.Residenza.Cap
            'End If
            'Cmd.Parameters.Add(param)






            param = Cmd.CreateParameter
            param.ParameterName = "@tel"
            If String.IsNullOrEmpty(lavoratore.Comunicazione.Cellulare1) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Comunicazione.Cellulare1
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@mai"
            If String.IsNullOrEmpty(lavoratore.Comunicazione.Mail) Then
                param.Value = ""
            Else
                param.Value = lavoratore.Comunicazione.Mail
            End If
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@ulm"
            If lavoratore.DataModifica = Date.MinValue Then
                param.Value = DateTime.Now
            Else
                param.Value = lavoratore.DataModifica
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@ulp"
            If String.IsNullOrEmpty(lavoratore.ModificatoDa) Then
                param.Value = ""
            Else
                param.Value = lavoratore.ModificatoDa
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = lavoratore.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto Lavoratore." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class


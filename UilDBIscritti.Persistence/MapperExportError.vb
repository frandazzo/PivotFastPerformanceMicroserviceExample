
Imports WIN.TECHNICAL.PERSISTENCE
Imports WIN.BASEREUSE

Imports WIN.BASEREUSE.AbstractPersona

Imports UilDBIscritti.Domain.ValidationSubsystem

Public Class MapperExportError
    Inherits AbstractRDBMapper





    Public Sub New()
        MyBase.Cache = New PersistentObjectCache(True)
        m_IsAutoIncrementID = True
    End Sub




#Region "Istruzioni Sql"

    Protected Overrides Function FindAllStatement() As String
        Return "Select * from erroriimportazione"
    End Function

    Protected Overrides Function FindByKeyStatement() As String
        Return "Select * from erroriimportazione where Id = @Id"
    End Function

    Protected Overrides Function InsertStatement() As String
        Return "Insert into erroriimportazione (Data, TipoErrore, FileLogPath, ErrorMessage, " _
                & "ContieneErroriTraccia, ContieneErroriLavoratori,ExportNumber) " _
                & "values (@dat, @tpe, @flp, @erm, @cet, @cel, @exp)"
    End Function

    Protected Overrides Function UpdateStatement() As String
        Return "UPDATE erroriimportazione SET " _
                & "Data = @dat, " _
                & "TipoErrore = @tpe, " _
                & "FileLogPath = @flp, " _
                & "ErrorMessage = @erm, " _
                & "ContieneErroriTraccia = @cet, " _
                & "ContieneErroriLavoratori = @cel, " _
                & "ExportNumber = @exp   WHERE (ID = @Id )"
    End Function

    Protected Overrides Function DeleteStatement() As String
        Return "Delete from erroriimportazione where Id = @Id"
    End Function

    Protected Overrides Function FindNextKeyStatement() As String
        Return ""
    End Function

#End Region


#Region "Metodi per la ricerca dell'oggetto secondo l'id proposto"

    Public Overrides Function FindObjectById(ByVal Id As Integer) As AbstractPersistenceObject

        Return DirectCast(MyBase.FindByKey(New Key(Id)), ExportError)


    End Function
    Protected Overloads Overrides Function DoLoad(ByVal Key As Key, ByVal rs As System.Collections.Hashtable) As AbstractPersistenceObject
        Try





            Dim d As DateTime = IIf(rs.Item("Data") IsNot Nothing, rs.Item("Data"), DateTime.Now.Date)
            Dim ApplicationErrorMessage As String = IIf(rs.Item("ErrorMessage") IsNot Nothing, rs.Item("ErrorMessage"), "")
            Dim t As ErrorType = [Enum].Parse(GetType(ErrorType), rs.Item("TipoErrore").ToString)
            Dim filePath As String = IIf(rs.Item("FileLogPath") IsNot Nothing, rs.Item("FileLogPath"), "")
            Dim cel As Boolean = IIf(rs.Item("ContieneErroriLavoratori").ToString = "False", False, True)
            Dim cet As Boolean = IIf(rs.Item("ContieneErroriTraccia").ToString = "False", False, True)
            Dim exp As Int32 = IIf(rs.Item("ExportNumber") IsNot Nothing, rs.Item("ExportNumber"), 0)

            Dim err As New ExportError(t, d, filePath, ApplicationErrorMessage, cel, cet, exp)
            err.Key = Key

            Return err
        Catch ex As Exception
            Throw New Exception("Impossibile caricare l'oggetto errore importazione con Id = " & Key.LongValue & ". " & vbCrLf & ex.Message)
        End Try
    End Function









    'Protected Overrides Function GetCommand(ByVal CommandText As String) As System.Data.IDbCommand
    '    Return DBTypeUtils.GetCommand(Me.m_PersistentService.ServiceName, CommandText, DBConnectionManager.Instance.GetCurrentConnection)
    'End Function

#End Region



    Protected Overrides Sub LoadInsertCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As ExportError = DirectCast(Item, ExportError)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@dat"
            param.Value = doc.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@tpe"
            param.Value = doc.ErrorType.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@flp"
            If String.IsNullOrEmpty(doc.TraceExportFilePath) Then
                param.Value = ""
            Else
                param.Value = doc.TraceExportFilePath
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@erm"
            If String.IsNullOrEmpty(doc.ApplicationErrorMessage) Then
                param.Value = ""
            Else
                param.Value = doc.ApplicationErrorMessage
            End If
            Cmd.Parameters.Add(param)




            param = Cmd.CreateParameter
            param.ParameterName = "@cet"
            param.Value = doc.ContainsTraceError.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cel"
            param.Value = doc.ContainsWorkerError.ToString
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@exp"
            param.Value = doc.ExportNumber
            Cmd.Parameters.Add(param)




        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di inserimento dell'oggetto  errore importazione ." & vbCrLf & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub LoadUpdateCommandParameters(ByVal Item As AbstractPersistenceObject, ByVal Cmd As System.Data.IDbCommand)
        Try
            Dim doc As ExportError = DirectCast(Item, ExportError)



            Dim param As IDbDataParameter = Cmd.CreateParameter
            param.ParameterName = "@dat"
            param.Value = doc.Date
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@tpe"
            param.Value = doc.ErrorType.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@flp"
            If String.IsNullOrEmpty(doc.TraceExportFilePath) Then
                param.Value = ""
            Else
                param.Value = doc.TraceExportFilePath
            End If
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@erm"
            If String.IsNullOrEmpty(doc.ApplicationErrorMessage) Then
                param.Value = ""
            Else
                param.Value = doc.ApplicationErrorMessage
            End If
            Cmd.Parameters.Add(param)




            param = Cmd.CreateParameter
            param.ParameterName = "@cet"
            param.Value = doc.ContainsTraceError.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@cel"
            param.Value = doc.ContainsWorkerError.ToString
            Cmd.Parameters.Add(param)

            param = Cmd.CreateParameter
            param.ParameterName = "@exp"
            param.Value = doc.ExportNumber
            Cmd.Parameters.Add(param)


            param = Cmd.CreateParameter
            param.ParameterName = "@Id"
            param.Value = doc.Key.Value(0)
            Cmd.Parameters.Add(param)

        Catch ex As Exception
            Throw New Exception("Impossibile caricare il comando di aggiornamento dell'oggetto  errore importazione ." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class




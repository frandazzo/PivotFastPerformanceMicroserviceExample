Imports WIN.TECHNICAL.PERSISTENCE

Public Class PersistenceFacade
    Inherits AbstractPersistenceFacade

    Public Sub New(ByVal ServiceName As String, ByVal ConnectionString As String, ByVal CacheSize As Int32)
        MyBase.New(ServiceName, ConnectionString, CacheSize)
    End Sub


#Region "Metodi di inizializzazione del servizio"


    ''' <summary>
    ''' Metodo usato per l'inizializzazione del servizio di persistenza. Vengono create le connessioni nel
    ''' ConnectionDBManager. Vengono inizializzati tutti i mapper del sottosistema.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Initialize()
        ' DBConnectionManager.Instance.AddNewDBConnection("MainConnection", DBConfigSettings.Instance.ConnectionString, Me)
        Me.m_MapperFinder = New PersistenceMapperRegistry()
        Me.m_MapperFinder.SetPersistentService(Me)
    End Sub

#End Region













End Class

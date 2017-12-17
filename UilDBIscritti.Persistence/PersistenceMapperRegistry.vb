Imports WIN.TECHNICAL.PERSISTENCE

Public Class PersistenceMapperRegistry
    Implements IMapperFinder

    'Private Shared m_Instance As PersistenceMapperRegistry
    Private m_PersistentService As IPersistenceFacade
    Private Mappers As New Hashtable
    'Shared Function Instance() As PersistenceMapperRegistry
    '    If m_Instance Is Nothing Then
    '        m_Instance = New PersistenceMapperRegistry
    '    End If
    '    Return m_Instance
    'End Function


    'Shared Sub ReInitialize()
    '   m_Instance = New PersistenceMapperRegistry
    'End Sub
    Public Sub SetPersistentService(ByVal PersistentService As IPersistenceFacade) Implements IMapperFinder.SetPersistentService
        m_PersistentService = PersistentService
    End Sub



    Public Function GetMapperByName(ByVal MapperName As String) As IMapper Implements IMapperFinder.GetMapperByName
        Dim map As Object
        Dim ObjectTypeName As String = Right(MapperName, (MapperName.Length - 6))
        Try
            If Mappers.ContainsKey(ObjectTypeName) Then Return Mappers.Item(ObjectTypeName)
            map = FindMapperByName(MapperName, ObjectTypeName)
            If Not map Is Nothing Then
                Return map
            Else
                Throw New Exception("Mapper non trovato")
            End If
        Catch ex As Exception
            Throw New Exception("Impossibile ottenere il mapper richiesto" & vbCrLf & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Metodo per ricercare il mapper dato il nome del tipo dell'oggetto di dominio mappato. Esso ricerca il mapper 
    ''' nella hash table interna attraverso il nome del tipo dell'oggetto di dominio e lo restituisce se
    ''' lo trova, altrimenti lo ricerca nell'assembly corrente. Se la ricerca va a buon fine crea un'istanza del tipo trovato, 
    ''' la aggiunge alla hash table e la restituisce. Se non trova nulla solleva un'eccezione.
    ''' La chiave di ricerca nell'assembly è costituita dal nome del tipo dell'oggetto
    ''' di dominio preceduta dal prefisso "Mapper"
    ''' </summary>
    ''' <param name="ObjectTypeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMapperByObjectTypeName(ByVal ObjectTypeName As String) As IMapper Implements IMapperFinder.GetMapperByObjectTypeName
        Dim map As Object
        Dim MapperName As String = "Mapper" & ObjectTypeName
        Try
            MapperName = MapperNameFinder.GetRealMapperName(MapperName)
            If Mappers.ContainsKey(ObjectTypeName) Then Return Mappers.Item(ObjectTypeName)
            map = FindMapperByName(MapperName, ObjectTypeName)
            If Not map Is Nothing Then
                Return map
            Else
                Throw New Exception("Mapper non trovato")
            End If
        Catch ex As Exception
            Throw New Exception("Impossibile ottenere il mapper richiesto" & vbCrLf & ex.Message)
        End Try
    End Function
    Private Function FindMapperByName(ByVal MapperName As String, ByVal ObjectTypeName As String) As Object
        Dim MapperObj As Object
        Dim ty As Type
        Dim asm As Reflection.Assembly
        Try
            asm = Reflection.Assembly.GetExecutingAssembly
            For Each ty In asm.GetTypes
                If ty.Name = MapperName Then
                    MapperObj = Activator.CreateInstance(ty)
                    DirectCast(MapperObj, AbstractPersistentMapper).SetPersistentService(m_PersistentService)
                    Mappers.Add(ObjectTypeName, MapperObj)
                    Return MapperObj
                End If
            Next
            Throw New Exception("Il mapper desiderato non esiste nell'assembly corrente")
        Catch ex As Exception
            Throw New Exception("Impossibile trovare il mapper " & MapperName & vbCrLf & ex.Message)
        End Try
    End Function

    Public ReadOnly Property DBMappers As System.Collections.IEnumerable Implements IMapperFinder.DBMappers
        Get
            Return Mappers.Values
        End Get
    End Property
End Class

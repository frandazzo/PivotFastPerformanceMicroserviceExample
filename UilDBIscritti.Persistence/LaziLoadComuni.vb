Imports WIN.TECHNICAL.PERSISTENCE

Public Class LaziLoadComuni
    Inherits VirtualLazyList

    Public Enum Type
        Regioni
        Provincie
    End Enum

    Private ListLoader As MapperComune
    Private m_Id As Int32
    Private m_mode As Type
    Private m_PersistenceMapperRegistry As PersistenceMapperRegistry

    Public Sub New(ByVal Id As Int32, ByVal searchType As Type, ByVal registry As PersistenceMapperRegistry)
        m_Id = Id
        m_mode = searchType
        m_PersistenceMapperRegistry = registry
        ListLoader = m_PersistenceMapperRegistry.GetMapperByName("MapperComune")
    End Sub



    Protected Overrides Function GetList() As System.Collections.ArrayList
        If Source Is Nothing Then

            Source = GetElementList()
        End If
        Return Source
    End Function

    Private Function GetElementList() As IList
        Dim ps As IPersistenceFacade = DataAccessServices.Instance.PersistenceFacade
        Dim query As Query = ps.CreateNewQuery("MapperComune")
        query.SetTable("TB_COMUNI")


        Dim column As String
        If m_mode = Type.Provincie Then
            column = "ID_TB_PROVINCIE"
        Else
            column = "ID_TB_REGIONI"
        End If

        Dim crit As AbstractBoolCriteria = Criteria.Equal(column, m_Id)

        Dim ord As AbstractBoolCriteria = New OrderByCriteria

        ord.AddCriteria(Criteria.SortCriteria("Descrizione", True))


        query.AddWhereClause(crit)

        query.AddOrderByClause(ord)

        Return query.Execute(ps)


        'If m_mode = Type.Provincie Then
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_PROVINCIE = " & m_Id & "  order by Descrizione")
        'Else
        '   Return ListLoader.FindByQuery("Select * from TB_COMUNI where ID_TB_REGIONI = " & m_Id & "  order by Descrizione")
        'End If

    End Function
End Class

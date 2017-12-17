Imports WIN.TECHNICAL.PERSISTENCE

Public Class LazyLoadProvincie
    Inherits VirtualLazyList

    Private ListLoader As MapperProvincia
    Private m_Id_Regione As Int32
   Private m_PersistenceMapperRegistry As PersistenceMapperRegistry

    Public Sub New(ByVal Id_Regione As Int32, ByVal registry As PersistenceMapperRegistry)
        m_Id_Regione = Id_Regione
        m_PersistenceMapperRegistry = registry
        ListLoader = m_PersistenceMapperRegistry.GetMapperByName("MapperProvincia")
    End Sub



    Protected Overrides Function GetList() As System.Collections.ArrayList
        If Source Is Nothing Then
            Source = GetElementList() ' ListLoader.FindByQuery("Select * from TB_PROVINCIE where ID_TB_REGIONI = " & m_Id_Regione & "  order by Descrizione")
        End If
        Return Source
    End Function

    Private Function GetElementList() As IList
        Dim ps As IPersistenceFacade = DataAccessServices.Instance.PersistenceFacade
        Dim query As Query = ps.CreateNewQuery("MapperProvincia")
        query.SetTable("TB_PROVINCIE")


        Dim column As String = "ID_TB_REGIONI"


        Dim crit As AbstractBoolCriteria = Criteria.Equal(column, m_Id_Regione)

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

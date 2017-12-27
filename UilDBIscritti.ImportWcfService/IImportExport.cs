using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscritti.ImportWcfService
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IImportExport
    {

        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: aggiungere qui le operazioni del servizio
        [OperationContract]
        string ImportData(ExportTrace trace);

        [OperationContract]
        IList<string> GetCategories();


        [OperationContract]
        IList<string> GetTerritori();

        [OperationContract]
        void RetrieveDataFromCoda();

        [OperationContract]
        bool UserIsValid(string username, string password, string category);


    }


    // Per aggiungere tipi compositi alle operazioni del servizio utilizzare un contratto di dati come descritto nell'esempio seguente.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

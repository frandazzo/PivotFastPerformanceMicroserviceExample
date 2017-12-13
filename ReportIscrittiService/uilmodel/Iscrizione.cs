using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportIscrittiService.uilmodel
{
    [Table("uildbiscritti.iscrizioni")]
    public class Iscrizione
    {
        public int ID { get; set; }

        public int Id_Lavoratore { get; set; }
        public int Id_Importazione { get; set; }

        public int Id_Provincia { get; set; }
        public string NomeProvincia { get; set; }

        public int Id_Regione { get; set; }
        public string NomeRegione { get; set; }

        public long categoryId { get; set; }
        public string nomeCategoria { get; set; }
        public string nazioneLavoratore { get; set; }
       

        public int Anno { get; set; }


        public long territorioId { get; set; }
        public Territorio Territorio { get; set; }



    }
}

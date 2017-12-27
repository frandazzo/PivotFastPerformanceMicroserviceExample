using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IscrittiMicroService.uilmodel
{
    [Table("uildbiscritti.fenealweb_company")]
    public class Territorio
    {
        public long id { get; set; }
        public string description { get; set; }
        public string alias { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Territorio()
        {
            Iscrizioni = new HashSet<Iscrizione>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Iscrizione> Iscrizioni { get; set; }
    }
}

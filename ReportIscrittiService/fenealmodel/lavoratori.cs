namespace ReportIscrittiService.fenealmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feneal.lavoratori")]
    public partial class lavoratori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lavoratori()
        {
            iscrizioni = new HashSet<iscrizioni>();
        }

        public int ID { get; set; }

        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [StringLength(40)]
        public string Nome { get; set; }

        [StringLength(80)]
        public string Cognome { get; set; }

        [StringLength(82)]
        public string NomeCompleto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string Sesso { get; set; }

        public int? Id_Nazione { get; set; }

        [StringLength(50)]
        public string NomeNazione { get; set; }

        public int? Id_Provincia { get; set; }

        [StringLength(50)]
        public string NomeProvincia { get; set; }

        public int? Id_Comune { get; set; }

        [StringLength(70)]
        public string NomeComune { get; set; }

        public int? Id_Provincia_Residenza { get; set; }

        [StringLength(50)]
        public string NomeProvinciaResidenza { get; set; }

        public int? Id_Comune_Residenza { get; set; }

        [StringLength(70)]
        public string NomeComuneResidenza { get; set; }

        [StringLength(200)]
        public string Indirizzo { get; set; }

        [StringLength(10)]
        public string Cap { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        public DateTime? UltimaModifica { get; set; }

        [StringLength(50)]
        public string UltimaProvinciaAdAggiornare { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iscrizioni> iscrizioni { get; set; }
    }
}

namespace IscrittiMicroService.fenealmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feneal.iscrizioni")]
    public partial class iscrizioni
    {
        public int ID { get; set; }

      


        public int? Id_Lavoratore { get; set; }

        public int? Id_Importazione { get; set; }

        public int? Id_Provincia { get; set; }

        [StringLength(50)]
        public string NomeProvincia { get; set; }

        public int? Id_Regione { get; set; }

        [StringLength(50)]
        public string NomeRegione { get; set; }

        [StringLength(30)]
        public string Settore { get; set; }

        [StringLength(30)]
        public string Ente { get; set; }

        [StringLength(30)]
        public string Piva { get; set; }

        [StringLength(160)]
        public string Azienda { get; set; }

        [StringLength(50)]
        public string Livello { get; set; }

        public double? Quota { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string TipoPeriodo { get; set; }

        public int? Periodo { get; set; }

        public int? Anno { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataInizio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataFine { get; set; }

        [StringLength(100)]
        public string Contratto { get; set; }

        [StringLength(120)]
        public string NomeCompleto { get; set; }

        public int? AnnoNascita { get; set; }

        [StringLength(60)]
        public string NomeNazioneNascita { get; set; }

        [StringLength(50)]
        public string NomeProvinciaNascita { get; set; }

        [StringLength(50)]
        public string NomeComuneNascita { get; set; }

        [StringLength(10)]
        public string Sesso { get; set; }

        [StringLength(30)]
        public string Struttura { get; set; }

        [StringLength(30)]
        public string Area { get; set; }

        public virtual lavoratori lavoratori { get; set; }
    }
}

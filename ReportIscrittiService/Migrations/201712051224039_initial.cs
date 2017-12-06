namespace ReportIscrittiService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "feneal.iscrizioni",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Id_Lavoratore = c.Int(),
                        Id_Importazione = c.Int(),
                        Id_Provincia = c.Int(),
                        NomeProvincia = c.String(maxLength: 50, unicode: false),
                        Id_Regione = c.Int(),
                        NomeRegione = c.String(maxLength: 50, unicode: false),
                        Settore = c.String(maxLength: 30, unicode: false),
                        Ente = c.String(maxLength: 30, unicode: false),
                        Piva = c.String(maxLength: 30, unicode: false),
                        Azienda = c.String(maxLength: 160, unicode: false),
                        Livello = c.String(maxLength: 50, unicode: false),
                        Quota = c.Double(),
                        TipoPeriodo = c.String(storeType: "enum"),
                        Periodo = c.Int(),
                        Anno = c.Int(),
                        DataInizio = c.DateTime(storeType: "date"),
                        DataFine = c.DateTime(storeType: "date"),
                        Contratto = c.String(maxLength: 100, unicode: false),
                        NomeCompleto = c.String(maxLength: 120, unicode: false),
                        AnnoNascita = c.Int(),
                        NomeNazioneNascita = c.String(maxLength: 60, unicode: false),
                        NomeProvinciaNascita = c.String(maxLength: 50, unicode: false),
                        NomeComuneNascita = c.String(maxLength: 50, unicode: false),
                        Sesso = c.String(maxLength: 10, unicode: false),
                        Struttura = c.String(maxLength: 30, unicode: false),
                        Area = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("feneal.lavoratori", t => t.Id_Lavoratore)
                .Index(t => t.Id_Lavoratore);
            
            CreateTable(
                "feneal.lavoratori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodiceFiscale = c.String(maxLength: 16, unicode: false),
                        Nome = c.String(maxLength: 40, unicode: false),
                        Cognome = c.String(maxLength: 80, unicode: false),
                        NomeCompleto = c.String(maxLength: 82, unicode: false),
                        DataNascita = c.DateTime(storeType: "date"),
                        Sesso = c.String(storeType: "enum"),
                        Id_Nazione = c.Int(),
                        NomeNazione = c.String(maxLength: 50, unicode: false),
                        Id_Provincia = c.Int(),
                        NomeProvincia = c.String(maxLength: 50, unicode: false),
                        Id_Comune = c.Int(),
                        NomeComune = c.String(maxLength: 70, unicode: false),
                        Id_Provincia_Residenza = c.Int(),
                        NomeProvinciaResidenza = c.String(maxLength: 50, unicode: false),
                        Id_Comune_Residenza = c.Int(),
                        NomeComuneResidenza = c.String(maxLength: 70, unicode: false),
                        Indirizzo = c.String(maxLength: 200, unicode: false),
                        Cap = c.String(maxLength: 10, unicode: false),
                        Telefono = c.String(maxLength: 50, unicode: false),
                        UltimaModifica = c.DateTime(precision: 0),
                        UltimaProvinciaAdAggiornare = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("feneal.iscrizioni", "Id_Lavoratore", "feneal.lavoratori");
            DropIndex("feneal.iscrizioni", new[] { "Id_Lavoratore" });
            DropTable("feneal.lavoratori");
            DropTable("feneal.iscrizioni");
        }
    }
}

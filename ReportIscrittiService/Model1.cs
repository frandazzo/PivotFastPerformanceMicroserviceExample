namespace ReportIscrittiService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<iscrizioni> iscrizioni { get; set; }
        public virtual DbSet<lavoratori> lavoratori { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeProvincia)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeRegione)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Settore)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Ente)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Piva)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Azienda)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Livello)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.TipoPeriodo)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Contratto)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeNazioneNascita)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeProvinciaNascita)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.NomeComuneNascita)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Sesso)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Struttura)
                .IsUnicode(false);

            modelBuilder.Entity<iscrizioni>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.CodiceFiscale)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Cognome)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Sesso)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeNazione)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeProvincia)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeComune)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeProvinciaResidenza)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.NomeComuneResidenza)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Indirizzo)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Cap)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .Property(e => e.UltimaProvinciaAdAggiornare)
                .IsUnicode(false);

            modelBuilder.Entity<lavoratori>()
                .HasMany(e => e.iscrizioni)
                .WithOptional(e => e.lavoratori)
                .HasForeignKey(e => e.Id_Lavoratore);
        }
    }
}

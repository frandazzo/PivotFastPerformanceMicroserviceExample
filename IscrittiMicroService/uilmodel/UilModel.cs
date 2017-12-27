using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IscrittiMicroService.uilmodel
{
    public class UilModel : DbContext
    {
        public UilModel()
            : base("name=UilModel")
        {
        }

        public virtual DbSet<Iscrizione> iscrizioni { get; set; }
        public virtual DbSet<Territorio> territori { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Iscrizione>()
            //    .Property(e => e.NomeProvincia)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Iscrizione>()
            //    .Property(e => e.NomeRegione)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Iscrizione>()
            //    .Property(e => e.nazioneLavoratore)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Iscrizione>()
            //   .Property(e => e.nomeCategoria)
            //   .IsUnicode(false);


            

            //modelBuilder.Entity<Territorio>()
            //    .HasMany(e => e.Iscrizioni)
            //    .WithRequired(e => e.Territorio)
            //    .HasForeignKey(e => e.territorioId);


            modelBuilder.Entity<Iscrizione>()
               .HasRequired(e => e.Territorio)
               .WithMany(e => e.Iscrizioni)
               .HasForeignKey(e => e.territorioId);


            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.CodiceFiscale)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Cognome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeCompleto)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Sesso)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeNazione)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeProvincia)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeComune)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeProvinciaResidenza)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.NomeComuneResidenza)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Indirizzo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Cap)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.Telefono)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .Property(e => e.UltimaProvinciaAdAggiornare)
            //    .IsUnicode(false);

            //modelBuilder.Entity<lavoratori>()
            //    .HasMany(e => e.iscrizioni)
            //    .WithOptional(e => e.lavoratori)
            //    .HasForeignKey(e => e.Id_Lavoratore);
        }
    }
}

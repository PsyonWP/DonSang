using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DonSang.context.Models
{
    public partial class DonSangYJContext : DbContext
    {
        public DonSangYJContext()
        {
        }

        public DonSangYJContext(DbContextOptions<DonSangYJContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Donneur> Donneurs { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<Reponse> Reponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DonSangDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donneur>(entity =>
            {
                entity.HasKey(e => e.IdDonneur).HasName("PK__Donneur__F07704D9B8933EAC");

                entity.ToTable("Donneur");

                entity.Property(e => e.IdDonneur).HasColumnName("Id_Donneur");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.MotDePasse)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Prenom)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.IdQuestion).HasName("PK__Question__B4902467B834CA96");

                entity.ToTable("Question");

                entity.Property(e => e.IdQuestion).HasColumnName("Id_Question");
                entity.Property(e => e.Categorie)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Enonce)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.TypeQuestion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(e => e.IdQuestionnaire).HasName("PK__Question__29FBA1F74AB1951C");

                entity.ToTable("Questionnaire");

                entity.Property(e => e.IdQuestionnaire).HasColumnName("Id_Questionnaire");
                entity.Property(e => e.IdDonneur).HasColumnName("Id_Donneur");
                entity.Property(e => e.Resultat)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Statut)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDonneurNavigation).WithMany(p => p.Questionnaires)
                    .HasForeignKey(d => d.IdDonneur)
                    .HasConstraintName("FK__Questionn__Id_Do__2C3393D0");
            });

            modelBuilder.Entity<Reponse>(entity =>
            {
                entity.HasKey(e => e.IdReponse).HasName("PK__Reponse__6DC7FE87BC8E82B6");

                entity.ToTable("Reponse");

                entity.Property(e => e.IdReponse).HasColumnName("Id_Reponse");
                entity.Property(e => e.ComplementTexte)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.IdDonneur).HasColumnName("Id_Donneur");
                entity.Property(e => e.IdQuestion).HasColumnName("Id_Question");
                entity.Property(e => e.Reponse1).HasColumnName("Reponse");

                entity.HasOne(d => d.IdDonneurNavigation).WithMany(p => p.Reponses)
                    .HasForeignKey(d => d.IdDonneur)
                    .HasConstraintName("FK__Reponse__Id_Donn__286302EC");

                entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Reponses)
                    .HasForeignKey(d => d.IdQuestion)
                    .HasConstraintName("FK__Reponse__Id_Ques__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

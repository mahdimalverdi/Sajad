using Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Storage
{
    public class SajadDbContext : DbContext
    {
        public SajadDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected SajadDbContext()
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<QuestionStruct> QuestionStructs { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasKey(d => d.Id);
            modelBuilder.Entity<Paragraph>().HasKey(d => d.Id);
            modelBuilder.Entity<QuestionStruct>().HasKey(d => d.Id);
            modelBuilder.Entity<Answer>().HasKey(d => d.Id);

            modelBuilder.Entity<Document>().HasIndex(d => d.Id);
            modelBuilder.Entity<Paragraph>().HasIndex(d => d.Id);
            modelBuilder.Entity<QuestionStruct>().HasIndex(d => d.Id);
            modelBuilder.Entity<Answer>().HasIndex(d => d.Id);

            modelBuilder.Entity<Document>().HasMany(d => d.Paragraphs).WithOne().HasForeignKey(p => p.DocumentId);
            modelBuilder.Entity<Paragraph>().HasMany(d => d.Questions).WithOne().HasForeignKey(q => q.ParagraphId);
            modelBuilder.Entity<QuestionStruct>().HasMany(d => d.Answers).WithOne().HasForeignKey(a => a.QuestionStructId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

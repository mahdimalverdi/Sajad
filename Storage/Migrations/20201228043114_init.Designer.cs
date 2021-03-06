﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage;

namespace Storage.Migrations
{
    [DbContext(typeof(SajadDbContext))]
    [Migration("20201228043114_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Abstraction.Models.Answer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AnswerStart")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionStructId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionStructId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Abstraction.Models.Document", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Abstraction.Models.Paragraph", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Context")
                        .HasColumnType("TEXT");

                    b.Property<string>("DocumentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Paragraphs");
                });

            modelBuilder.Entity("Abstraction.Models.QuestionStruct", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParagraphId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Question")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParagraphId");

                    b.ToTable("QuestionStructs");
                });

            modelBuilder.Entity("Abstraction.Models.Answer", b =>
                {
                    b.HasOne("Abstraction.Models.QuestionStruct", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionStructId");
                });

            modelBuilder.Entity("Abstraction.Models.Paragraph", b =>
                {
                    b.HasOne("Abstraction.Models.Document", null)
                        .WithMany("Paragraphs")
                        .HasForeignKey("DocumentId");
                });

            modelBuilder.Entity("Abstraction.Models.QuestionStruct", b =>
                {
                    b.HasOne("Abstraction.Models.Paragraph", null)
                        .WithMany("Questions")
                        .HasForeignKey("ParagraphId");
                });

            modelBuilder.Entity("Abstraction.Models.Document", b =>
                {
                    b.Navigation("Paragraphs");
                });

            modelBuilder.Entity("Abstraction.Models.Paragraph", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Abstraction.Models.QuestionStruct", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}

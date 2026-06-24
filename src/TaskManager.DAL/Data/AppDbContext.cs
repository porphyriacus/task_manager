using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Enums;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Board> Boards { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }  

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(project => {
                project
                .HasMany(pr => pr.Boards)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

                project
                .HasOne(p => p.Owner)
                .WithMany(o => o.OwnedProjects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

                project
                .HasMany<User>(p => p.Users)
                .WithMany(u => u.AssignedProjects)
                .UsingEntity(t => t.ToTable("ProjectUsers"));

                project.HasIndex(p => p.OwnerId);

            });

            modelBuilder.Entity<Board>(board =>
            {
                board
                .HasMany(b => b.Tasks)
                .WithOne(t => t.Board)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Cascade);


                board.HasIndex(b => b.ProjectId);

            });

            modelBuilder.Entity<TaskEntity>(task =>
            {

                task
                .HasMany(t => t.Executers)
                .WithMany(u => u.ExecutedTasks)
                .UsingEntity(j => j.ToTable("TaskExecuters"));
                task
                .HasOne(t => t.Owner)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

                task
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity(j => j.ToTable("TaskTags"));

                task
                .HasMany(t => t.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

                task.HasIndex(t => t.BoardId);      // для быстрого поиска задач по доске
                task.HasIndex(t => t.OwnerId);      // для быстрого поиска задач по владельцу
                task.HasIndex(t => t.Deadline);

                task
                .Property(t => t.Priority)
                .HasConversion<string>(
                    pr => pr.ToString(),
                    v => (TaskPriority)Enum.Parse(typeof(TaskPriority), v)
                    );
            });

            modelBuilder.Entity<Tag>(tag => { 
                tag
                .HasIndex(t => t.Name).IsUnique();
            });
        }

    }
}

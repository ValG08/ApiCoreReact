using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestWebApiCore.DAL.Entities;

namespace TestWebApiCore.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public static void SeedDataBaseForUnitTest(AppDbContext context)
        {
            if (context.Notes.Any())
            {
                return;
            }

            List<Note> notes = new List<Note>()
            {
                new Note() { Id = 1, NoteMessage = "Note Message 1" },
                new Note() { Id = 2, NoteMessage = "Note Message 2" }
            };

            context.AddRange(notes);
            context.SaveChanges();
        }
    }
}

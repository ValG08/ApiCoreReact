using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestWebApiCore.DAL;

namespace TestWebApiCore.Tests
{
    public class TestDataBase
    {
        public static DbContextOptions<AppDbContext> CreateOptions<T>() where T : DbContext
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "memory" };
            var connectionString = connectionStringBuilder.ToString();

            var connection = new SqliteConnection(connectionString);

            connection.Open();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlite(connection);

            return builder.Options;
        }
    }
}

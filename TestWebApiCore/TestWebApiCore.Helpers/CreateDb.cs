using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace TestWebApiCore.Helpers
{
    public class CreateDb
    {
        private readonly string _dbCreateScript =
                     "USE master;" +
                     "IF (SELECT name FROM master.sys.databases WHERE name LIKE 'TestNoteDb') IS NULL " +
                     "BEGIN " +
                     "CREATE DATABASE TestNoteDb;" +
                     "END ";

        public void CreateDataBase
           (string fileName,
            string connectMaster,
            string connectDB)
        {
            try
            {
                using (var connection = new SqlConnection(connectMaster))
                {
                    connection.Open();
                    using (var command = new SqlCommand(_dbCreateScript, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                using (var connection = new SqlConnection(connectDB))
                {
                    connection.Open();
                    using (var sqlCommand = new SqlCommand(FileRead(fileName), connection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("ROLLBACK"))
                {
                    throw ex;
                }
            }
        }

        private string FileRead(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new NullReferenceException("filename for dbScript is null");
            }
            if (!File.Exists(fileName))
            {
                throw new Exception("file not found: " + fileName);
            }

            return File.ReadAllText(fileName, Encoding.Default);
        }
    }
}

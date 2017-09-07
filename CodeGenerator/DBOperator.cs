using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
namespace CodeGenerator
{
    class DBOperator
    {
        SQLiteConnection m_dbConnection;

        void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=Config.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public SQLiteConnection getConnection()
        {
            connectToDatabase();
            return m_dbConnection;
        }

    }
}

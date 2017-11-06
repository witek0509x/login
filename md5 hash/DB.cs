using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SQLite;

namespace md5_hash
{
    class DB
    {
        SQLiteConnection m_dbConnection;
        public DB(string source)
        {
            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;");
                m_dbConnection.Open();
            }
            catch
            {

            }
        }
        public string Compute(string txt)
        {
                using (MD5 md5Hash = MD5.Create())
                {
                    return GetMd5Hash(md5Hash, txt);
                }
            }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerifyPassword(string login, string password)
        {
            string sql;
            sql = "select password from users where login = '" + login + "'";
            string respond = Query(sql);
            using (MD5 md5Hash = MD5.Create())
            {
                return VerifyMd5Hash(md5Hash, respond, Compute(password));
            }    
        }
        public void CreateNewUser(string login, string password, string permission)
        {

        }



        public string Query(string sql)
        {
            try
            {
                string wynik = "";
                int i = 0;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    while (reader.FieldCount > i)
                    {
                        wynik += reader.GetString(i);
                        wynik += "  ";
                        i++;
                    }
                    i = 0;
                    wynik += Environment.NewLine;
                }
                return wynik;
            }
            catch
            {
                return "something went wrong";
            }
        }

    }


}


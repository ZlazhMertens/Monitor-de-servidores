using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MonitorEstadoDB
{
    class DBConect
    {
        private SqlConnection cnct;
        private string server;
        private string id;
        private string psw;
        private string query;
        private SqlCommand cmnd;

        public DBConect(string Server, string UserID, string Password, string SQLEngine)
        {
            Initialize(Server,UserID,Password,SQLEngine);
        }

        private void Initialize(string server,string id, string psw, string engine)
        {
            this.server = server;
            this.id = id;
            this.psw = psw;
            string cnctStr;
            switch (engine)
            {
                case "MY_SQL":
                    query = "Select now();";
                    cnctStr = "SERVER=" + server + ";" + "DATABASE= master;" + "UID=" + id + ";" + "PASSWORD=" + psw + ";";
                    break;
                case "SQL_SERVER":
                    query = "Select getdate();";
                    cnctStr = "Data Source=" + server + ";Initial Catalog= master;User ID=" + id + ";Password=" + psw;
                    break;
                default:
                    query = null;
                    cnctStr = null;
                    break;
            }
            cnct = new SqlConnection(cnctStr);
        }

        private bool OpenConnection()
        {
            try
            {
                cnct.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                cnct.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool validate()
        {
            if (this.OpenConnection() == true)
            {
                cmnd = new SqlCommand(query, cnct);
                SqlDataReader DR = cmnd.ExecuteReader();
                DR.Read();
                string s = DR[0].ToString();
                DR.Close();
                this.CloseConnection();
                if (s != "")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}

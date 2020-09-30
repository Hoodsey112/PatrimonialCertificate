using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    class InsertUpdateClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static DataTable LoadMotherUPDT_BTAL1(int client_id)
        {
            DataTable forUpdate = new DataTable();

            try
            {
                string query = "SELECT id," +
                                     " IF(LNAME is NULL, '', LNAME) LNAME," +
                                     " IF(FNAME is null, '', FNAME) FNAME," +
                                     " IF(MNAME is null, '', MNAME) MNAME," +
                                     " IF(BDATE = '00.00.0000', CURRENT_DATE(), BDATE) BDATE," +
                                     " IF(ADDRESS is null, '', ADDRESS) address," +
                                     " IF(SNILS is null, '', SNILS) SNILS," +
                                     " IF(SPOLICY is null, '', SPOLICY) SPOLICY," +
                                     " IF(NPOLICY is null, '', NPOLICY) NPOLICY," +
                                     " IF(TDOC is null, 0, TDOC) TDOC," +
                                     " IF(SDOC is null, '', SDOC) SDOC," +
                                     " IF(NDOC is null, '', NDOC) NDOC," +
                                     " IF(date_FORMAT(DDOC, '%d.%m.%Y') = '00.00.0000', CURRENT_DATE(), date_FORMAT(DDOC, '%d.%m.%Y')) DDOC," +
                                     " IF(ODOC is null, '', ODOC) ODOC," +
                                     " IF(SCERTIF is null, '', SCERTIF) SCERTIF," +
                                     " IF(NCERTIF is null, '', NCERTIF) NCERTIF," +
                                     " IF(date_FORMAT(DCERTIF, '%d.%m.%Y') = '00.00.0000', CURRENT_DATE(), date_FORMAT(DCERTIF, '%d.%m.%Y')) DCERTIF," +
                                     " IF(SLEAF IS NULL, '', SLEAF) SLEAF," +
                                     " IF(NLEAF IS NULL, '', NLEAF) NLEAF," +
                                     " IF(date_FORMAT(DLEAF,'%d.%m.%Y') = '00.00.0000', CURRENT_DATE(), date_FORMAT(DLEAF,'%d.%m.%Y')) DLEAF," +
                                     " CHK," +
                                     " IF(date_FORMAT(DREG,'%d.%m.%Y') = '00.00.0000', CURRENT_DATE(), date_FORMAT(DREG,'%d.%m.%Y')) DREG," +
                                     " QTW," +
                                     " MULP," +
                                     " PREM," +
                                     " PHELP" +
                              " FROM BTAL1" +
                             $" WHERE client_id = {client_id}";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    forUpdate.Load(sqlCommand.ExecuteReader());
                }

                return forUpdate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }
        public static DataTable FuncMotherUPD(int client_id)
        {
            DataTable forTableUPD = new DataTable();

            try
            {
                string query = "SELECT IF(DREG = '00.00.0000', current_date(), DREG) DREG," +     //0
                                     " DIAG," +     //1
                                     " BQNT," +     //2
                                     " LNAME," +    //3
                                     " FNAME," +    //4
                                     " MNAME," +    //5
                                     " IF(BDATE = '00.00.0000', current_date(), BDATE) BDATE," +    //6
                                     " ADDRESS," +  //7
                                     " SNILS," +    //8
                                     " SPOLICY," +  //9
                                     " NPOLICY," +  //10
                                     " TDOC," +     //11
                                     " SDOC," +     //12
                                     " NDOC," +     //13
                                     " IF(DDOC = '00.00.0000', current_date(), DDOC) DDOC," +     //14
                                     " ODOC," +     //15
                                     " SCERTIF," +  //16
                                     " NCERTIF," +  //17
                                     " IF(DCERTIF = '00.00.0000', current_date(), DCERTIF) DCERTIF," +  //18
                                     " SLEAF," +    //19
                                     " NLEAF," +    //20
                                     " IF(DLEAF = '00.00.0000', current_date(), DLEAF) DLEAF," +    //21
                                     " NCARD," +    //22
                                     " IF(DCARD = '00.00.0000', current_date(), DCARD) DCARD," +    //23
                                     " client_id," +//24
                                     " id" + //25
                              " FROM BTAL2 b2" +
                             $" WHERE client_id = {client_id}" +
                                " and deleted = 0";
                
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    forTableUPD.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();
                }
                return forTableUPD;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static DataTable FuncChildUPD(int client_id, string motherCertif)
        {
            DataTable childTable = new DataTable();

            try
            {
                string queryCH = "select id," +
                                       " SEX," +
                                       " WEIGHT," +
                                       " GROWTH," +
                                       " DIAG" +
                                " FROM CHILD" +
                               $" where mother_id = {client_id}" +
                                 $" and motherCertif = '{motherCertif}'" +
                                  " and deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(queryCH, sqlConnection);
                    childTable.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();
                }
                return childTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }
    }
}

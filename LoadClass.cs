using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    class LoadClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static DataTable LoadBeremCertif(string dateFrom, string dateTo)
        {
            DataTable CertifBerem = new DataTable();

            try
            {
                string query = "SELECT CONCAT(SCERTIF, ' ', NCERTIF) CERTIF," +
                                     " IF(DCERTIF = '00.00.0000', '', DATE_FORMAT(DCERTIF, '%d.%m.%Y')) DCERTIF," +
                                     " SNILS," +
                                     " CONCAT(LNAME, ' ', FNAME, ' ', MNAME) FIO_berem," +
                                     " DATE_FORMAT(BDATE, '%d.%m.%Y') BDATE," +
                                     " ADDRESS," +
                                     " CONCAT('Серия ', SDOC, ', номер ', NDOC, ', дата выдачи ', DATE_FORMAT(DDOC, '%d.%m.%Y'), ', выдан ', ODOC) DocType," +//" IF(SDOC <> '' and NDOC <> '' and DDOC <> '00.00.0000' and ODOC <> '', CONCAT('Серия ', SDOC, ', номер ', NDOC, ', дата выдачи ', DATE_FORMAT(DDOC, '%d.%m.%Y'), ', выдан ', ODOC), '') DocType," +
                                     " IF(SPOLICY = '', NPOLICY, CONCAT(SPOLICY, ' ', NPOLICY)) POLICY," +
                                     " IF(SLEAF = '' AND NLEAF = '', '', CONCAT(SLEAF, ' ', NLEAF, ', выдан ', DLEAF)) SICKLIST," +
                                     " DATE_FORMAT(DREG, '%d.%m.%Y') DREG," +
                                     " IF(QTW < 20, 0, QTW - 19) fromQTW," +
                                     " QTW," +
                                     " client_id," +
                                     " id" +
                              " FROM BTAL1 b1" +
                             $" where DCERTIF between '{dateFrom}' AND '{dateTo}'" +
                                " and deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    CertifBerem.Load(sqlCommand.ExecuteReader());
                }

                return CertifBerem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static DataTable FuncLoadFromTable(string dateFrom, string dateTo)
        {
            DataTable womenTable = new DataTable();

            try
            {
                string query = "SELECT CONCAT(SCERTIF, ' ', NCERTIF) CERTIF," +
                                     " DATE_FORMAT(DCERTIF, '%d.%m.%Y') DCERTIF," +
                                     " SNILS," +
                                     " CONCAT(LNAME, ' ', FNAME, ' ', MNAME) FIO_rod," +
                                     " DATE_FORMAT(BDATE, '%d.%m.%Y') BDATE," +
                                     " ADDRESS," +
                                     " CONCAT('Серия ', SDOC, ', номер ', NDOC, ', дата выдачи ', DATE_FORMAT(DDOC, '%d.%m.%Y'), ', выдан ', ODOC) DocType," +
                                     " IF(SPOLICY = '', NPOLICY, CONCAT(SPOLICY, ' ', NPOLICY)) POLICY," +
                                     " IF(SLEAF = '' AND NLEAF = '', '', CONCAT(SLEAF, ' ', NLEAF, ', выдан ', DLEAF)) SICKLIST," +
                                     " DATE_FORMAT(DREG, '%d.%m.%Y') DREG," +
                                     " chld.CNTCHILD," +
                                     " BQNT CNTCHILDALL," +
                                     " chd.CHILD," +
                                     " b2.client_id," +
                                     " b2.id" +
                              " FROM BTAL2 b2" +
                              " JOIN (" +
                                    " SELECT mother_id," +
                                           " GROUP_CONCAT(IF(DIAG = '', CONCAT(SEX, ', ', WEIGHT, ', ', GROWTH), CONCAT(SEX, ', ', WEIGHT, ', ', GROWTH, ', ', DIAG)) SEPARATOR '; ') CHILD" +
                                    " FROM CHILD" +
                                    " where deleted = 0" +
                                    " group by 1" +
                                   " )chd ON b2.client_id = chd.mother_id" +
                              " JOIN (" +
                                   " SELECT mother_id, SUM(1) CNTCHILD" +
                                   " FROM CHILD" +
                                   " where deleted = 0" +
                                   " GROUP BY 1" +
                                  " ) chld ON b2.client_id = chld.mother_id" +
                             $" WHERE DREG BETWEEN '{dateFrom}' AND '{dateTo}'" +
                                " and deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    womenTable.Load(sqlCommand.ExecuteReader());
                }

                return womenTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static DataTable FuncLoad(string dateFrom, string dateTo)
        {
            DataTable womens = new DataTable();

            try
            {
                string query = "SELECT DISTINCT e.client_id client_id," +
                                     " z_getClientFIO(e.client_id) fio," +
                                     " z_getClientRodDocument(e.client_id) rodCertif," +
                                     " z_getClientRodDocumentDate(e.client_id) dateCertif," +
                                     " z_getClientRodDocumentWhoGive(e.client_id) whoGiveCertif" +
                              " FROM Event e" +
                              " WHERE e.deleted = 0" +
                                " AND (e.execDate IS NULL OR e.execDate >= DATE_ADD(NOW(), INTERVAL -1 MONTH))" +
                               $" AND e.setDate BETWEEN '{dateFrom} 00:00:00' AND '{dateTo} 23:59:59'" +
                                " AND e.eventType_id = 60";
                
                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    womens.Load(sqlCommand.ExecuteReader());
                }

                return womens;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static DataTable FuncINSRT(int client_id)
        {
            DataTable woman = new DataTable();
            try
            {
                
                string query = "SELECT IF(lastName is NULL, '', lastName) LNAME," +
                                     " IF(firstName is null, '', firstName) FNAME," +
                                     " IF(patrName is null, '', patrName) MNAME," +
                                     " IF(birthDate is null, CURRENT_DATE(), birthDate) BDATE," +
                                     " IF(getClientRegAddress(c.id) is null, '', IF ( getClientRegAddress(c.id) LIKE '% обл, %' OR getClientRegAddress(c.id) LIKE '% край, %' OR getClientRegAddress(c.id) LIKE '% респ, %', " +
                                                                                    " CONCAT(" +
                                                                                            " LEFT(UPPER(getClientRegAddress(c.id)), 1)," +
                                                                                            " SUBSTRING(UPPER(getClientRegAddress(c.id))," +
                                                                                            " LOCATE(' ', getClientRegAddress(c.id)) + 1, 1)," +
                                                                                            " ', '," +
                                                                                            " SUBSTRING(getClientRegAddress(c.id), LOCATE(',', getClientRegAddress(c.id)) + 1, LENGTH(getClientRegAddress(c.id))))," +
                                                                                            " IF(getClientRegAddress(c.id) LIKE '% ао, %' AND getClientRegAddress(c.id) LIKE '%-%'," +
                                                                                                " CONCAT(" +
                                                                                                        " LEFT(UPPER(getClientRegAddress(c.id)), 1)," +
                                                                                                        " SUBSTRING(UPPER(getClientRegAddress(c.id)), LOCATE('-', getClientRegAddress(c.id)) + 1, 1)," +
                                                                                                        " SUBSTRING(UPPER(getClientRegAddress(c.id))," +
                                                                                                        " LOCATE(' ', getClientRegAddress(c.id)) + 1, 2)," +
                                                                                                        " ', '," +
                                                                                                        " SUBSTRING(getClientRegAddress(c.id), LOCATE(',', getClientRegAddress(c.id)) + 1, LENGTH(getClientRegAddress(c.id))))," +
                                                                                                        " IF(getClientRegAddress(c.id) LIKE '% ао, %'," +
                                                                                                            " CONCAT(" +
                                                                                                                    " LEFT(UPPER(getClientRegAddress(c.id)), 1)," +
                                                                                                                    " SUBSTRING(UPPER(getClientRegAddress(c.id))," +
                                                                                                                    " LOCATE(' ', getClientRegAddress(c.id)) + 1, 2)," +
                                                                                                                    " ', '," +
                                                                                                                    " SUBSTRING(getClientRegAddress(c.id), LOCATE(',', getClientRegAddress(c.id)) + 1, LENGTH(getClientRegAddress(c.id)))), getClientRegAddress(c.id))))) address," +
                                     " IF(c.SNILS is null, '', c.SNILS) SNILS," +
                                     " IF(cp.serial is null, '', cp.serial) SPOLICY," +
                                     " IF(cp.number is null, '', cp.number) NPOLICY," +
                                     " IF(dt.regionalCode is null, 0, dt.regionalCode) TDOC," +
                                     " IF(cd.serial is null, '', cd.serial) SDOC," +
                                     " IF(cd.number is null, '', cd.number) NDOC," +
                                     " IF(date_FORMAT(cd.date, '%d.%m.%Y') IS NULL, CURRENT_DATE(), date_FORMAT(cd.date, '%d.%m.%Y')) DDOC," +
                                     " IF(cd.origin is null, '', cd.origin) ODOC," +
                                     " IF(rc.serial is null, '', rc.serial) SCERTIF," +
                                     " IF(rc.number is null, '', rc.number) NCERTIF," +
                                     " IF(date_FORMAT(rc.date, '%d.%m.%Y') IS NULL, CURRENT_DATE(), date_FORMAT(rc.date, '%d.%m.%Y')) DCERTIF," +
                                     " IF(ti.serial IS NULL, '', ti.serial) SLEAF," +
                                     " IF(ti.number IS NULL, '', ti.number) NLEAF," +
                                     " IF(date_FORMAT(ti.issueDate,'%d.%m.%Y') IS NULL, CURRENT_DATE(), date_FORMAT(ti.issueDate,'%d.%m.%Y')) DLEAF" +
                              " FROM Client c" +
                              " LEFT JOIN ClientPolicy cp ON cp.id = getClientPolicyID(c.id, 1)" +
                              " LEFT JOIN ClientDocument cd ON cd.id = getClientDocumentId(c.id)" +
                              " LEFT JOIN ClientDocument rc ON c.id = rc.client_id AND rc.documentType_id = 22 AND rc.id = (SELECT MAX(rc1.id) FROM ClientDocument rc1 WHERE rc1.client_id = c.id AND rc1.documentType_id = 22)" +
                              " left JOIN rbDocumentType dt ON cd.documentType_id = dt.id" +
                              " LEFT JOIN TempInvalid ti ON c.id = ti.client_id AND ti.doctype = 0" +
                             $" WHERE c.id = {client_id}";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    woman.Load(sqlCommand.ExecuteReader());
                }

                return woman;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
                return null;
            }
        }

        public static DataTable FuncTable(string dateFrom, string dateTo)
        {
            DataTable ReportTable = new DataTable();

            try
            {
                string query = "SELECT \"В период беременности\" period," +
                                     " sum(CASE when b1.ADDRESS LIKE '% г%' OR b1.ADDRESS LIKE '% г.%' or b1.ADDRESS LIKE '% ,г. %' or b1.ADDRESS LIKE '% ,г %' THEN 0 ELSE 1 END) Popular," +
                                     " COUNT(DISTINCT b1.client_id) AllCNT" +
                              " FROM BTAL1 b1" +
                             $" WHERE b1.DCERTIF BETWEEN '{dateFrom}' AND '{dateTo}'" +
                                     " AND b1.ADDRESS <> ''" +
                                     " AND b1.deleted = 0" +
                              " GROUP BY 1" +
                              " UNION ALL" +
                              " SELECT \"В период родов и послеродовой период\" period," +
                                       " sum(CASE when b2.ADDRESS LIKE '% г%' OR b2.ADDRESS LIKE '% г.%' or b2.ADDRESS LIKE '% ,г. %' or b2.ADDRESS LIKE '% ,г %' THEN 0 ELSE 1 END) Popular," +
                                       " COUNT(DISTINCT b2.client_id) AllCNT" +
                              " FROM BTAL2 b2" +
                             $" WHERE b2.DREG BETWEEN '{dateFrom}' AND '{dateTo}'" +
                                " AND b2.ADDRESS <> ''" +
                                " AND b2.deleted = 0" +
                              " GROUP BY 1";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                    ReportTable.Load(sqlCommand.ExecuteReader());
                }

                return ReportTable;
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

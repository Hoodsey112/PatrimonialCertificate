using System;
using System.Text;
using System.Xml;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using NLog;

namespace Родовые_сертификаты
{
    class CreateXML
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static DataTable xmlTableM;
        static DataTable xmlTableC;
        static DataTable xmlTableB;
        public static void LoadTableBTAL2(string queryMother, string queryChild)
        {
            try
            {
                var defSet = Properties.Settings.Default;
                xmlTableM = new DataTable();
                xmlTableC = new DataTable();

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(queryMother, sqlConnection);
                    xmlTableM.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();

                    sqlConnection.Open();
                    sqlCommand = new MySqlCommand(queryChild, sqlConnection);
                    xmlTableC.Load(sqlCommand.ExecuteReader());
                }

                using (XmlTextWriter writer = new XmlTextWriter($@"C:\Users\{Environment.UserName}\Desktop\BTAL2_{DateTime.Now.Date.ToShortDateString()}.xml", Encoding.GetEncoding(1251)))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 1;
                    writer.IndentChar = '\t';
                    writer.WriteStartDocument();
                    writer.WriteProcessingInstruction("BSERTIF2", "version=\"0.1\" povers=\"10\"");

                    writer.WriteStartElement("BIRTH_SERTIF");
                    {
                        //Данные больницы добавить в настройки по умолчанию.
                        writer.WriteStartElement("HEADER");
                        writer.WriteAttributeString("FROM", $"{defSet.Name_LPU}");
                        writer.WriteAttributeString("TO", $"{defSet.Name_FSS}");
                        writer.WriteAttributeString("DATE", $"{DateTime.Now.ToString("yyyy.MM.dd")}");
                        writer.WriteAttributeString("INN", $"{defSet.INN_LPU}");
                        writer.WriteAttributeString("REG_NUM", $"{defSet.RegNumb_LPU}");
                        writer.WriteAttributeString("KPP", $"{defSet.KPP_LPU}");
                        writer.WriteAttributeString("OGRN", $"{defSet.OGRN_LPU}");
                        writer.WriteAttributeString("ADRES", $"{defSet.Address_LPU}");
                        writer.WriteAttributeString("TYPE", $"{defSet.Type_LPU}");
                        writer.WriteAttributeString("GDOCTER", $"{defSet.OwnDoc_LPU}");
                        writer.WriteAttributeString("GBUH", $"{defSet.OwnBookKeep}");
                        writer.WriteAttributeString("ACC", $"{defSet.RS_LPU}");
                        writer.WriteAttributeString("BNAME", $"{defSet.BankName_LPU}");
                        writer.WriteAttributeString("BIC", $"{defSet.BIK_LPU}");
                        writer.WriteAttributeString("KACC", $"{defSet.KS_LPU}");
                        writer.WriteAttributeString("NDOC", $"{defSet.DocNumb_FSS}");
                        writer.WriteAttributeString("DDOC", $"{defSet.DocDate_FSS}");
                        writer.WriteEndElement();

                        for (int i = 0; i < xmlTableM.Rows.Count; i++)
                        {
                            writer.WriteStartElement("BTAL2");
                            writer.WriteAttributeString("DREG", $"{xmlTableM.Rows[i][0].ToString()}");
                            writer.WriteAttributeString("DIAG", $"{xmlTableM.Rows[i][1].ToString()}");
                            writer.WriteAttributeString("BQNT", $"{xmlTableM.Rows[i][2].ToString()}");
                            writer.WriteAttributeString("CHK", $"{xmlTableM.Rows[i][3].ToString()}");
                            {
                                writer.WriteStartElement("CERTIF");
                                writer.WriteAttributeString("SCERTIF", $"{xmlTableM.Rows[i][4].ToString()}");
                                writer.WriteAttributeString("NCERTIF", $"{xmlTableM.Rows[i][5].ToString()}");
                                writer.WriteAttributeString("DCERTIF", $"{xmlTableM.Rows[i][6].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("PERSON");
                                writer.WriteAttributeString("SNILS", $"{xmlTableM.Rows[i][7].ToString()}");
                                writer.WriteAttributeString("LNAME", $"{xmlTableM.Rows[i][8].ToString()}");
                                writer.WriteAttributeString("FNAME", $"{xmlTableM.Rows[i][9].ToString()}");
                                writer.WriteAttributeString("MNAME", $"{xmlTableM.Rows[i][10].ToString()}");
                                writer.WriteAttributeString("BDATE", $"{xmlTableM.Rows[i][11].ToString()}");
                                writer.WriteAttributeString("ADDRESS", $"{xmlTableM.Rows[i][12].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("PASSPORT");
                                writer.WriteAttributeString("TDOC", $"{xmlTableM.Rows[i][13].ToString()}");
                                writer.WriteAttributeString("SDOC", $"{xmlTableM.Rows[i][14].ToString()}");
                                writer.WriteAttributeString("NDOC", $"{xmlTableM.Rows[i][15].ToString()}");
                                writer.WriteAttributeString("DDOC", $"{xmlTableM.Rows[i][16].ToString()}");
                                writer.WriteAttributeString("ODOC", $"{xmlTableM.Rows[i][17].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("POLICY");
                                writer.WriteAttributeString("SPOLICY", $"{xmlTableM.Rows[i][18].ToString()}");
                                writer.WriteAttributeString("NPOLICY", $"{xmlTableM.Rows[i][19].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("SICKLIST");
                                writer.WriteAttributeString("SLEAF", $"{xmlTableM.Rows[i][20].ToString()}");
                                writer.WriteAttributeString("NLEAF", $"{xmlTableM.Rows[i][21].ToString()}");
                                writer.WriteAttributeString("DLEAF", $"{xmlTableM.Rows[i][22].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("EXCCARD");
                                writer.WriteAttributeString("NCARD", $"{xmlTableM.Rows[i][23].ToString()}");
                                writer.WriteAttributeString("DCARD", $"{xmlTableM.Rows[i][24].ToString()}");
                                writer.WriteEndElement();

                                for (int j = 0; j < xmlTableC.Rows.Count; j++)
                                {
                                    if (xmlTableM.Rows[i][25].ToString() == xmlTableC.Rows[j][0].ToString())
                                    {
                                        writer.WriteStartElement("CHILD");
                                        writer.WriteAttributeString("SEX", $"{xmlTableC.Rows[j][1].ToString()}");
                                        writer.WriteAttributeString("WEIGHT", $"{xmlTableC.Rows[j][2].ToString()}");
                                        writer.WriteAttributeString("GROWTH", $"{xmlTableC.Rows[j][3].ToString()}");
                                        writer.WriteAttributeString("DIAG", $"{xmlTableC.Rows[j][4].ToString()}");
                                        writer.WriteEndElement();
                                    }
                                }
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }

        public static void LoadTableBTAL1(string queryMother)
        {
            try
            {
                xmlTableB = new DataTable();
                var defSet = Properties.Settings.Default;

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnectionTable())
                {
                    if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                    MySqlCommand sqlCommand = new MySqlCommand(queryMother, sqlConnection);
                    xmlTableB.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();
                }

                using (XmlTextWriter writer = new XmlTextWriter($@"C:\Users\{Environment.UserName}\Desktop\BTAL1_{DateTime.Now.Date.ToShortDateString()}.xml", Encoding.GetEncoding(1251)))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 1;
                    writer.IndentChar = '\t';
                    writer.WriteStartDocument();
                    writer.WriteProcessingInstruction("BSERTIF1", "version=\"0.1\" povers=\"10\"");

                    writer.WriteStartElement("BIRTH_SERTIF");
                    {
                        //Данные больницы добавить в настройки по умолчанию.
                        writer.WriteStartElement("HEADER");
                        writer.WriteAttributeString("FROM", $"{defSet.Name_LPU}");
                        writer.WriteAttributeString("TO", $"{defSet.Name_FSS}");
                        writer.WriteAttributeString("DATE", $"{DateTime.Now.ToString("yyyy.MM.dd")}");
                        writer.WriteAttributeString("INN", $"{defSet.INN_LPU}");
                        writer.WriteAttributeString("REG_NUM", $"{defSet.RegNumb_LPU}");
                        writer.WriteAttributeString("KPP", $"{defSet.KPP_LPU}");
                        writer.WriteAttributeString("OGRN", $"{defSet.OGRN_LPU}");
                        writer.WriteAttributeString("ADRES", $"{defSet.Address_LPU}");
                        writer.WriteAttributeString("TYPE", $"{defSet.Type_LPU}");
                        writer.WriteAttributeString("GDOCTER", $"{defSet.OwnDoc_LPU}");
                        writer.WriteAttributeString("GBUH", $"{defSet.OwnBookKeep}");
                        writer.WriteAttributeString("ACC", $"{defSet.RS_LPU}");
                        writer.WriteAttributeString("BNAME", $"{defSet.BankName_LPU}");
                        writer.WriteAttributeString("BIC", $"{defSet.BIK_LPU}");
                        writer.WriteAttributeString("KACC", $"{defSet.KS_LPU}");
                        writer.WriteAttributeString("NDOC", $"{defSet.DocNumb_FSS}");
                        writer.WriteAttributeString("DDOC", $"{defSet.DocDate_FSS}");
                        writer.WriteEndElement();

                        for (int i = 0; i < xmlTableB.Rows.Count; i++)
                        {
                            writer.WriteStartElement("BTAL1");
                            writer.WriteAttributeString("DREG", $"{xmlTableB.Rows[i][0].ToString()}");
                            writer.WriteAttributeString("QTW2", $"{xmlTableB.Rows[i][1].ToString()}");
                            writer.WriteAttributeString("MULP", $"{xmlTableB.Rows[i][2].ToString()}");
                            writer.WriteAttributeString("PREM", $"{xmlTableB.Rows[i][3].ToString()}");
                            writer.WriteAttributeString("CHK", $"{xmlTableB.Rows[i][4].ToString()}");
                            if (xmlTableB.Rows[i][26].ToString() == "1") writer.WriteAttributeString("PHELP", $"{xmlTableB.Rows[i][26].ToString()}");
                            {
                                writer.WriteStartElement("CERTIF");
                                writer.WriteAttributeString("SCERTIF", $"{xmlTableB.Rows[i][5].ToString()}");
                                writer.WriteAttributeString("NCERTIF", $"{xmlTableB.Rows[i][6].ToString()}");
                                writer.WriteAttributeString("DCERTIF", $"{xmlTableB.Rows[i][7].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("PERSON");
                                writer.WriteAttributeString("SNILS", $"{xmlTableB.Rows[i][8].ToString()}");
                                writer.WriteAttributeString("LNAME", $"{xmlTableB.Rows[i][9].ToString()}");
                                writer.WriteAttributeString("FNAME", $"{xmlTableB.Rows[i][10].ToString()}");
                                writer.WriteAttributeString("MNAME", $"{xmlTableB.Rows[i][11].ToString()}");
                                writer.WriteAttributeString("BDATE", $"{xmlTableB.Rows[i][12].ToString()}");
                                writer.WriteAttributeString("ADDRESS", $"{xmlTableB.Rows[i][13].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("PASSPORT");
                                writer.WriteAttributeString("TDOC", $"{xmlTableB.Rows[i][14].ToString()}");
                                writer.WriteAttributeString("NDOC", $"{xmlTableB.Rows[i][15].ToString()}");
                                writer.WriteAttributeString("SDOC", $"{xmlTableB.Rows[i][16].ToString()}");
                                writer.WriteAttributeString("DDOC", $"{xmlTableB.Rows[i][17].ToString()}");
                                writer.WriteAttributeString("ODOC", $"{xmlTableB.Rows[i][18].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("POLICY");
                                writer.WriteAttributeString("SPOLICY", $"{xmlTableB.Rows[i][19].ToString()}");
                                writer.WriteAttributeString("NPOLICY", $"{xmlTableB.Rows[i][20].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("EXCCARD");
                                writer.WriteAttributeString("NCARD", $"{xmlTableB.Rows[i][21].ToString()}");
                                writer.WriteAttributeString("DCARD", $"{xmlTableB.Rows[i][22].ToString()}");
                                writer.WriteEndElement();

                                writer.WriteStartElement("SICKLIST");
                                writer.WriteAttributeString("SLEAF", $"{xmlTableB.Rows[i][23].ToString()}");
                                writer.WriteAttributeString("NLEAF", $"{xmlTableB.Rows[i][24].ToString()}");
                                writer.WriteAttributeString("DLEAF", $"{xmlTableB.Rows[i][25].ToString()}");
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Debug("\n/--------------------------------------------------------------------/\n" + ex.StackTrace + "\n//----------------------------//\n" + ex.Message + "\n\n");
            }
        }
    }
}

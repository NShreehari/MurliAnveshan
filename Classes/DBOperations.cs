using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System;
using MurliAnveshan;

// C:\Users\Shree\AppData\Local\Microsoft\Microsoft SQL Server Data
namespace MurliSearch.Classes
{
    public static class DBOperations
    {
        private static readonly string SqlCS = ConfigurationManager.ConnectionStrings["MurliAnveshanDBConString"].ConnectionString;

        //public static SqlConnection GetSqlConnection()
        //{
        //    StreamWriter sw = null;

        //    SqlConnection conn = null;
        //    try
        //    {
        //        conn = new SqlConnection(SqlCS);
        //        conn.Open();
        //    }
        //    catch (Exception ex)
        //    {
        //        sw = new StreamWriter(ConfigurationManager.ConnectionStrings["LogFilePath"].ConnectionString + "\\Log File.txt");
        //        sw.Write(ex.GetType().ToString());
        //        sw.WriteLine(": " + ex.Message);
        //        sw.WriteLine();
        //        sw.WriteLine(ex.StackTrace);
        //        PrintDashes(sw);

        //        MessageBox.Show(ex.Message);

        //    }
        //    finally
        //    {
        //        if (sw != null)
        //        {
        //            sw.Close();
        //        }
        //    }
        //    return conn;
        //}

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(SqlCS);

            conn.Open();

            return conn;
        }

        //StreamWriter sw = null;

        //catch (SqlException SqlEx)
        //{
        //sw = new StreamWriter(ConfigurationManager.ConnectionStrings["LogFilePath"].ConnectionString + "\\Log File.txt",true);
        //sw.WriteLine("Type: " + SqlEx.GetType());
        //sw.WriteLine();

        //sw.WriteLine("Message: " + SqlEx.Message);
        //sw.WriteLine();
        //sw.WriteLine();


        //sw.WriteLine("Source: " + SqlEx.Source);
        //sw.WriteLine();
        //sw.WriteLine();

        //sw.WriteLine();
        //sw.WriteLine("STACK TRACE" + SqlEx.StackTrace);
        //PrintDashes(sw);


        //if (SqlEx.Message.Contains("26"))
        //{
        //    MessageBox.Show("Service Not Started.");
        //}
        //}
        //finally
        //{
        //    if (conn != null)
        //    {
        //        conn.Close();
        //    }

        //    if (sw != null)
        //    {
        //        sw.Close();
        //    }
        //}





        public static void PrintDashes(StreamWriter sw)
        {
            sw.WriteLine("--------------------------------------------------------------------------------------------------------------------------\n\n\n");
        }

        public static OleDbConnection GetOledbConnection(string oledbCS)
        {
            OleDbConnection ocon = new OleDbConnection(oledbCS);
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }
            return ocon;
        }

        //public static int ExecuteScalarQuery(string strSql,string oledbCS)
        //{
        //    OleDbConnection con = GetOledbConnection(string oledbCS);
        //    OleDbCommand cmd = new OleDbCommand(strSql,con);


        //    int rowsAffected ;
        //    return rowsAffected;
        //}



        public static DataTable ExecuteSelect(string strSql)
        {
            DataTable dt = null;
            //StreamWriter sw = null;

            using (SqlConnection Connection = GetSqlConnection())
            using (SqlCommand Command = new SqlCommand(strSql, Connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = Command;

                dt = new DataTable();

                adapter.Fill(dt);
            }


            //catch (SqlException ex)
            //{
            //    sw = new StreamWriter(ConfigurationManager.ConnectionStrings["LogFilePath"].ConnectionString + "\\Log File.txt");
            //    sw.Write(ex.GetType().ToString());
            //    sw.WriteLine(": " + ex.Message);
            //    sw.WriteLine();
            //    sw.WriteLine(ex.StackTrace);
            //    PrintDashes(sw);


            //}
            //finally
            //{

            //if (sw != null)
            //{
            //    sw.Close();
            //}
            //}
            return dt;
        }


        public static DataTable ExecuteSelect(string strSql, string searchTerm)
        {
            DataTable dt = null;

            using (SqlConnection Connection = GetSqlConnection())
            using (SqlCommand Command = new SqlCommand(strSql, Connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (adapter.SelectCommand = Command)
            {
                Command.Parameters.AddWithValue("@searchTerm", searchTerm);
                dt = new DataTable();

                adapter.Fill(dt);
            }

            return dt;
        }


        public static DataTable ExecuteSelect(string strSql, string fileName, int pageNumber)
        {
            DataTable dt = null;

            using (SqlConnection Connection = GetSqlConnection())
            using (SqlCommand Command = new SqlCommand(strSql, Connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (adapter.SelectCommand = Command)
            {
                Command.Parameters.AddWithValue("@pageNumber", pageNumber);
                Command.Parameters.AddWithValue("@fileName", fileName);

                dt = new DataTable();

                adapter.Fill(dt);
            }

            return dt;
        }

        public static DataTable ExecuteSelectToGetPageNumber(string strSql, string murliTitle, string murliDate)
        {
            DataTable dt = null;

            using (SqlConnection Connection = GetSqlConnection())
            using (SqlCommand Command = new SqlCommand(strSql, Connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (adapter.SelectCommand = Command)
            {
                Command.Parameters.AddWithValue("@MurliTitle", murliTitle);
                Command.Parameters.AddWithValue("@MurliDate", murliDate);

                dt = new DataTable();

                adapter.Fill(dt);
            }

            return dt;
        }


        //public static int ExecuteStoredProcedure(string spName)
        //{        //    using (SqlConnection Connection = GetSqlConnection())
        //    {
        //        SqlCommand Command = new SqlCommand(spName, Connection);
        //        Command.CommandType = CommandType.StoredProcedure;

        //        Command.Parameters.AddWithValue

        //        int RowsAffected = Command.ExecuteNonQuery();
        //    }
        //}

        public static int ExecuteQuery(string strSql)
        {
            using (SqlConnection Connection = GetSqlConnection())
            {
                int RowsAffected;
                using (SqlCommand Command = new SqlCommand(strSql, Connection))
                {
                    RowsAffected = Command.ExecuteNonQuery();
                }
                return RowsAffected;
            }
        }


        public static void ExecuteBulkCopy(DataTable tbl)
        {
            using (SqlConnection Connection = GetSqlConnection())
            {
                //int RowsAffected;

                SqlBulkCopy bulkCopier = new SqlBulkCopy(Connection)
                {
                    //assign Destination table name  
                    DestinationTableName = "tblIndex"
                };
                bulkCopier.ColumnMappings.Add(tbl.Columns[0].ColumnName, "MurliDate");
                bulkCopier.ColumnMappings.Add(tbl.Columns[1].ColumnName, "MurliHeading");
                bulkCopier.ColumnMappings.Add(tbl.Columns[2].ColumnName, "PageNumber");

                //con.Open();
                //insert bulk Records into DataBase.

                bulkCopier.WriteToServer(tbl);

                Connection.Close();
                //using (SqlCommand Command = new SqlCommand(strSql, Connection))
                //{
                //    RowsAffected = Command.ExecuteNonQuery();
                //}
                //return RowsAffected;
            }
        }


        public static int ExecuteQuery(string strSql, string strSql2)
        {
            using (SqlConnection connection = GetSqlConnection())
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand Command = new SqlCommand(strSql, connection, transaction);
                        int RowsAffected = Command.ExecuteNonQuery();

                        SqlCommand Command2 = new SqlCommand(strSql2, connection, transaction);
                        int RowsAffected2 = Command2.ExecuteNonQuery();

                        transaction.Commit();
                        return RowsAffected;

                        //if (RowsAffected > 0 && RowsAffected2 > 0)
                        //{
                        //    transaction.Commit();
                        //    return RowsAffected;
                        //}
                        //else
                        //{
                        //    transaction.Rollback();
                        //    throw new Exception("Unable Delete All Students. Some Error Occurred. Please Contact Your Administrator.");
                        //}
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Messages.ExceptionMessage(ex.Message);
                        return 0;
                    }
                }
            }
        }


        public static int ExecuteScalar(string strSql)
        {
            SqlConnection Connection = GetSqlConnection();

            SqlCommand Command = new SqlCommand(strSql, Connection);

            int RowsAffected = (int)Command.ExecuteScalar();

            Connection.Close();
            return RowsAffected;
        }


        //public void insertdataintosql(string USN, string StudentName, string Regular, string Arrear)
        //{//inserting data into the Sql Server
        //    SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS; AttachDbFilename= C:\Users\Shree\documents\visual studio 2010\Projects\Exam\Exam\App_Data\ExamDB.mdf; Integrated Security = True; User Instance = True");
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = "insert into tblStudent(USN,StudentName,Regular,Arrear)";
        //    cmd.CommandText += " values(@USN,@StudentName,@Regular,@Arrear)";
        //    cmd.Parameters.Add("@USN", SqlDbType.NVarChar).Value = USN;
        //    cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = StudentName;
        //    cmd.Parameters.Add("@Regular", SqlDbType.NVarChar).Value = Regular;
        //    cmd.Parameters.Add("@Arrear", SqlDbType.NVarChar).Value = Arrear;

        //    cmd.CommandType = CommandType.Text;
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}
    }
}

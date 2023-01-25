using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace S3Q4.Models
{
    public class FactoryClass
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());

        public bool LogtoSQL2(Exception ex, string source = "", string data = "", string TxnID = "")
        {
            bool status = false;
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Concat("* Error Log - ", DateTime.Now, "**"));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(string.Concat("Exception Type : ", ex.GetType().Name));
            sb.Append(Environment.NewLine);
            sb.Append(string.Concat("Error Message : ", ex.Message));
            sb.Append(Environment.NewLine);
            sb.Append(string.Concat("Error Source : ", ex.Source));
            sb.Append(Environment.NewLine);
            if (ex.StackTrace != null)
            {
                sb.Append(string.Concat("Error Trace : ", ex.StackTrace));
            }
            for (Exception innerEx = ex.InnerException; innerEx != null; innerEx = innerEx.InnerException)
            {
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Exception Type : ", innerEx.GetType().Name));
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Error Message : ", innerEx.Message));
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Error Source : ", innerEx.Source));
                sb.Append(Environment.NewLine);
                if (ex.StackTrace != null)
                {
                    sb.Append(string.Concat("Error Trace : ", innerEx.StackTrace));
                }
            }

            {
                using (SqlCommand cmd = new SqlCommand("Insert_Execption", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure; cmd.CommandTimeout = 600;
                    cmd.Parameters.AddWithValue("@TxnID", TxnID);
                    cmd.Parameters.AddWithValue("@Source", ex.Source.ToString());
                    cmd.Parameters.AddWithValue("@Data", data);
                    cmd.Parameters.AddWithValue("@ExceptionMessage", sb.ToString());


                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        status = true;
                    }
                    else
                    {

                        status = false;
                    }
                }
            }

            return status;
        }
    }
}

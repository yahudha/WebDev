using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChurchApp
{

    public class DataBaseClass
    {
        public DataBaseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlDataAdapter da;
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();


        public void ConnectDataBaseToInsert(string Query)
        {
            con = new SqlConnection(@"Data Source=LAPTOP-H558TVAP;Initial Catalog=ChurchApp;Integrated Security=True");
            cmd.CommandText = Query;
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public DataSet ConnectDataBaseReturnDS(string Query)
        {
            ds = new DataSet();
            con = new SqlConnection(@"Data Source=LAPTOP-H558TVAP;Initial Catalog=ChurchApp;Integrated Security=True");
            cmd.CommandText = Query;
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataTable ConnectDataBaseReturnDT(string Query)
        {
            dt = new DataTable();
            con = new SqlConnection(@"Data Source=LAPTOP-H558TVAP;Initial Catalog=ChurchApp;Integrated Security=True");
            cmd.CommandText = Query;
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return dt;
        }

    }
}



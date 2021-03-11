using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ChurchApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        DataBaseClass dbClass = new DataBaseClass();
        public DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
                Session.Abandon();
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            bool Authenticated = false;
            Authenticated = UserAuthenticate(txtUserId.Text, txtPasword.Text);

            if (Authenticated == true)
            {

                Response.Redirect("Default.aspx?Id=" + Session["UserId"].ToString());

            }




        }




        private bool UserAuthenticate(string UserName, string Password)
        {
            bool boolReturnValue = false;

            //--------------------------------
            dt = new DataTable();
            string chkUser = "Select * FROM [Users] where Email='" + UserName + "' AND Password='" + Password + "'";
            dt = dbClass.ConnectDataBaseReturnDT(chkUser);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsBlocked"].ToString() == "True")
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "MessagePopup", "alert('Your Account is Blocked. Please contact Administrator.'); window.location.href='Login.aspx';", true);
                    return false;
                }
                else
                {
                    boolReturnValue = true;
                    Session["UserId"] = dt.Rows[0]["Id"].ToString();
                    Session["UserName"] = dt.Rows[0]["Name"].ToString();
                    string updateLastLogin = "Update [Users] SET LastLogin='" + System.DateTime.Now.ToString() + "' where Id='" + Session["UserId"].ToString() + "'";
                    dbClass.ConnectDataBaseToInsert(updateLastLogin);
                }
            }
            return boolReturnValue;

        }
    }
}
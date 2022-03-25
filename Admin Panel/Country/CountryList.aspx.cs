using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_Project6.Admin_Panel.Country
{
    public partial class CountryList : System.Web.UI.Page
    {

        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                FillGridView();
            }
        }

        #endregion Load Event

        #region FillGridView
        private void FillGridView()
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);
            //SqlConnection objConn = new SqlConnection();
            //objConn.ConnectionString = "data source = LAPTOP-HO6C6KIO\\SQLEXPRESS;initial catalog=Addressbook;Integrated Security=True";

            //Security = True means Windows Authentication
            //Security = False means SQL Authentication
            try
            {
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }
                SqlCommand objCmd = new SqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.CommandText = "[dbo].[PR_Country_SelectByUserID]";
                if(Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }


                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    gvCountry.DataSource = objSDR;
                    gvCountry.DataBind();
                }

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }

            catch (Exception ex)
            {
                lblDisplay.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
        }


        #endregion FillGridView

        #region gvCountry: RowCommand
        protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Which Command Is selected
            //Which Command button is Clicked  | e.CommandName
            //Which Row IS Clicked | Get ID Of that ROW | e.CommandArgument

            #region Delete Record

            lblDisplay.Text = "";
            if (e.CommandName == "DeleteRecord")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DeleteCountry(Convert.ToInt32(e.CommandArgument.ToString().Trim()), (Convert.ToInt32(Request.QueryString["UserID"])));
                }
            }

            #endregion Delete Record
        }
        #endregion gvCountry: RowCommand

        #region Delete Country Record
        private void DeleteCountry(SqlInt32 CountryID, SqlInt32 UserID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

            try
            {
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_Country_DeleteByPK]";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString());
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }
                objCmd.ExecuteNonQuery();

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }

                FillGridView();

            }
            catch (Exception ex)
            {
                lblDisplay.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
        }

        #endregion Delete Country Record


    }
}
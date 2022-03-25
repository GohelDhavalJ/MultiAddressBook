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

namespace Net_Project6.Admin_Panel.City
{
    public partial class CityList : System.Web.UI.Page
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
            try
            {
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = new SqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_City_SelectByUserID]";

                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    gvCity.DataSource = objSDR;
                    gvCity.DataBind();
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

        #region gvCity : RowCommand
        protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    DeleteCity(Convert.ToInt32(e.CommandArgument.ToString().Trim()), (Convert.ToInt32(Request.QueryString["UserID"])));

                }
            }

            #endregion Delete Record
        }
        #endregion gvCity : RowCommand

        #region Delete City Record
        private void DeleteCity(SqlInt32 CityID,SqlInt32 UserID)
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
                objCmd.CommandText = "[dbo].[PR_City_DeleteByPK]";
                objCmd.Parameters.AddWithValue("@CityID", CityID.ToString());
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
        #endregion Delete City Record
    }
}
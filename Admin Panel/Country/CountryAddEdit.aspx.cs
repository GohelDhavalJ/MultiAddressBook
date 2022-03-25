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
    public partial class CountryAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["CountryID"] != null)
                {
                    FillControls(Convert.ToInt32(Page.RouteData.Values["CountryID"]), Convert.ToInt32(Page.RouteData.Values["UserID"]));
                    lblMessage.Text += "<br/>CountryID = " + Page.RouteData.Values["CountryID"].ToString().Trim();
                }
            }
        }
        #endregion Load Event

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());

            //Declare Local Variables to insert the data
            SqlInt32 strUserID = SqlInt32.Null;
            SqlString strCountryName = SqlString.Null;
            SqlString strCountryCode = SqlString.Null;

            #endregion Local Variables

            try
            {
                #region Server Side Validation

                //validate The Data
                String strErrorMessage = "";

                if (txtCountryName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter CountryName <br/>";
                }
                if (strErrorMessage != "")
                {
                    lblMessage.Text = strErrorMessage;
                    return;
                }
               

                #endregion Server Side Validation

                #region Gather The Information

                //Gather Information
                if (Session["UserID"] != null)
                {
                    strUserID = Convert.ToInt32(Session["UserID"]);
                }
                if (txtCountryName.Text.Trim() != "")
                {
                    strCountryName = txtCountryName.Text.Trim();
                }

                strCountryCode = txtCountryCode.Text.Trim();

                #endregion Gather The Information

                #region set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                //Pass the parameters in the SP
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }
                objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
                objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);

                #endregion set Connection & Command object

                if (Page.RouteData.Values["CountryID"] != null)
                {
                    #region Update Record

                    objCmd.Parameters.AddWithValue("@CountryID", Page.RouteData.Values["CountryID"].ToString().Trim());
                    objCmd.CommandText = "[dbo].[PR_Country_UpdateByPK]";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/Admin Panel/Country/List");
                    #endregion Update Record
                }

                else 
                {
                    #region Insert Record
                    objCmd.CommandText = "[dbo].[PR_Country_Insert]";
                    objCmd.ExecuteNonQuery();
                    lblMessage.Text = "Data Inserted SucessFully";
                    txtCountryName.Text = "";
                    txtCountryCode.Text = "";
                    #endregion Insert Record
                }
              

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }

            }

        }

        #endregion Button : Save

        #region FillControls
        private void FillControls(SqlInt32 CountryID, SqlInt32 UserID)
        {
            #region Local Variables
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());
            #endregion Local Variables

            try
            {
                #region Set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_Country_SelectByPK]";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
                //objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }
                    


                #endregion Set Connection & Command object

                #region Read the value and set the controls
                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        //if(objSDR["CountryID"].Equals(DBNull.Value) !=true)
                        if (!objSDR["CountryName"].Equals(DBNull.Value))
                        {
                            txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                        }
                        if (!objSDR["CountryCode"].Equals(DBNull.Value))
                        {
                            txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                        }
                        break;
                    }
                }
                else
                {
                    lblMessage.Text = "No Data Available for the StateID = " + CountryID.ToString();
                }
                #endregion Read the value and set the controls

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
        }

        #endregion FillControls


    }
}
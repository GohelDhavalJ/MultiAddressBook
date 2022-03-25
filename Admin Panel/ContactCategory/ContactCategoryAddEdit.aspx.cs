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

namespace Net_Project6.Admin_Panel.ContactCategory
{
    public partial class ContactCategoryAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              
                if (Page.RouteData.Values["ContactCategoryID"] != null)
                {
                    FillControls(Convert.ToInt32(Page.RouteData.Values["ContactCategoryID"]), Convert.ToInt32(Page.RouteData.Values["UserID"]));
                    lblMessage.Text += "<br/>CountryID = " + Page.RouteData.Values["ContactCategoryID"].ToString().Trim();
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
            SqlString strContactCategoryName = SqlString.Null;

            #endregion Local Variables

            try
            {
                #region Server Side Validation

                //validate The Data
                String strErrorMessage = "";

                if (txtContactCategoryName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter ContactCategoryName <br/>";
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
                if(txtContactCategoryName.Text.Trim() != null)
                {
                    strContactCategoryName = txtContactCategoryName.Text.Trim();
                }
                

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
                objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);

                #endregion set Connection & Command object

                if (Page.RouteData.Values["ContactCategoryID"] != null)
                {
                    #region Update Record
                    objCmd.Parameters.AddWithValue("@ContactCategoryID", Page.RouteData.Values["ContactCategoryID"].ToString().Trim());
                    objCmd.CommandText = "[dbo].[PR_ContactCategory_UpdateByPK]";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/Admin Panel/ContactCategory/List", true);
                    #endregion Update Record

                }
                else
                {
                    #region Insert Record
                    objCmd.CommandText = "[dbo].[PR_ContactCategory_Insert]";
                    objCmd.ExecuteNonQuery();
                    lblMessage.Text = "Data Inserted SucessFully";
                    txtContactCategoryName.Text = "";
                    txtContactCategoryName.Focus();
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
        private void FillControls(SqlInt32 ContactCategoryID, SqlInt32 UserID)
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
                objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectByPK]";
                objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
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
                        if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                        {
                            txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                        }

                        break;
                    }
                }
                else
                {
                    lblMessage.Text = "No Data Available for the StateID = " + ContactCategoryID.ToString();
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
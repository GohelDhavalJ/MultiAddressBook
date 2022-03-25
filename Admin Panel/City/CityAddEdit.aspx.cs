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
    public partial class CityAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDropDownListCountry();

                
                if (Page.RouteData.Values["CityID"] != null)
                {
                    FillControls(Convert.ToInt32(Page.RouteData.Values["CityID"]), Convert.ToInt32(Page.RouteData.Values["UserID"]));
                    lblMessage.Text += "<br/>CountryID = " + Page.RouteData.Values["CityID"].ToString().Trim();

                    FillDropDownListCountry();
                    FillDropDownListState();
                }
            }
        }
        #endregion Load Event

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

            SqlInt32 strStateID = SqlInt32.Null;
            SqlInt32 strCountryID = SqlInt32.Null;
            SqlString strCityName = SqlString.Null;
            SqlString strSTDCode = SqlString.Null;
            SqlString strPinCode = SqlString.Null;

            #endregion Local Variables

            try
            {
                #region Server Side Validation

                String strErrorMessage = "";

                if (ddlCountryID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select Country<br/>";
                }
                if (ddlStateID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select State<br/>";
                }
                if (txtCityName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter CityName<br/>";
                }
                if (strErrorMessage.Trim() != "")
                {
                    lblMessage.Text = strErrorMessage;
                    return;
                }

                #endregion Server Side Validation

                #region Gather The Information
                //Gather The Information

                if (ddlCountryID.SelectedIndex > 0)
                {
                    strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
                }
                if (ddlStateID.SelectedIndex > 0)
                {
                    strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
                }
                if (txtCityName.Text.Trim() != "")
                {
                    strCityName = txtCityName.Text.Trim();
                }
                if (txtSTDCode.Text.Trim() != "")
                {
                    strSTDCode = txtSTDCode.Text.Trim();
                }
                if (txtPincode.Text.Trim() != "")
                {
                    strPinCode = txtPincode.Text.Trim();
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
                objCmd.Parameters.AddWithValue("@StateID", strStateID);
                objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
                objCmd.Parameters.AddWithValue("@CityName", strCityName);
                objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);
                objCmd.Parameters.AddWithValue("@PinCode", strPinCode);

                #endregion set Connection & Command object

                if (Page.RouteData.Values["CityID"] != null)
                {
                    #region Update Record
                    objCmd.Parameters.AddWithValue("@CityID", Page.RouteData.Values["CityID"].ToString().Trim());
                    objCmd.CommandText = "[dbo].[PR_City_UpdateByPK]";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/Admin Panel/City/List", true);
                    #endregion Update Record
                }
                else
                {
                    #region Insert Record
                    objCmd.CommandText = "[dbo].[PR_City_Insert]";
                    objCmd.ExecuteNonQuery();
                    txtCityName.Text = "";
                    txtSTDCode.Text = "";
                    txtPincode.Text = "";
                    ddlCountryID.SelectedIndex = 0;
                    ddlStateID.SelectedIndex = 0;
                    ddlCountryID.Focus();
                    lblMessage.Text = "Data Inserted SuccessFully";
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

        #region FillDropDownListState
        private void FillDropDownListState()
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);
            try
            {
                #region Set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_State_SelectForDropDownList]";

                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlStateID.DataSource = objSDR;
                    ddlStateID.DataValueField = "StateID";
                    ddlStateID.DataTextField = "StateName";
                    ddlStateID.DataBind();
                }

                ddlStateID.Items.Insert(0, new ListItem("---Select State---", "-1"));

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

        #endregion FillDropDownListState

        #region FillDropDownListCountry
        private void FillDropDownListCountry()
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());

            try
            {
                #region Set Connection & Command object
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_Country_SelectForDropDownList]";

                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlCountryID.DataSource = objSDR;
                    ddlCountryID.DataValueField = "CountryID";
                    ddlCountryID.DataTextField = "CountryName";
                    ddlCountryID.DataBind();
                }

                ddlCountryID.Items.Insert(0, new ListItem("---Select Country---", "-1"));

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

        #endregion FillDropDownListCountry

        #region FillControls
        private void FillControls(SqlInt32 CityID,SqlInt32 UserID)
        {
            #region Local Variables
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);
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
                objCmd.CommandText = "[dbo].[PR_City_SelectByPK]";
                objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
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
                        //if(objSDR["CountryID"].Equals(DBNull.Value) != true)
                        if (!objSDR["CountryID"].Equals(DBNull.Value))
                        {
                            ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        }
                        if (!objSDR["StateID"].Equals(DBNull.Value))
                        {
                            ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                        }
                        if (!objSDR["CityName"].Equals(DBNull.Value))
                        {
                            txtCityName.Text = objSDR["CityName"].ToString().Trim();
                        }
                        if (!objSDR["STDCode"].Equals(DBNull.Value))
                        {
                            txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                        }
                        if (!objSDR["PinCode"].Equals(DBNull.Value))
                        {
                            txtPincode.Text = objSDR["PinCode"].ToString().Trim();
                        }
                        break;
                    }

                }
                else
                {
                    lblMessage.Text = "No Data Available for the StateID = " + CityID.ToString();
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

        #region ddlCountry - Selected Index Changed

        protected void ddlCountryID_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //we want to fill dropdownlist
            if (ddlCountryID.SelectedIndex > 0)
            {
                FillDropDownListStateByCountryID(Convert.ToInt32(ddlCountryID.SelectedValue));
            }
            else
            {
                ddlStateID.Items.Clear();
                ddlStateID.Items.Insert(0, new ListItem("---Select State---", "-1"));


            }
        }

        #endregion ddlCountry - Selected Index Changed

        #region FillDropDownList state by CountryID
        private void FillDropDownListStateByCountryID(SqlInt32 CountryID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());

            try
            {
                #region Set Connection & Command object
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_State_SelectForDropDownListByCountryID]";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlStateID.DataSource = objSDR;
                    ddlStateID.DataValueField = "StateID";
                    ddlStateID.DataTextField = "StateName";
                    ddlStateID.DataBind();
                    ddlStateID.Items.Insert(0, new ListItem("---Select State---", "-1"));
                }
                else
                {
                    ddlStateID.Items.Clear();
                    ddlStateID.Items.Insert(0, new ListItem("---Select State---", "-1"));

                    
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

        #endregion FillDropDownList state by CountryID

       
    }
}
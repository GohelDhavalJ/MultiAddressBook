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


namespace Net_Project6.Admin_Panel.Contact
{
    public partial class ContactAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropDownListCountry();
                FillDropDownListContactCategory();

                if (Page.RouteData.Values["ContactID"] != null)
                {
                    FillControls(Convert.ToInt32(Page.RouteData.Values["ContactID"].ToString().Trim()), Convert.ToInt32(Page.RouteData.Values["UserID"]));
                  
                    lblMessage.Text += "<br/>CountryID = " + Page.RouteData.Values["ContactID"].ToString().Trim();
                    FillDropDownListState();
                    FillDropDownListCity();

                }


            }
        }
        #endregion Load Event

        #region FillDropDownListCountry
       public void FillDropDownListCountry()
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

        #region FillDropDownListState
        private void FillDropDownListState()
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

        #region FillDropDownListCity
        private void FillDropDownListCity()
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
                objCmd.CommandText = "[dbo].[PR_City_SelectForDropDownList]";

                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlCityID.DataSource = objSDR;
                    ddlCityID.DataValueField = "CityID";
                    ddlCityID.DataTextField = "CityName";
                    ddlCityID.DataBind();
                }

                ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));

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

        #endregion FillDropDownListCity

        #region FillDropDownListContactCategory
        private void FillDropDownListContactCategory()
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
                objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectForDropDownList]";

                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlContactCategoryID.DataSource = objSDR;
                    ddlContactCategoryID.DataValueField = "ContactCategoryID";
                    ddlContactCategoryID.DataTextField = "ContactCategoryName";
                    ddlContactCategoryID.DataBind();
                }

                ddlContactCategoryID.Items.Insert(0, new ListItem("Select ContactCategory", "-1"));

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

        #endregion FillDropDownListContactCategory


        #region ddlCountry - Selected Index Changed

        protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
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

                ddlCityID.Items.Clear();
                ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));
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

                    ddlCityID.Items.Clear();
                    ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));
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


        #region ddlState - Selected Index Changed

        protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStateID.SelectedIndex > 0)
            {
                FillDropDownListCityBySatateID(Convert.ToInt32(ddlStateID.SelectedValue));
            }
            else
            {
                ddlCityID.Items.Clear();
                ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));
            }
        }

        #endregion ddlState - Selected Index Changed

        #region FillDropDownList City by StateID
        private void FillDropDownListCityBySatateID(SqlInt32 StateID)
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
                objCmd.CommandText = "[dbo].[PR_City_SelectForDropDownListByStateID]";
                objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());

                #endregion Set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlCityID.DataSource = objSDR;
                    ddlCityID.DataValueField = "CityID";
                    ddlCityID.DataTextField = "CityName";
                    ddlCityID.DataBind();
                    ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));
                }
                else
                {
                    ddlCityID.Items.Clear();
                    ddlCityID.Items.Insert(0, new ListItem("---Select City---", "-1"));
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

        #endregion FillDropDownList City by StateID

        #region FillControls
        private void FillControls(SqlInt32 ContactID, SqlInt32 UserID)
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
                objCmd.CommandText = "[dbo].[PR_Contact_SelectByPK]";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
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
                        if (!objSDR["CountryID"].Equals(DBNull.Value))
                        {
                            ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        }
                        if (!objSDR["StateID"].Equals(DBNull.Value))
                        {
                            ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                        }
                        if (!objSDR["CityID"].Equals(DBNull.Value))
                        {
                            ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                        }
                        if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        {
                            ddlContactCategoryID.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                        }
                        if (!objSDR["ContactName"].Equals(DBNull.Value))
                        {
                            txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                        }
                        if (!objSDR["ContactNo"].Equals(DBNull.Value))
                        {
                            txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                        }
                        if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                        {
                            txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                        }
                        if (!objSDR["BirthDate"].Equals(DBNull.Value))
                        {
                            txtBirthdate.Text = Convert.ToDateTime(objSDR["BirthDate"]).Date.ToString("yyyy-MM-dd");
                        }
                        if (!objSDR["Email"].Equals(DBNull.Value))
                        {
                            txtEmail.Text = objSDR["Email"].ToString().Trim();
                        }
                        if (!objSDR["Age"].Equals(DBNull.Value))
                        {
                            txtAge.Text = objSDR["Age"].ToString().Trim();
                        }
                        if (!objSDR["Address"].Equals(DBNull.Value))
                        {
                            txtAddress.Text = objSDR["Address"].ToString().Trim();
                        }
                        if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                        {
                            txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                        }
                        if (!objSDR["FaceBookID"].Equals(DBNull.Value))
                        {
                            txtFaceBookID.Text = objSDR["FaceBookID"].ToString().Trim();
                        }
                        if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                        {
                            txtLinkedINID.Text = objSDR["LinkedINID"].ToString().Trim();
                        }
                        break;
                    }
                }
                else
                {
                    lblMessage.Text = "No Data Available for the ContactID = " + ContactID.ToString();
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

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());

            SqlInt32 strUserID = SqlInt32.Null;
            SqlInt32 strCountryID = SqlInt32.Null;
            SqlInt32 strStateID = SqlInt32.Null;
            SqlInt32 strCityID = SqlInt32.Null;
            SqlInt32 strContactCategoryID = SqlInt32.Null;
            SqlString strContactName = SqlString.Null;
            SqlString strContactNo = SqlString.Null;
            SqlString strWhatsAppNo = SqlString.Null;
            SqlDateTime strBirthDate = SqlDateTime.Null;
            SqlString strEmail = SqlString.Null;
            SqlString strAge = SqlString.Null;
            SqlString strAddress = SqlString.Null;
            SqlString strBloodGroup = SqlString.Null;
            SqlString strFaceBookID = SqlString.Null;
            SqlString strLinkedINID = SqlString.Null;
            #endregion Local Variables

            try
            {
                #region Server Side Validation
                //Server Side Validation
                String strErrorMessage = "";

                if (ddlCountryID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select Country<br/>";
                }
                if (ddlStateID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select State<br/>";
                }
                if (ddlCityID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select City<br/>";
                }
                if (ddlContactCategoryID.SelectedIndex == 0)
                {
                    strErrorMessage += "- Select ContactCategory<br/>";
                }
                if (txtContactName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter ContactName<br/>";
                }
                if (txtContactNo.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter ContactNo<br/>";
                }
                if (txtEmail.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter Email<br/>";
                }
                if (txtAddress.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter Address<br/>";
                }
                if (strErrorMessage.Trim() != "")
                {
                    lblMessage.Text = strErrorMessage;
                    return;
                }

                #endregion Server Side Validation

                #region Gather The Information

                //Gather The Information
                if (Session["UserID"] != null)
                {
                    strUserID = Convert.ToInt32(Session["UserID"]);
                }

                if (ddlCountryID.SelectedIndex > 0)
                {
                    strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
                }
                if (ddlStateID.SelectedIndex > 0)
                {
                    strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
                }
                if (ddlCityID.SelectedIndex > 0)
                {
                    strCityID = Convert.ToInt32(ddlCityID.SelectedValue);
                }
                if (ddlContactCategoryID.SelectedIndex > 0)
                {
                    strContactCategoryID = Convert.ToInt32(ddlContactCategoryID.SelectedValue);
                }
                if (txtContactName.Text.Trim() != "")
                {
                    strContactName = txtContactName.Text.Trim();
                }
                if (txtContactNo.Text.Trim() != "")
                {
                    strContactNo = txtContactNo.Text.Trim();
                }
                if (txtEmail.Text.Trim() != "")
                {
                    strEmail = txtEmail.Text.Trim();
                }
                if (txtAddress.Text.Trim() != "")
                {
                    strAddress = txtAddress.Text.Trim();
                }
                if (txtWhatsAppNo.Text.Trim() != "")
                {
                    strWhatsAppNo = txtWhatsAppNo.Text.Trim();
                }
                if (txtBirthdate.Text.Trim() != "")
                {
                    strBirthDate = Convert.ToDateTime(txtBirthdate.Text.Trim());
                }
                if (txtAge.Text.Trim() != "")
                {
                    strAge = txtAge.Text.Trim();
                }
                if (txtBloodGroup.Text.Trim() != "")
                {
                    strBloodGroup = txtBloodGroup.Text.Trim();
                }
                if (txtFaceBookID.Text.Trim() != "")
                {
                    strFaceBookID = txtFaceBookID.Text.Trim();
                }
                if (txtLinkedINID.Text.Trim() != "")
                {
                    strLinkedINID = txtLinkedINID.Text.Trim();
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
                objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
                objCmd.Parameters.AddWithValue("@StateID", strStateID);
                objCmd.Parameters.AddWithValue("@CityID", strCityID);
                objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);
                objCmd.Parameters.AddWithValue("@ContactName", strContactName);
                objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
                objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
                objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
                objCmd.Parameters.AddWithValue("@Email", strEmail);
                objCmd.Parameters.AddWithValue("@Age", strAge);
                objCmd.Parameters.AddWithValue("@Address", strAddress);
                objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
                objCmd.Parameters.AddWithValue("@FaceBookID", strFaceBookID);
                objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedINID);

                #endregion set Connection & Command object

                if (Page.RouteData.Values["ContactID"] != null)
                {
                    #region Update Record
                    objCmd.Parameters.AddWithValue("@ContactID", Page.RouteData.Values["ContactID"].ToString().Trim());
                    objCmd.CommandText = "[dbo].[PR_Contact_UpdateByPK]";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/Admin Panel/Contact/List", true);
                    #endregion Update Record
                }
                else
                {
                    #region Insert Record
                    objCmd.CommandText = "[dbo].[PR_Contact_Insert]";
                    objCmd.ExecuteNonQuery();
                    txtContactName.Text = "";
                    txtContactNo.Text = "";
                    txtWhatsAppNo.Text = "";
                    txtBirthdate.Text = "";
                    txtEmail.Text = "";
                    txtAge.Text = "";
                    txtAddress.Text = "";
                    txtBloodGroup.Text = "";
                    txtFaceBookID.Text = "";
                    txtLinkedINID.Text = "";
                    ddlCountryID.SelectedIndex = 0;
                    ddlCountryID.Focus();
                    ddlCityID.SelectedIndex = 0;

                    ddlStateID.SelectedIndex = 0;

                    ddlContactCategoryID.SelectedIndex = 0;

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


    }
}
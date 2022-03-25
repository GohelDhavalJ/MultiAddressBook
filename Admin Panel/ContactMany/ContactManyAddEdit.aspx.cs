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

namespace Net_Project6.Admin_Panel.ContactMany
{
    public partial class ContactManyAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCBLContactCategoryID();
                if (Request.QueryString["ContactID"] != null)
                {
                    lblMessage.Text = "ContactID = " + Request.QueryString["ContactID"];
                    FillControls(Convert.ToInt32(Request.QueryString["ContactID"].ToString().Trim()));                   
                }
                else
                {
                    lblMessage.Text = "Add Mode";
                }
            }
        }
        #endregion Load Event

        #region FillControls
        private void FillControls(SqlInt32 ContactID)
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
                objCmd.CommandText = "[dbo].[PR_ContactMany_SelectByPK]";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());

                #endregion Set Connection & Command object

                #region Read the value and set the controls

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {

                        if (!objSDR["ContactName"].Equals(DBNull.Value))
                        {
                            txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                        }

                        break;
                    }
                }

                objSDR.Close();

                #region FillContactCategory
                DataTable dt = new DataTable();
                SqlCommand objCmdContactCategory = objConn.CreateCommand();
                objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_CheckboxList";
                objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
                SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();
                dt.Load(objSDRContactCategory);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][2].ToString() == "SELECTED")
                        {
                            cblContactCategoryID.Items[i].Selected = true;
                        }
                    }
                }
                #endregion FillContactCategory

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

            SqlString strContactName = SqlString.Null;
            #endregion Local Variables

            try
            {
                #region Server Side Validation
                //Server Side Validation
                String strErrorMessage = "";


                if (txtContactName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter ContactName<br/>";
                }

                if (strErrorMessage.Trim() != "")
                {
                    lblMessage.Text = strErrorMessage;
                    return;
                }

                #endregion Server Side Validation

                #region Gather The Information

                //Gather The Information

                if (txtContactName.Text.Trim() != "")
                {
                    strContactName = txtContactName.Text.Trim();
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

                objCmd.Parameters.AddWithValue("@ContactName", strContactName);

                #endregion set Connection & Command object

                if (Request.QueryString["ContactID"] != null)
                {
                    #region Update Record
                    objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                    objCmd.CommandText = "[dbo].[PR_ContactMany_Update]";
                    objCmd.ExecuteNonQuery();

                    DeleteContactCategory(Convert.ToInt32(Request.QueryString["ContactID"]));
                    AddContactCategory(Convert.ToInt32(Request.QueryString["ContactID"]));

                    Response.Redirect("~/Admin Panel/ContactMany/ContactManyList.aspx");
                    #endregion Update Record
                }
                else
                {
                    #region Insert Record
                    objCmd.CommandText = "[dbo].[PR_ContactMany_Insert]";

                    //Out parameter 

                    objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    //objCmd.Parameters["@ContactID"].Direction = ParameterDirection.Output;

                    objCmd.ExecuteNonQuery();

                    SqlInt32 ContactID = 0;
                    ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);

                    //We need contactID (PK) after insertion of the record
                    //it is needed to insert records in the table ContactWiseContactCategory
                    AddContactCategory(ContactID);

                    txtContactName.Text = "";
                    
                    lblMessage.Text = "Data Inserted SuccessFully With ContactID = " + ContactID.ToString();
                    cblContactCategoryID.ClearSelection();

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

        #region Add ContactCategory
        private void AddContactCategory(SqlInt32 ContactID)
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
                foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
                {
                    if (liContactCategoryID.Selected)
                    {
                        SqlCommand objCmd = objConn.CreateCommand();
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "[dbo].[PR_ContactWiseContactCategory_Insert]";
                        objCmd.Parameters.AddWithValue("@ContactID",ContactID.ToString());
                        objCmd.Parameters.AddWithValue("@ContactCategoryID", liContactCategoryID.Value.ToString());
                        objCmd.ExecuteNonQuery();
                    }
                }

                #endregion Set Connection & Command object
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

        #endregion Add ContactCategory

        #region DeleteContactCategory
        private void DeleteContactCategory(SqlInt32 ContactID)
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
                objCmd.CommandText = "[dbo].[PR_ContactWiseContactCategory_DeleteByPKContactID]";
                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"]);
                objCmd.ExecuteNonQuery();

                #endregion Set Connection & Command object

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

        #endregion DeleteContactCategory

        #region FillCBLContactCategoryID
        private void FillCBLContactCategoryID()
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

                objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectForDropDownList]";


                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    cblContactCategoryID.DataTextField = "ContactCategoryName";
                    cblContactCategoryID.DataValueField = "ContactCategoryID";
                    cblContactCategoryID.DataSource = objSDR;
                    cblContactCategoryID.DataBind();

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
        #endregion FillCBLContactCategoryID

    }
}
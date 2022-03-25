using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_Project6.Admin_Panel.ContactPhoto
{
    public partial class ContactPhotoAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {

            String ContactPhotoPath = "";

            if (fuContactPhotoPath.HasFile)
            {
                ContactPhotoPath = "~/UserContent/" + fuContactPhotoPath.FileName.ToString().Trim();
                fuContactPhotoPath.SaveAs(Server.MapPath(ContactPhotoPath));
            }
            #region Local Variables

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString.Trim());

            //Declare Local Variables to insert the data
            SqlString strContactName = SqlString.Null;
            SqlString strContactPhotoPath = SqlString.Null;

            string LogicalPath = "~/UserContent/"; 
            string AbsolutePath = "";
            #endregion Local Variables

            try
            {
                #region Server Side Validation

                //validate The Data
                String strErrorMessage = "";

                if (txtContactName.Text.Trim() == "")
                {
                    strErrorMessage += "- Enter ContactName <br/>";
                }

                #endregion Server Side Validation

                #region Gather The Information

                //Gather Information

                if (txtContactName.Text.Trim() != "")
                {
                    strContactName = txtContactName.Text.Trim();
                }


                #endregion Gather The Information

                #region Add/Edit File In Directory
                if (fuContactPhotoPath.HasFile)
                {
                    #region Create FolderPath , AbsolutePath & ContactPhotoPath
                    String FolderPath = "~/UserContent/";
                    AbsolutePath = Server.MapPath(FolderPath);
                    ContactPhotoPath = FolderPath + fuContactPhotoPath.FileName.ToString().Trim();
                    #endregion Create FolderPath , AbsolutePath & ContactPhotoPath

                    #region Create Directory If Does Not Exists
                    if (!Directory.Exists(AbsolutePath))
                    {
                        Directory.CreateDirectory(AbsolutePath);
                    }
                    #endregion Create Directory If Does Not Exists

                    #region Save File
                    fuContactPhotoPath.SaveAs(AbsolutePath + fuContactPhotoPath.FileName.ToString().Trim());
                    #endregion Save File
                }

                #endregion Add/Edit File In Directory

                #region set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                //Pass the parameters in the SP
                if(ContactPhotoPath != "")
                {
                    #region Calling Function To Delete OldFile From Directory

                    DeleteOldPhoto(Convert.ToInt32(Request.QueryString["ContactID"]));

                    #endregion Calling Function To Delete OldFile From Directory

                    objCmd.CommandText = "[dbo].[PR_ContactPhoto_Update]";
                    objCmd.Parameters.AddWithValue("@ContactName", strContactName);
                    objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);
                }
                else
                {
                    objCmd.CommandText = "[dbo].[PR_ContactPhoto_UpdateContactName]";
                    objCmd.Parameters.AddWithValue("@ContactName", strContactName);
                }
                

                #endregion set Connection & Command object

                if (Request.QueryString["ContactID"] != null)
                {
                    
                    #region Update Record

                    objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                    
                    objCmd.ExecuteNonQuery();
                   
                    Response.Redirect("~/Admin Panel/ContactPhoto/ContactPhotoList.aspx", true);

                    #endregion Update Record
                }

                else
                {
                    #region Insert Record

                    objCmd.CommandText = "[dbo].[PR_ContactPhoto_Insert]";
                    objCmd.ExecuteNonQuery();
                    lblMessage.Text = "Data Inserted SucessFully";
                    txtContactName.Text = "";

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

        #region Delete Old Photo
        private void DeleteOldPhoto(SqlInt32 ContactID)
         {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

             try
             {
                String LogicalPath = "";
                if (objConn.State != ConnectionState.Open)
                 {
                     objConn.Open();
                 }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_ContactPhoto_GetLogicalPath_ByContactID]";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        LogicalPath = objSDR["ContactPhotoPath"].ToString();
                    }
                }

                #region Delete Image
                String AbsolutePath = Server.MapPath(LogicalPath);
                FileInfo file = new FileInfo(AbsolutePath);

                if (file.Exists)
                {
                    file.Delete();
                    lblMessage.Text = "File deleted Successfully";
                }
                else
                {
                    lblMessage.Text = "Image dosen't upload!";
                }
                #endregion Delete Image


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
        #endregion Delete Old Photo

        #region Delete Contact Record
        private void DeleteContact(SqlInt32 ContactID)
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
                    objCmd.CommandText = "[dbo].[PR_ContactPhoto_DeleteByPK]";
                    objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
                    objCmd.ExecuteNonQuery();

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

        #endregion Delete Contact Record

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
                objCmd.CommandText = "[dbo].[PR_ContactPhoto_SelectByPK]";
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

                        if (!objSDR["ContactPhotoPath"].Equals(DBNull.Value))
                        {
                            imgPhoto.ImageUrl = objSDR["ContactPhotoPath"].ToString().Trim();
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
    }

}
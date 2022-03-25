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
    public partial class ContactList : System.Web.UI.Page
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

                objCmd.CommandText = "[dbo].[PR_ContactPhoto_SelectAll]";


                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    gvContactPhoto.DataSource = objSDR;
                    gvContactPhoto.DataBind();
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

        #region Delete Contact Photo
        private void DeleteContactPhoto(SqlInt32 ContactID)
        {
            String LogicalPath = "";
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

            try
            {
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
                    lblDisplay.Text = "File deleted Successfully";
                }
                else
                {
                    lblDisplay.Text = "Image dosen't upload!";
                }
                #endregion Delete Image



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


        #endregion Delete Contact Photo

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


        #endregion Delete Contact Record

        #region gvContact: RowCommand
        protected void gvContactPhoto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Delete Record

            lblDisplay.Text = "";
            if (e.CommandName == "DeleteRecord")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DeleteContactPhoto(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                    DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                }
            }

            #endregion Delete Record
        }

        #endregion gvContact: RowCommand

        
    }
}
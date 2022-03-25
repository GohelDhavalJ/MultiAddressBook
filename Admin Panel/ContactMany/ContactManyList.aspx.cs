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
    public partial class ContactManyList : System.Web.UI.Page
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
                //objCmd.CommandType = CommandType.Text;
                //objCmd.CommandType = CommandType.TableDirect;

                objCmd.CommandText = "[dbo].[PR_ContactWiseContactCategory_SelectAll]";

                //objCmd.ExecuteNonQuery();   //Insert/Update/Delete
                //objCmd.ExecuteReader();     //Select
                //objCmd.ExecuteScalar();     //Only One scalar value for example count function
                //objCmd.ExecuteXmlReader();  //xml type of data
                //SqlDataReader objSDR = objCmd.ExecuteReader();

                SqlDataAdapter objSDA = new SqlDataAdapter(objCmd);
                DataTable dt = new DataTable();
                objSDA.Fill(dt);
                gvContactMany.DataSource = dt;  
                gvContactMany.DataBind();

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

        #region gvContact: RowCommand
        protected void gvContactMany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Delete Record

            lblDisplay.Text = "";
            if (e.CommandName == "DeleteRecord")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                }
            }

            #endregion Delete Record
        }

        #endregion gvContact: RowCommand

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

                DeleteContactCategory(ContactID);

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_ContactMany_DeleteByPK]";
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

        #region Delete Contact Category
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
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());

                #endregion Set Connection & Command object

                objCmd.ExecuteNonQuery();

                lblDisplay.Text = "Contact Deleted SucessFully";

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
        #endregion Delete Contact Category


    }
}
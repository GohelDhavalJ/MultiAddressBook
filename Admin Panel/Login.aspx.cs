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

namespace Net_Project6.Admin_Panel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Login Button
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Validate User OrederParallelQuery not Validate User
            //Username,Password

            #region Local Variable

            //Declare Local Variables to insert the data
            SqlString strUserName = SqlString.Null;
            SqlString strPassword = SqlString.Null;

            #endregion Local Variable

            #region ServerSide Validation
            String strErrorMessage = "";

            if (txtUserNameLogin.Text.Trim() == "")
            {
                strErrorMessage += "- Enter UserName<br/>";
            }
            if (txtPasswordLogin.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Password<br/>";
            }

            if (strErrorMessage != "")
            {
                lblDisplay.Text = "Kinlay Slove following Error(s) <br/>";
                return;
            }
            #endregion ServerSide Validation

            #region Assign the Value

            if (txtUserNameLogin.Text.Trim() != "")
            {
                strUserName = txtUserNameLogin.Text.Trim();
            }
            if (txtPasswordLogin.Text.Trim() != "")
            {
                strPassword = txtPasswordLogin.Text.Trim();
            }

            #endregion Assign the Value

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

            try
            {
                #region set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "[dbo].[PR_User_SelectByUserNamepassword]";

                objCmd.Parameters.AddWithValue("@Username", strUserName);
                objCmd.Parameters.AddWithValue("@Password", strPassword);
                #endregion set Connection & Command object

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    //Valid User
                    lblDisplay.Text = "Valid User";

                    while (objSDR.Read())
                    {
                        if (!objSDR["UserID"].Equals(DBNull.Value))
                        {
                            Session["UserID"] = objSDR["UserID"].ToString().Trim();
                        }
                        if (!objSDR["DisplayName"].Equals(DBNull.Value))
                        {
                            Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                        }
                        break;
                    }
                    Response.Redirect("~/Admin Panel/Default.aspx", false);
                }
                else
                {
                    lblDisplay.Text = "Either username or password is not valid try again,with correct username and password";
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
        #endregion Login Button

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            #region Local Variable

            //Declare Local Variables to insert the data
            SqlString strUserName = SqlString.Null;
            SqlString strPassword = SqlString.Null;
            SqlString strDispalyName = SqlString.Null;
            SqlString strMobileNo = SqlString.Null;
            SqlString strEmail = SqlString.Null;

            #endregion Local Variable

            #region ServerSide Validation
            String strErrorMessage = "";

            if (txtUserNameRegistration.Text.Trim() == "")
            {
                strErrorMessage += "- Enter UserName<br/>";
            }
            if (txtPasswordRegistration.Text.Trim() == "")
            {
                strErrorMessage += "- Enter Password<br/>";
            }
            if (txtDisplayNameRegistration.Text.Trim() == "")
            {
                strErrorMessage += "- Enter DisplayName<br/>";
            }

            if (strErrorMessage != "")
            {
                lblDisplay.Text = "Kinlay Slove following Error(s) <br/>";
                return;
            }
            #endregion ServerSide Validation

            #region Assign the Value

            if (txtUserNameRegistration.Text.Trim() != "")
            {
                strUserName = txtUserNameRegistration.Text.Trim();
            }
            if (txtPasswordRegistration.Text.Trim() != "")
            {
                strPassword = txtPasswordRegistration.Text.Trim();
            }
            if (txtDisplayNameRegistration.Text.Trim() != "")
            {
                strDispalyName = txtDisplayNameRegistration.Text.Trim();
            }
            strMobileNo = txtMobileNoRegistration.Text.Trim();
            strEmail = txtEmailRegistration.Text.Trim();

            #endregion Assign the Value

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnetionString"].ConnectionString);

            try
            {
                #region set Connection & Command object

                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.AddWithValue("@Username", strUserName);
                objCmd.Parameters.AddWithValue("@Password", strPassword);
                objCmd.Parameters.AddWithValue("@DisplayName",strDispalyName);
                objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);
                objCmd.Parameters.AddWithValue("@Email", strDispalyName);


                #endregion set Connection & Command object


                #region Insert Record
                objCmd.CommandText = "[dbo].[PR_UserID_Insert]";
                objCmd.ExecuteNonQuery();

                lblDisplay.Text = "Data Inserted SucessFully";

                txtUserNameRegistration.Text = "";
                txtPasswordRegistration.Text = "";
                txtDisplayNameRegistration.Text = "";
                txtMobileNoRegistration.Text = "";
                txtEmailRegistration.Text = "";

                txtUserNameRegistration.Focus();

                #endregion Insert Record

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
    }
}
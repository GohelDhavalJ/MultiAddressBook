using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Net_Project6.App_Code
{
    public static class CommanDropDownFillMethods
    {
        #region FillDropDownListCountry
        public static void FillDropDownListCountry(DropDownList ddl)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "CountryID";
                    ddl.DataTextField = "CountryName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("---Select Country---", "-1"));

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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

        public static void FillDropDownListState(DropDownList ddl)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "StateID";
                    ddl.DataTextField = "StateName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("---Select State---", "-1"));

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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
        private static void FillDropDownListCity(DropDownList ddl)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "CityID";
                    ddl.DataTextField = "CityName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("---Select City---", "-1"));

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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
        private static void FillDropDownListContactCategory(DropDownList ddl)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "ContactCategoryID";
                    ddl.DataTextField = "ContactCategoryName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("Select ContactCategory", "-1"));

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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

        #region FillDropDownList state by CountryID
        public static void FillDropDownListStateByCountryID(DropDownList ddl, SqlInt32 CountryID)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "StateID";
                    ddl.DataTextField = "StateName";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("---Select State---", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
                    ddl.Items.Insert(0, new ListItem("---Select State---", "-1"));

                    ddl.Items.Clear();
                    ddl.Items.Insert(0, new ListItem("---Select City---", "-1"));
                }



                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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

        #region FillDropDownList City by StateID
        public static void FillDropDownListCityBySatateID(DropDownList ddl, SqlInt32 StateID)
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
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "CityID";
                    ddl.DataTextField = "CityName";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("---Select City---", "-1"));
                }
                else
                {
                    ddl.Items.Clear();
                    ddl.Items.Insert(0, new ListItem("---Select City---", "-1"));
                }

                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception ex)
            {

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
    }
}
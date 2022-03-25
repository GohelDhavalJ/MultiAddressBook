using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Net_Project6
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
            {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            #region Country Routing

            routes.MapPageRoute("AdminPanelCountryList", "Admin Panel/Country/List", "~/Admin Panel/Country/CountryList.aspx");

            routes.MapPageRoute("AdminPanelCountryAdd", "Admin Panel/Country/{OperationName}", "~/Admin Panel/Country/CountryAddEdit.aspx");

            routes.MapPageRoute("AdminPanelCountryEdit", "Admin Panel/Country/{OperationName}/{CountryID}", "~/Admin Panel/Country/CountryAddEdit.aspx");

            //1.String - RouteName : AdminPanelCountryList
            //2.String - RouteUrl  : Admin Panel/Country/List
            //3.String - PhysicalFile : ~/Admin Panel/Country/CountryList.aspx
            #endregion Country Routing

            #region State Routing

            routes.MapPageRoute("AdminPanelStateList", "Admin Panel/State/List", "~/Admin Panel/State/StateList.aspx");

            routes.MapPageRoute("AdminPanelStateAdd", "Admin Panel/State/{OperationName}", "~/Admin Panel/State/StateAddEdit.aspx");

            routes.MapPageRoute("AdminPanelStateEdit", "Admin Panel/State/{OperationName}/{StateID}", "~/Admin Panel/State/StateAddEdit.aspx");

            #endregion State Routing

            #region City Routing
            routes.MapPageRoute("AdminPanelCityList", "Admin Panel/City/List", "~/Admin Panel/City/CityList.aspx");

            routes.MapPageRoute("AdminPanelCityAdd", "Admin Panel/City/{OperationName}", "~/Admin Panel/City/CityAddEdit.aspx");

            routes.MapPageRoute("AdminPanelCityEdit", "Admin Panel/City/{OperationName}/{CityID}", "~/Admin Panel/City/CityAddEdit.aspx");
            #endregion City Routing

            #region ContactCategory Routing
            routes.MapPageRoute("AdminPanelContactCategoryList", "Admin Panel/ContactCategory/List", "~/Admin Panel/ContactCategory/ContactCategoryList.aspx");

            routes.MapPageRoute("AdminPanelContactCategoryAdd", "Admin Panel/ContactCategory/{OperationName}", "~/Admin Panel/ContactCategory/ContactCategoryAddEdit.aspx");

            routes.MapPageRoute("AdminPanelContactCategoryEdit", "Admin Panel/ContactCategory/{OperationName}/{ContactCategoryID}", "~/Admin Panel/ContactCategory/ContactCategoryAddEdit.aspx");
            #endregion ContactCategory Routing

            #region Contact Routing
            routes.MapPageRoute("AdminPanelContactList", "Admin Panel/Contact/List", "~/Admin Panel/Contact/ContactList.aspx");

            routes.MapPageRoute("AdminPanelContactAdd",
                "Admin Panel/Contact/{OperationName}", 
                "~/Admin Panel/Contact/ContactAddEdit.aspx");

            routes.MapPageRoute("AdminPanelContactEdit",
                "Admin Panel/Contact/{OperationName}/{ContactID}",
                "~/Admin Panel/Contact/ContactAddEdit.aspx");
            #endregion Contact Routing


        }
    }
}
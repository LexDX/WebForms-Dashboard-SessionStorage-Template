using DevExpress.DashboardWeb;
using System;


namespace DXWebApplication1 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ASPxDashboard1.SetDashboardStorage(new SessionDashboardStorage(new string[] { "dashboard1" }, string.Format(@"~/App_Data/Dashboards/")));
            //ASPxDashboard1.SetDashboardStorage(new DashboardFileStorage("~/App_Data/Dashboards")); // Uncomment this line to use the App_Data/Dashboards folder to store dashboards.
        }
    }
}
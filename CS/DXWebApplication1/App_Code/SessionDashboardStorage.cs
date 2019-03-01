using DevExpress.DashboardWeb;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Xml.Linq;
public class SessionDashboardStorage : DashboardStorageBase {
    const string DashboardStorageKey = "DashboardStorage";
    Dictionary<string, XDocument> Storage {
        get {
            HttpSessionState session = HttpContext.Current.Session;
            if (session != null) {
                Dictionary<string, XDocument> storage = session[DashboardStorageKey] as Dictionary<string, XDocument>;
                if (storage == null) {
                    storage = new Dictionary<string, XDocument>();
                    session[DashboardStorageKey] = storage;
                    return storage;
                }
                return storage;
            }
            throw new Exception();
        }
    }
    public SessionDashboardStorage(string[] dashboardIds, string path) : base() {
        for (int i = 0; i < dashboardIds.Length; i++) {
            RegisterDefaultDashboard(dashboardIds[i], path);
        }
    }
    protected override IEnumerable<string> GetAvailableDashboardsID() {
        return Storage.Keys;
    }
    protected override XDocument LoadDashboard(string dashboardID) {
        XDocument document = Storage[dashboardID];
        return document;
    }
    protected override void SaveDashboard(string dashboardID, XDocument dashboard, bool createNew) {
        if (createNew ^ Storage.ContainsKey(dashboardID))
            Storage[dashboardID] = dashboard;
    }
    public void RegisterDashboard(string dashboardID, XDocument dashboard) {
        SaveDashboard(dashboardID, dashboard, true);
    }
    void RegisterDefaultDashboard(string dashboardId, string path) {
        string dashboardLocalPath = HttpContext.Current.Server.MapPath(path + dashboardId + ".xml");
        RegisterDashboard(dashboardId, XDocument.Load(dashboardLocalPath));
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using Newtonsoft.Json;

namespace ElectiveService
{

    public class VisitCountData
    {
        [JsonProperty("totalVisits")]
        public int TotalVisits { get; set; }
    }
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            string path = Server.MapPath("~/App_Data/visit-count.json");
            string json = File.ReadAllText(path);
            VisitCountData data = JsonConvert.DeserializeObject<VisitCountData>(json);

            if (data == null) {
                throw new InvalidDataException("invalid json content.");
            }

            Application["VisitorCount"] = data.TotalVisits;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Response.Redirect("~/error.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            try
            {
                int newCount = Convert.ToInt32(Application["VisitorCount"] ?? 0) + 1;
                string path = Server.MapPath("~/App_Data/visit-count.json");

                var data = new VisitCountData { TotalVisits = newCount };
                File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));

                Application["VisitorCount"] = newCount;
            }
            finally
            {
                Application.UnLock();
            }
        }

    }
}

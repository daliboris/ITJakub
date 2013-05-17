﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ujc.Naki.MVCWebLayer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "SearchDetailType",
                "hledani/{searchTerm}/detail-podle-druhu",
                new { controller = "Search", action = "DetailByType", searchTerm = "" }
            );

            routes.MapRoute(
                "SearchDetail",
                "hledani/{searchTerm}/detail",
                new { controller = "Search", action = "Detail", searchTerm = "" }
            );

            routes.MapRoute(
                "SearchSearch",
                "hledani",
                new { controller = "Search", action = "Search" }
            );

            routes.MapRoute(
                "SourcesMain",
                "zdroje",
                new { controller = "Sources", action = "Listing", mode = "Jmeno", alphabet = "A" }
            );

            routes.MapRoute(
                "SourcesListing",
                "zdroje/listovani/{mode}/{alphabet}",
                new { controller = "Sources", action = "Listing", mode = "Jmeno", alphabet = "A" }
            );

            routes.MapRoute(
                "SourcesDetail",
                "zdroje/{id}",
                new { controller = "Sources", action = "Detail", part = "Info", id = "1-zizka" }
            );

            routes.MapRoute(
                "SourcesDetailSearch",
                "zdroje/{id}/hledani/{searchTerm}",
                new { controller = "Sources", action = "DetailHledat", searchTerm = "" }
            );

            routes.MapRoute(
                "SourcesGoThrough",
                "zdroje/{id}/plny-text/{page}",
                new { controller = "Sources", action = "Prochazet", page = 1 }
            );

            routes.MapRoute(
                "SourcesGoThroughWithId",
                "zdroje/{id}/plny-text",
                new { controller = "Sources", action = "Prochazet", id = 1 }
            );

            routes.MapRoute(
                "SourcesDetailZpracovani",
                "zdroje/{id}/zpracovani-dokumentu",
                new { controller = "Sources", action = "Detail", part = "Zpracovani", id = "1-zizka" }
            );

            routes.MapRoute(
                "SourcesDetailPodminky",
                "zdroje/{id}/podminky-uziti",
                new { controller = "Sources", action = "Detail", part = "Podminky", id = "1-zizka" }
            );

            routes.MapRoute(
                "StaroceskaBanka",
                "staroceska-textova-banka/{id}",
                new { controller = "Modules", action = "Index" }
            );

            routes.MapRoute(
                "HomePage",
                "homepage/vyhledavani",
                new { controller = "Index", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Index", action = "Index" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
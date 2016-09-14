﻿using System.Web.Mvc;

namespace ITJakub.Web.Hub.Areas.Tools
{
    public class ToolsAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get { return "Tools"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterRoutes(context);
        }

        private void RegisterRoutes(AreaRegistrationContext context)
        {
            RouteConfig.RegisterRoutes(context);
        }
    }
}
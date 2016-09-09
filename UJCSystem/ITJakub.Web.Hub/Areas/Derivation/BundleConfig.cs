﻿using System.Web.Optimization;

namespace ITJakub.Web.Hub.Areas.Derivation
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/itjakub/derivation/javascript").Include(
                "~/Areas/Derivation/Scripts/itjakub.derivation.js",
                "~/wwwroot/js/Plugins/Lemmatization/itjakub.lemmatization.shared.js"));

            bundles.Add(new StyleBundle("~/itjakub/derivation/css")
                .Include("~/Areas/Derivation/Content/itjakub.derivation.css"));
        }
    }
}
﻿using System.Web;
using System.Web.Mvc;
using ITJakub.Shared.Contracts.Notes;
using ITJakub.Web.Hub.Identity;
using ITJakub.Web.Hub.Managers;
using ITJakub.Web.Hub.Models;
using Microsoft.AspNet.Identity.Owin;

namespace ITJakub.Web.Hub.Controllers
{
    public class HomeController : BaseController
    {
        private readonly StaticTextManager m_staticTextManager;

        public HomeController(StaticTextManager staticTextManager)
        {
            m_staticTextManager = staticTextManager;
        }

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Copyright()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(username))
            {
                return View();
            }

            using (var client = GetEncryptedClient())
            {
                var user = client.FindUserByUserName(username);
                var viewModel = new FeedbackViewModel
                {
                    Name = string.Format("{0} {1}", user.FirstName, user.LastName),
                    Email = user.Email
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(FeedbackViewModel model)
        {
           
            using (var client = GetMainServiceClient())
            {
                if (Request.IsAuthenticated)
                {
                    client.CreateFeedback(model.Text, GetUserName(), FeedbackCategoryEnumContract.None);
                }
                else
                {
                    client.CreateAnonymousFeedback(model.Text, model.Name, model.Email, FeedbackCategoryEnumContract.None);
                }
            }

            return View("Index");
        }

        public ActionResult HowToCite()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Support()
        {
            var staticTextViewModel = m_staticTextManager.GetRenderedHtmlText(StaticTexts.TextHomeSupport);
            return View(staticTextViewModel);
        }

        public ActionResult GetTypeaheadAuthor(string query)
        {
            using (var client = GetMainServiceClient()) { 
            var result = client.GetTypeaheadAuthors(query);
            return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTypeaheadTitle(string query)
        {
            using (var client = GetMainServiceClient()) { 
                var result = client.GetTypeaheadTitles(query);
            return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTypeaheadDictionaryHeadword(string query)
        {
            using (var client = GetMainServiceClient())
            {
                var result = client.GetTypeaheadDictionaryHeadwords(null, null, query, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
}
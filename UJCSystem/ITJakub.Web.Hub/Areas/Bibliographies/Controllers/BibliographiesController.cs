﻿using System.Web.Mvc;
using ITJakub.Shared.Contracts;
using ITJakub.Shared.Contracts.Notes;
using ITJakub.Web.Hub.Controllers;
using ITJakub.Web.Hub.Models;

namespace ITJakub.Web.Hub.Areas.Bibliographies.Controllers
{
    [RouteArea("Bibliographies")]
    public class BibliographiesController : BaseController
    {
        public ActionResult Index()
        {
            return View("Search");
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Information()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            var username = HttpContext.User.Identity.Name;
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
            var username = HttpContext.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(username))
            {
                using (var client = GetUnsecuredClient())
                    client.CreateAnonymousFeedback(model.Text, model.Name, model.Email, FeedbackCategoryEnumContract.Bibliographies);
            }
            else
            {
                using (var client = GetAuthenticatedClient())
                {
                    client.CreateFeedback(model.Text, username, FeedbackCategoryEnumContract.Bibliographies);
                }
            }

            return View("Information");
        }

        public ActionResult SearchTerm(string term)
        {
            using (var client = GetUnsecuredClient())
            {
                var listBooks = client.Search(term);
                foreach (var list in listBooks)
                {
                    list.CreateTimeString = list.CreateTime.ToString();
                }
                return Json(new {books = listBooks}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTypeaheadAuthor(string query)
        {
            using (var client = GetUnsecuredClient())
            {
                var result = client.GetTypeaheadAuthorsByBookType(query, BookTypeEnumContract.BibliographicalItem);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTypeaheadTitle(string query)
        {
            using (var client = GetUnsecuredClient())
            {
                var result = client.GetTypeaheadTitlesByBookType(query, BookTypeEnumContract.BibliographicalItem, null, null);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;
using ITJakub.ITJakubService.DataContracts.Contracts;
using ITJakub.Shared.Contracts;
using ITJakub.Shared.Contracts.Notes;
using ITJakub.Shared.Contracts.Searching.Results;
using ITJakub.Web.Hub.Controllers;
using ITJakub.Web.Hub.Models;
using Microsoft.Ajax.Utilities;

namespace ITJakub.Web.Hub.Areas.CardFiles.Controllers
{
    [RouteArea("CardFiles")]
    public class CardFilesController : BaseController
    {
        public ActionResult Index()
        {
            return View("List");
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Listing()
        {
            return View();
        }

        public ActionResult List(string term)
        {
            return View();
        }

        public ActionResult SearchList(string term)
        {
            term = term.ToLower();

            using (var client = GetUnsecuredClient())
            {
                var cardFiles = client.GetCardFiles();
                var result = new List<SearchResultContract>();
                foreach (var cardFile in cardFiles)
                {
                    if (cardFile.Name.ToLower().Contains(term) || cardFile.Description.ToLower().Contains(term) || term.IsNullOrWhiteSpace())
                    {
                        result.Add(new SearchResultContract
                        {
                            Title = cardFile.Name,
                            BookXmlId = cardFile.Id,
                            SubTitle = cardFile.Description,
                            BookType = BookTypeEnumContract.CardFile
                        });
                    }
                }
                return Json(new {books = result}, JsonRequestBehavior.AllowGet);
            }
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
                {
                    client.CreateAnonymousFeedback(model.Text, model.Name, model.Email, FeedbackCategoryEnumContract.CardFiles);
                }
            }
            else
            {
                using (var client = GetAuthenticatedClient())
                {
                    client.CreateFeedback(model.Text, username, FeedbackCategoryEnumContract.CardFiles);
                }
            }

            return View("Information");
        }

        public ActionResult CardFiles()
        {
            using (var client = GetUnsecuredClient())
            {
                var cardFiles = client.GetCardFiles();
                return Json(new {cardFiles}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Buckets(string cardFileId, string headword)
        {
            using (var client = GetUnsecuredClient())
            {
                var buckets = headword.IsEmpty()
                    ? client.GetBuckets(cardFileId)
                    : client.GetBucketsWithHeadword(cardFileId, headword);
                return Json(new {buckets}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Cards(string cardFileId, string bucketId)
        {
            using (var client = GetUnsecuredClient())
            {
                var cards = client.GetCards(cardFileId, bucketId);
                return Json(new {cards}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CardsShort(string cardFileId, string bucketId)
        {
            using (var client = GetUnsecuredClient())
            {
                var cards = client.GetCardsShort(cardFileId, bucketId);
                return Json(new {cards}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Card(string cardFileId, string bucketId, string cardId)
        {
            using (var client = GetUnsecuredClient())
            {
                var card = client.GetCard(cardFileId, bucketId, cardId);
                return Json(new {card}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Image(string cardFileId, string bucketId, string cardId, string imageId, string imageSize)
        {
            ImageSizeEnum imageSizeEnum;
            var parsingSucceeded = Enum.TryParse(imageSize, true, out imageSizeEnum);

            if (!parsingSucceeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "invalid image size");
            }
            using (var client = GetUnsecuredClient())
            {
                var imageDataStream = client.GetImage(cardFileId, bucketId, cardId, imageId, imageSizeEnum);
                return new FileStreamResult(imageDataStream, "image/jpeg");
            }
        }
    }
}
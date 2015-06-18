﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITJakub.Contracts.Categories;
using ITJakub.Contracts.Searching;
using ITJakub.MVCWebLayer.Services;
using ITJakub.MVCWebLayer.ViewModels;

namespace ITJakub.MVCWebLayer.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchResultProvider m_resultsProvider = new ItJakubSearchProvider();

        [HttpGet]
        public ActionResult Search(SearchViewModel model)
        {

            KeyWordsResponse response = m_resultsProvider.GetSearchResults(model.SearchTerm, ParseParamList(model.Kategorie), ParseParamList(model.Dila));

            List<string> parsedCategories = ParseParamList(model.Kategorie);
            List<string> parsedBooks = ParseParamList(model.Dila);

            return View(new SearchResultViewModel
                {
                    Search = new SearchViewModel
                        {
                            SearchTerm = model.SearchTerm,
                            Dila = model.Dila,
                            Kategorie = model.Kategorie
                        },
                    FoundWords = response.FoundTerms,
                    FoundInBooks = response.FoundInBooks,
                    Categories = response.CategoryTree,
                    SelectedCategoryIds = parsedCategories,
                    SelectedBookIds = parsedBooks,
                    CategoryIds = model.Kategorie,
                    BookIds = model.Dila,
                });
        }

        private List<string> ParseParamList(string paramList)
        {
            if (string.IsNullOrWhiteSpace(paramList)) return new List<string>();
            var splitted = paramList.Split(' ');
            return splitted.ToList();
        }

        [HttpGet]
        public ActionResult GetCategoryChildren(string categoryId)
        {
            List<SelectionBase> children;
            
            if (string.IsNullOrEmpty(categoryId))
                children = m_resultsProvider.GetRootCategories();
            else
                children = m_resultsProvider.GetCategoryChildrenById(categoryId);

            foreach (var category in children)
            {
                Category childAsCategory = category as Category;
                if (childAsCategory != null)
                {
                    childAsCategory.Subitems = new List<SelectionBase>();
                }
            }

            return View("GetCategoryChildren", null, new CategoriesViewModel { Children = children, CategoryId = categoryId });
        }

        [HttpGet]
        public ActionResult Detail(SearchViewModel model)
        {
            var searchResultWithHtmlContexts = m_resultsProvider.GetHtmlContextForKeyWord(model.SelectedTerm, ParseParamList(model.Kategorie), ParseParamList(model.Dila));
            searchResultWithHtmlContexts.Sort((x,y)=>x.ShowOrder.CompareTo(y.ShowOrder));
            return View("Detail", null, new SearchKeyWordsViewModel
                {
                    Results = searchResultWithHtmlContexts
                });
        }

        [HttpGet]
        public ActionResult DetailByType(SearchViewModel model)
        {
            List<string> bookIds = new List<string> {model.BookId};
            List<SearchResultWithHtmlContext> searchResultWithHtmlContexts = m_resultsProvider.GetHtmlContextForKeyWord(model.SearchTerm, new List<string>(), bookIds);
            searchResultWithHtmlContexts.Sort((x, y) => x.ShowOrder.CompareTo(y.ShowOrder));


            return View("DetailByType", null, new SearchKeyWordsViewModel { Results = searchResultWithHtmlContexts });

            //return View("DetailByType", null, model.BookId);
        }
    }
}
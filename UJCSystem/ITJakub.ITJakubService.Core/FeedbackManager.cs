using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ITJakub.DataEntities.Database.Entities;
using ITJakub.DataEntities.Database.Entities.Enums;
using ITJakub.DataEntities.Database.Repositories;
using ITJakub.Shared.Contracts.Notes;
using FeedbackSortEnum = ITJakub.DataEntities.Database.Entities.Enums.FeedbackSortEnum;

namespace ITJakub.ITJakubService.Core
{
    public class FeedbackManager
    {
        private readonly FeedbackRepository m_feedbackRepository;
        private readonly BookVersionRepository m_bookVersionRepository;
        private readonly AuthorizationManager m_authorizationManager;
        private readonly UserRepository m_userRepository;

        public FeedbackManager(UserRepository userRepository, FeedbackRepository feedbackRepository, BookVersionRepository bookVersionRepository, AuthorizationManager authorizationManager)
        {
            m_userRepository = userRepository;
            m_feedbackRepository = feedbackRepository;
            m_bookVersionRepository = bookVersionRepository;
            m_authorizationManager = authorizationManager;
        }

        public void CreateFeedback(string note, string username, FeedbackCategoryEnumContract feedbackCategory)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is empty, cannot add bookmark");

            User user = m_userRepository.FindByUserName(username);
            if (user == null)
                throw new ArgumentException(string.Format("Cannot locate user by username: '{0}'", username));

            Feedback entity = new Feedback {CreateDate = DateTime.UtcNow, Text = note, User = user, Category = (FeedbackCategoryEnum) feedbackCategory};
            m_feedbackRepository.Save(entity);
        }

        public void CreateAnonymousFeedback(string feedback, string name, string email, FeedbackCategoryEnumContract feedbackCategory)
        {
            Feedback entity = new Feedback
            {
                CreateDate = DateTime.UtcNow,
                Text = feedback,
                Name = name,
                Email = email,
                Category = (FeedbackCategoryEnum) feedbackCategory
            };
            m_feedbackRepository.Save(entity);
        }

        public void CreateAnonymousFeedbackForHeadword(string feedback, string bookXmlId, string versionXmlId, string entryXmlId, string name, string email)
        {
            BookHeadword headwordEntity = m_bookVersionRepository.GetFirstHeadwordInfo(bookXmlId, entryXmlId, versionXmlId);

            HeadwordFeedback entity = new HeadwordFeedback
            {
                CreateDate = DateTime.UtcNow,
                Text = feedback,
                Name = name,
                Email = email,
                BookHeadword = headwordEntity,
                Category = FeedbackCategoryEnum.Dictionaries
            };
            m_feedbackRepository.Save(entity);
        }

        public void CreateFeedbackForHeadword(string feedback, string bookXmlId, string versionXmlId, string entryXmlId, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is empty, cannot add bookmark");

            User user = m_userRepository.FindByUserName(username);
            if (user == null)
                throw new ArgumentException(string.Format("Cannot locate user by username: '{0}'", username));

            BookHeadword headwordEntity = m_bookVersionRepository.GetFirstHeadwordInfo(bookXmlId, entryXmlId, versionXmlId);
            if (headwordEntity == null)
                throw new ArgumentException(string.Format("Cannot find headword with bookId: {0}, versionId: {1}, entryXmlId: {2}", bookXmlId, versionXmlId,
                    entryXmlId));

            HeadwordFeedback entity = new HeadwordFeedback
            {
                CreateDate = DateTime.UtcNow,
                Text = feedback,
                BookHeadword = headwordEntity,
                User = user,
                Category = FeedbackCategoryEnum.Dictionaries
            };
            m_feedbackRepository.Save(entity);
        }

        public List<FeedbackContract> GetFeedbacks(FeedbackCriteriaContract feedbackSearchCriteria)
        {
            m_authorizationManager.CheckUserCanManageFeedbacks();

            var sortCriteria = feedbackSearchCriteria.SortCriteria;
            var categoriesContract = feedbackSearchCriteria.Categories;
            var categories = categoriesContract != null && categoriesContract.Count > 0
                ? categoriesContract.Select(category => (FeedbackCategoryEnum) category).ToList()
                : null;
            
            var feedbacks = m_feedbackRepository.GetFeedbacks(categories, (FeedbackSortEnum) sortCriteria.SortByField, sortCriteria.SortAsc,
                feedbackSearchCriteria.Start, feedbackSearchCriteria.Count);
            var resultFeedbacks = Mapper.Map<List<FeedbackContract>>(feedbacks);

            if (feedbacks.Any(x => x.FeedbackType == FeedbackType.Headword))
            {
                var headwordFeedbacks = feedbacks.Where(x => x.FeedbackType == FeedbackType.Headword).Cast<HeadwordFeedback>();
                var headwordFeedbacksDetail = m_feedbackRepository.GetHeadwordFeedbacksById(headwordFeedbacks.Select(x => x.Id));
                var headwordFeedbacksDetailDict = headwordFeedbacksDetail.ToDictionary(x => x.Id);

                foreach (var resultFeedback in resultFeedbacks.Where(x => x.FeedbackType == FeedbackTypeEnumContract.Headword))
                {
                    resultFeedback.HeadwordInfo = Mapper.Map<FeedbackHeadwordInfoContract>(headwordFeedbacksDetailDict[resultFeedback.Id].BookHeadword);
                }
            }

            return resultFeedbacks;
        }

        public int GetFeedbacksCount(FeedbackCriteriaContract feedbackSearchCriteria)
        {
            m_authorizationManager.CheckUserCanManageFeedbacks();

            var categoriesContract = feedbackSearchCriteria.Categories;
            var categories = categoriesContract != null && categoriesContract.Count > 0
                ? categoriesContract.Select(category => (FeedbackCategoryEnum) category).ToList()
                : null;
            return m_feedbackRepository.GetFeedbacksCount(categories);
        }

        public void DeleteFeedback(long feedbackId)
        {
            m_authorizationManager.CheckUserCanManageFeedbacks();
            m_feedbackRepository.DeleteFeedback(feedbackId);
        }
    }
}
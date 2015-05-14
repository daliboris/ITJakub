﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ITJakub.MobileApps.Client.Fillwords.ViewModel;
using ITJakub.MobileApps.Client.Shared.Communication;

namespace ITJakub.MobileApps.Client.Fillwords.DataService
{
    public class FillwordsDataService
    {
        private readonly TaskManager m_taskManager;
        private readonly IErrorService m_errorService;

        public FillwordsDataService(ISynchronizeCommunication applicationCommunication)
        {
            m_errorService = applicationCommunication.ErrorService;
            m_taskManager = new TaskManager(applicationCommunication);
        }

        public IErrorService ErrorService
        {
            get { return m_errorService; }
        }

        public void CreateTask(string taskName, string bookRtfContent, IList<OptionsViewModel> optionsList, Action<Exception> callback)
        {
            m_taskManager.CreateTask(taskName, bookRtfContent, optionsList, callback);
        }

        public void SetTaskAndGetData(string data, Action<TaskViewModel> callback)
        {
            m_taskManager.SetTaskAndGetData(data, callback);
        }

        public void EvaluateTask(Action<EvaluationResultViewModel, Exception> callback)
        {
            m_taskManager.EvaluateTask(callback);
        }

        public void GetTaskResults(Action<TaskFinishedViewModel, Exception> callback)
        {
            m_taskManager.GetTaskResults(callback);
        }

        public void StartPollingResults(Action<ObservableCollection<UserResultViewModel>, Exception> callback)
        {
            m_taskManager.StartPollingResults(callback);
        }

        public void StopPolling()
        {
            m_taskManager.StopPolling();
        }
    }
}
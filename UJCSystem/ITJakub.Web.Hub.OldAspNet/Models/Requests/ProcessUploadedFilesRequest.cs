﻿namespace ITJakub.Web.Hub.Models.Requests
{
    public class ProcessUploadedFilesRequest
    {
        public string SessionId { get; set; }

        public string UploadMessage { get; set; }
    }
}
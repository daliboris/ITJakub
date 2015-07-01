﻿using Newtonsoft.Json;

namespace ITJakub.MobileApps.Client.Fillwords.DataContract
{
    public class FillwordsAnswerContract
    {
        [JsonProperty("WordPosition")]
        public int WordPosition { get; set; }

        [JsonProperty("SelectedAnswer")]
        public string SelectedAnswer { get; set; }
    }
}
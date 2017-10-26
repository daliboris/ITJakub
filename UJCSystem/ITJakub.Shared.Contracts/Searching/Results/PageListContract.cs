﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace ITJakub.Shared.Contracts.Searching.Results
{
    [DataContract]
    public class PageListContract
    {
        [DataMember]
        public IList<PageDescriptionContract> PageList { get; set; }

        public virtual string ToXml()
        {
            using (Stream stream = new MemoryStream())
            {
                //Serialize the Record object to a memory stream using DataContractSerializer. 
                DataContractSerializer serializer = new DataContractSerializer(GetType());
                serializer.WriteObject(stream, this);


                stream.Position = 0;
                string result = new StreamReader(stream).ReadToEnd();

                return result;
            }
        }

        public static PageListContract FromXml(string xml)
        {
            using (Stream stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();

                stream.Position = 0;
                //object result = new XmlSerializer(typeof (Server)).Deserialize(stream); 

                object result = new DataContractSerializer(typeof(PageListContract)).ReadObject(stream);
                return (PageListContract)result;
            }
        }
    }
}
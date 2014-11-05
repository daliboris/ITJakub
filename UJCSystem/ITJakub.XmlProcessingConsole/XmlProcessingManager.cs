﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ITJakub.XmlProcessingConsole
{
    public class XmlProcessingManager
    {
        private const string HeaderElement = "teiHeader";
        private const string RootElement = "TEI";
        private readonly XNamespace m_nlpNamespace = "http://vokabular.ujc.cas.cz/ns/tei-nlp/1.0";
        private readonly XNamespace m_teiNamespace = "http://www.tei-c.org/ns/1.0";

        public BookVersion GetMetadataFromXml(FileStream fileStream)
        {
            XDocument header = ParseHeader(fileStream);
            return GetInfoFromHeader(header);
        }

        private BookVersion GetInfoFromHeader(XDocument document)
        {
            return new BookVersion
            {
                Book = GetBookInfo(document),
                Name = GetBookVersionName(document),
                Authors = GetBookVersionAuthors(document)
            };
        }

        private Book GetBookInfo(XDocument document)
        {
            string guid = document.Root.Attribute("n").Value;
            return new Book { Guid = guid };
        }

        private string GetBookVersionName(XDocument document)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(document);
            string name = string.Join(" ", document.Root.Descendants(AddNS(m_teiNamespace, "title")).FirstOrDefault()
                .Descendants(AddNS(m_teiNamespace, "w"))
                .Select(x => x.Value)
                .ToArray());
            return name;
        }

        private List<Author> GetBookVersionAuthors(XDocument document)
        {
            var authors = new List<Author>();
            foreach (XElement authorElement in document.Root.Descendants(AddNS(m_teiNamespace, "author")))
            {
                var author = new Author
                {
                    Name =
                        string.Join(" ",
                            authorElement.Descendants(AddNS(m_teiNamespace, "w")).Select(x => x.Value).ToArray())
                };
                authors.Add(author);
            }

            return authors;
        }

        private string AddNS(XNamespace nameSpace, string element)
        {
            return string.Concat(nameSpace, element);
        }

        private XDocument ParseHeader(FileStream fileStream)
        {
            XElement rootElement = null;
            var xmlTextReader = new XmlTextReader(fileStream)
            {
                WhitespaceHandling = WhitespaceHandling.None,
                Namespaces = true
            };

            while (xmlTextReader.Read())
            {
                if (xmlTextReader.NodeType == XmlNodeType.Element &&
                    xmlTextReader.LocalName == RootElement &&
                    xmlTextReader.IsStartElement())
                {
                    IDictionary<string, string> namespaces = xmlTextReader.GetNamespacesInScope(XmlNamespaceScope.Local);
                    //rootElement = new XElement(xmlTextReader.Name, namespaces);
                    rootElement = new XElement(xmlTextReader.Name);
                    if (xmlTextReader.HasAttributes)
                    {
                        xmlTextReader.MoveToFirstAttribute();
                        while (xmlTextReader.MoveToNextAttribute())
                        {
                            string attrName = xmlTextReader.Name;
                            var attribute = (XAttribute) xmlTextReader;
                            if (attrName.Equals("xmlns"))
                            {
                                XNamespace xNamespace = xmlTextReader.Value;
                                rootElement.SetAttributeValue("xmlns", xNamespace.NamespaceName);
                                rootElement.Name = xNamespace + rootElement.Name.LocalName;
                            }
                            else if (xmlTextReader.IsNa)
                            {
                                string[] nmspaces = attrName.Split(':');
                                XNamespace nspace;
                                if (namespaces.ContainsKey(nmspaces[0]))
                                {
                                    nspace = namespaces[nmspaces[0]];
                                } else if (nmspaces[0].Equals("xmlns"))
                                {
                                    nspace = namespaces[""];
                                }
                                else
                                {
                                    nspace = nmspaces[0];
                                }
                                rootElement.Add(new XAttribute(nspace + nmspaces[1], xmlTextReader.Value));
                            }
                            else
                            {
                                rootElement.Add(new XAttribute(attrName, xmlTextReader.Value));
                            }
                        }
                        xmlTextReader.MoveToElement();
                    }
                }

                if (xmlTextReader.NodeType == XmlNodeType.Element &&
                    xmlTextReader.LocalName == HeaderElement &&
                    xmlTextReader.IsStartElement())
                {
                    if (rootElement == null) return null; //TODO throw exception here
                    Console.WriteLine(rootElement);
                    rootElement.AddFirst(XElement.Parse(xmlTextReader.ReadOuterXml()));
                    var xdocument = new XDocument(rootElement);
                    return xdocument;
                }
            }
            return null;
        }
    }


    public class BookVersion
    {
        public string VersionId { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; } //SQLDB createTime
        public string Description { get; set; }

        public Book Book { get; set; }
        public Transformation Transformation { get; set; }
        public Publisher Publisher { get; set; }
        public string PublishPlace { get; set; }
        public string PublishDate { get; set; }
        public string Copyright { get; set; }
        public int AvailabilityStatus { get; set; }
        public int BiblText { get; set; }

        public IList<Author> Authors { get; set; }
        public IList<Responsible> Responsibles { get; set; }
        public IList<BookBibl> BookBibls { get; set; }
    }

    public class Book
    {
        public string Guid { get; set; }
        public Category Category { get; set; }
        public BookType BookType { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public Category ParentCategory { get; set; }
    }

    public class BookType
    {
        public string Type { get; set; }
    }

    public class Transformation
    {
        public bool IsDefault { get; set; }
        public int ResultType { get; set; }
        public BookType BookType { get; set; }
    }

    public class Publisher
    {
        public string Text { get; set; }
        public string Email { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }

    public class Responsible
    {
        public string Text { get; set; }

        public ResponsibleType ResponsibleType { get; set; }
    }

    public class ResponsibleType
    {
        public string Text { get; set; }

        public int Type { get; set; }
    }

    public class BookBibl
    {
        public string Text { get; set; }

        public string Type { get; set; }
        public string SubType { get; set; }
        public int BiblType { get; set; }
    }
}
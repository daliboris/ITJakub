using System;
using Vokabular.DataEntities.Database.Entities.Enums;

namespace Vokabular.DataEntities.Database.Entities
{
    public class ResourceVersion : IEquatable<ResourceVersion>
    {
        public virtual long Id { get; set; }

        public virtual int VersionNumber { get; set; }

        public virtual string Comment { get; set; }

        public virtual DateTime CreateTime { get; set; }
        
        public virtual Resource Resource { get; set; }

        public virtual Resource ParentResource { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual bool Equals(ResourceVersion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ResourceVersion) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class MetadataResource : ResourceVersion
    {
       public virtual string Title { get; set; }
       public virtual string SubTitle { get; set; }
       public virtual string RelicAbbreviation { get; set; }
	   public virtual string SourceAbbreviation { get; set; }
	   public virtual Publisher Publisher { get; set; }
	   public virtual string PublishPlace { get; set; }
	   public virtual string PublishDate { get; set; }
	   public virtual string Copyright { get; set; }
	   public virtual string BiblText { get; set; }
	   public virtual string OriginDate { get; set; }
	   public virtual DateTime? NotBefore { get; set; }
       public virtual DateTime? NotAfter { get; set; }
       public virtual string ManuscriptIdno { get; set; }
	   public virtual string ManuscriptSettlement { get; set; }
	   public virtual string ManuscriptCountry { get; set; }
	   public virtual string ManuscriptRepository { get; set; }
	   public virtual string ManuscriptExtent { get; set; }
       public virtual string ManuscriptTitle { get; set; }
    }

    public class PageResource : ResourceVersion
    {
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
    }

    public class TextResource : ResourceVersion
    {
        public virtual string ExternalId { get; set; }
    }
    
    public class ImageResource : ResourceVersion
    {
        public virtual string FileName { get; set; }
        public virtual string MimeType { get; set; }
        public virtual int Size { get; set; }
    }

    public class AudioResource : ResourceVersion
    {
        public virtual long Duration { get; set; }
        public virtual string FileName { get; set; }
        public virtual AudioTypeEnum AudioType { get; set; }
        public virtual string MimeType { get; set; }
    }

    public class ChapterResource : ResourceVersion
    {
        public virtual string Name { get; set; }
        public virtual int Position { get; set; }
        public virtual Resource BeginningPageResource { get; set; }
    }

    public class DefaultHeadwordResource : ResourceVersion
    {
        public virtual string ExternalId { get; set; }
        public virtual string DefaultHeadword { get; set; }
        public virtual string Sorting { get; set; }
    }

    public class HeadwordResource : ResourceVersion
    {
        public virtual string Headword { get; set; }
        public virtual string HeadwordOriginal { get; set; }
        public virtual Resource PageResource { get; set; }
    }

    public class TermResource : ResourceVersion
    {
        public virtual Term Term { get; set; }
    }

    public class KeywordResource : ResourceVersion
    {
        public virtual Keyword Keyword { get; set; }
    }
}
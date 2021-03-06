using System;
using System.Collections.Generic;
using ITJakub.DataEntities.Database.Entities.Enums;

namespace ITJakub.DataEntities.Database.Entities
{
    public class BookVersion : IEquatable<BookVersion>
    {

        public virtual long Id { get; set; }

        public virtual Book Book { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual BookType DefaultBookType { get; set; }

        public virtual string VersionId { get; set; }

        public virtual string Title { get; set; }

        public virtual string SubTitle { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual string Description { get; set; }

        public virtual string PublishPlace { get; set; }

        public virtual string PublishDate { get; set; }

        public virtual string Copyright { get; set; }

        public virtual AvailabilityStatus AvailabilityStatus { get; set; }

        public virtual string BiblText { get; set; }

        public virtual string Acronym { get; set; }

        public virtual string RelicAbbreviation { get; set; }

        public virtual string SourceAbbreviation { get; set; }

        public virtual IList<Category> Categories { get; set; }

        public virtual IList<ManuscriptDescription> ManuscriptDescriptions { get; set; }
        
        public virtual IList<Keyword> Keywords { get; set; }

        public virtual IList<Author> Authors { get; set; }

        public virtual IList<Responsible> Responsibles { get; set; }

        public virtual IList<Transformation> Transformations { get; set; }

        public virtual IList<BookPage> BookPages { get; set; }

        public virtual IList<BookContentItem> BookContentItems { get; set; }

        public virtual IList<BookHeadword> BookHeadwords { get; set; } 

        public virtual IList<Track> Tracks { get; set; }

        public virtual IList<FullBookRecording> FullBookRecordings { get; set; }

        public virtual IList<BookAccessory> Accessories { get; set; }

        public virtual IList<LiteraryOriginal> LiteraryOriginals { get; set; }

        public virtual IList<LiteraryKind> LiteraryKinds { get; set; }

        public virtual IList<LiteraryGenre> LiteraryGenres { get; set; }

        public virtual IList<FavoriteBookVersion> FavoriteItems { get; set; }


        public virtual bool Equals(BookVersion other)
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
            return Equals((BookVersion) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
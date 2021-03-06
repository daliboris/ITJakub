using System;
using System.Collections.Generic;

namespace ITJakub.DataEntities.Database.Entities
{
    public class Book : IEquatable<Book>
    {
        public virtual long Id { get; set; }

        public virtual BookVersion LastVersion { get; set; }

        public virtual string Guid { get; set; }        

        public virtual IList<BookVersion> BookVersions { get; set; }

        public virtual IList<Permission> Permissions { get; set; }

        public virtual IList<LiteraryGenre> LiteraryGenres { get; set; }

        public virtual IList<FavoriteBook> FavoriteItems { get; set; }

        public virtual bool Equals(Book other)
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
            return Equals((Book) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
using System;
using System.Collections.Generic;

namespace ITJakub.DataEntities.Database.Entities
{
    public class Author : IEquatable<Author>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<BookVersion> BookVersions { get; set; }

        public virtual bool Equals(Author other)
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
            return Equals((Author) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
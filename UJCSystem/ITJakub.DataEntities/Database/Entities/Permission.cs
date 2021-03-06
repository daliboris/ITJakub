using System;
using System.Collections.Generic;
using ITJakub.DataEntities.Database.Entities.Enums;

namespace ITJakub.DataEntities.Database.Entities
{
    public class Permission : IEquatable<Permission>
    {
        public virtual long Id { get; set; }

        public virtual Group Group { get; set; }

        public virtual Book Book { get; set; }


        public virtual bool Equals(Permission other)
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
            return Equals((Permission) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
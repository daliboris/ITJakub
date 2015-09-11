using System;
using System.Collections.Generic;
using ITJakub.DataEntities.Database.Entities.Enums;

namespace ITJakub.DataEntities.Database.Entities
{
    public class Group : IEquatable<Group>
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual IList<User> Users { get; set; }

        public virtual bool Equals(Group other)
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
            return Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
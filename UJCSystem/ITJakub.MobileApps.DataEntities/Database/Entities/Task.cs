using System;
using System.Collections.Generic;

namespace ITJakub.MobileApps.DataEntities.Database.Entities
{
    public class Task : IEquatable<Task>
    {
        public virtual long Id { get; set; }
        public virtual Application Application { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual string Name { get; set; }
        public virtual string Guid { get; set; }


        public virtual List<Group> Groups { get; set; }

        public virtual bool Equals(Task other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Task) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
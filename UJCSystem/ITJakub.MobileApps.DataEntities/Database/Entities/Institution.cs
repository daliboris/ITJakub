﻿using System;
using System.Collections.Generic;

namespace ITJakub.MobileApps.DataEntities.Database.Entities
{
    public class Institution : IEquatable<Institution>
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual List<Group> Groups { get; set; }
        public virtual List<User> Users { get; set; } //TODO resolve N:M relationship

        public virtual bool Equals(Institution other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Institution) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
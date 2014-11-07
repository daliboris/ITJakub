using System;
using System.Text;
using System.Collections.Generic;


namespace ITJakub.DataEntities.Database.Entities {
    
    public class Category : IEquatable<Category> {

        public virtual int Id { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Book> Books { get; set; }
        public virtual IList<Category> Categories { get; set; }

        public virtual bool Equals(Category other)
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
            return Equals((Category) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

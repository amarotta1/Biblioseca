using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string title { get; set; }
        public virtual string description { get; set; }
        public virtual string isbn { get; set; }
        public virtual Author author { get; set; }
        public virtual Category category { get; set; }
        public virtual int stock { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual void MarkAsDeleted()
        {
            this.Deleted = true;
        }

        public virtual void DecreaseStock()
        {
            this.stock -= 1;
        }

        public virtual void IncreaseStock()
        {
            this.stock += 1;
        }

    }
}

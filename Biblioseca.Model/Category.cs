using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string name { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual void MarkAsDeleted()
        {
            this.Deleted = true;
        }
    }
}

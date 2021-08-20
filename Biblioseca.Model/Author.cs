using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Author
    {
        public virtual int Id { get; set; } //virtual para que NHibernate pueda usarlas. Solo NHibernate lo necesita para sobreescribir
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual void MarkAsDeleted()
        {
            this.Deleted = true;
        }
    }
}

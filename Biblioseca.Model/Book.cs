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
        public virtual int isbn { get; set; }
        public virtual Author author { get; set; }
        public virtual Category category { get; set; }

    }
}

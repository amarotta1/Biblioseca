using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    class Loan
    {
        public virtual int Id { get; set; }
        public virtual Partner partner { get; set; }
        public virtual Book book { get; set; }
        public virtual DateTime initialDate { get; set; } //dia retirado
        public virtual DateTime finishDate { get; set; } //dia supuesto a devolver
        public virtual DateTime returnedDate { get; set; } //dia devuelto
        public virtual bool returned { get; set; } //devuelto o no
    }
}

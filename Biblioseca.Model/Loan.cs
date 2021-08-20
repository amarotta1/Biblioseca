using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Loan
    {
        public virtual int Id { get; set; }
        public virtual Partner partner { get; set; }
        public virtual Book book { get; set; }
        public virtual DateTime initialDate { get; set; } //dia retirado
        public virtual DateTime finishDate { get; set; } //dia supuesto a devolver
        public virtual DateTime? returnedDate { get; set; } //dia devuelto si es Null significa que no se devolvio
        // El ? es para que acepte nulos
        public virtual bool Deleted { get; set; }

        public virtual void MarkAsDeleted()
        {
            this.Deleted = true;
        }
        public virtual void Returned()
        {
            returnedDate = DateTime.Now;
        }
    }

    
}

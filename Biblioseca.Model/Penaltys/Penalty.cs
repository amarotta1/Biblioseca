using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Penalty
    {
        public virtual int Id { get; set; }
        
        public virtual Loan loan { get; set; }
        //asociado a un prestamo para saber sobre que libro se penalizo, que a su vez esta asociado a un usuario
        public virtual DateTime initialDate { get; set; }
        public virtual DateTime finishDate { get; set; }//con las dos fechas se puede calcular la cantidad de días de cuarentena

        //Pensar y tener en cuenta que pasa si debe los dos libros es decir dos loan distintos
        //deberia sumarle días a la cuarentena por ambos. Por ejemplo;
        //1 dia de atraso --> 2 dias de cuarentena => 2 dias en libroA y 3 dias en libroB = 10 dias de cuarentena

        // public virtual Partner partner { get; set; }

        //Ademas esta asociado a n facturas, las cuales se deberan crear y asociar 1 cada 10 dias de cuarentena 

    }
}

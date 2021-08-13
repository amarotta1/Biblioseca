using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Service
{
    public static class CheckService
    {
        public static void Exists(object modelObject)
        {
            if (modelObject == null)
            {
                string message = "El objeto no existe";
                throw new Exception(message);
            }
        }

        public static void BusinessLogic(Boolean condition, string message) 
        {
            if (condition)
            {
                throw new Exception(message);
            }
            
        }
    }
}

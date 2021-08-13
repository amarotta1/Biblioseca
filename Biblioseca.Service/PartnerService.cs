using System;
using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess;
using Biblioseca.Model;

namespace Biblioseca.Service
{
    public class PartnerService
    {
        private readonly PartnerDao partnerDao;

        public PartnerService(PartnerDao partnerDao)
        {
            this.partnerDao = partnerDao;
        }

        public IEnumerable<Partner> GetAll()
        {
            return partnerDao.GetAll();
        }

        public Partner GetOneById(int partnerId)
        {
            CheckService.BusinessLogic(partnerId <= 0, "El id del socio debe ser mayor a cero");
            Partner partner = partnerDao.Get(partnerId);
            CheckService.Exists(partner);

            return partner;

        }

        public IEnumerable<Partner> GetByName(string partnerName)
        {
           
            return partnerDao.GetByName(partnerName); //pueden haber varios con el mismo nombre
        }

        public IEnumerable<Partner> GetByLastName(string partnerLastName)
        {
           
            return partnerDao.GetByLastName(partnerLastName);
        }

        public Partner GetByUserName(string partnerUserName)
        {
            return partnerDao.GetByUserName(partnerUserName);
        }

        public Partner Create(string name, string lastName, string userName)
        {

            Partner p = partnerDao.GetByUserName(userName);
            CheckService.BusinessLogic(p != null, "El Nombre de usuario ya ha sido utilizado");

            Partner partner = new Partner();
            partner.FirstName = name;
            partner.LastName = lastName;
            partner.UserName = userName;
            partnerDao.Save(partner);
            return partner;
        }

        public bool Delete(int partnerId)
        {
            CheckService.BusinessLogic(partnerId <= 0, "El id del socio debe ser mayor a cero");
            Partner partner = partnerDao.Get(partnerId);
            CheckService.Exists(partner);
            partnerDao.Delete(partner);
            return true;

        }
    }
}

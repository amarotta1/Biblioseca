using Biblioseca.DataAccess;
using Biblioseca.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Service
{
    public class AuthorService
    {
        private readonly AuthorDao authorDao;

        public AuthorService(AuthorDao authorDao)
        {
            this.authorDao = authorDao;
        }

        public IEnumerable<Author> GetAll()
        {
            return authorDao.GetAll();
        }

        public Author GetOnebyId(int authorId)
        {
            CheckService.BusinessLogic(authorId<= 0, "El id del autor debe ser mayor a cero");
            Author author = authorDao.Get(authorId);
            CheckService.Exists(author);

            return author;

        }

        public Author Create(string name,string lastName)
        {
            

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", name);
            parameters.Add("LastName", lastName);
            Author author1 = authorDao.GetUniqueByHqlQuery("FROM Author WHERE FirstName = :FirstName and LastName = :LastName", parameters);


            CheckService.BusinessLogic(author1 != null, "El autor ya existe");

            Author author = new Author();
            author.FirstName = name;
            author.LastName = lastName;
            authorDao.Save(author);
            return author;
        }
        
        public bool Delete(int authorId)
        {
            CheckService.BusinessLogic(authorId<=0, "El id del autor debe ser mayor a cero");
            Author author =  authorDao.Get(authorId);
            CheckService.Exists(author);
            authorDao.Delete(author);
            return true;

        }

    }
}

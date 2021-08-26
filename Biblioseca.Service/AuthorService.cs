using Biblioseca.DataAccess;
using Biblioseca.Model;
using System.Collections.Generic;
using System.Linq;


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

        public Author Create(string name,string lastName)        {
            

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("FirstName", name);
            parameters.Add("LastName", lastName);
            IEnumerable<Author> author1 = authorDao.AuthorAllreadyExist(name,lastName);

            CheckService.BusinessLogic(author1.Any(), "El autor ya existe");

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
            author.MarkAsDeleted();
            authorDao.Save(author);
            return true;

        }

        public void Update(int authorId, string firstName, string lastName)
        {
            CheckService.BusinessLogic(authorId <= 0, "El id del autor debe ser mayor a cero");
            Author author = authorDao.Get(authorId);
            CheckService.Exists(author);

            author.FirstName = firstName;
            author.LastName = lastName;

            authorDao.Save(author);

        }


    }
}

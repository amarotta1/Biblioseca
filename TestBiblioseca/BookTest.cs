using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;

namespace TestBiblioseca
{
    [TestClass]
    public class BookTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();

        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }
        
        [TestMethod]
        public void CreatedBook()
        {
            Book b1 = new Book();
          
            b1.author = session.Get<Author>(1);
            b1.category = session.Get<Category>(1);
            b1.description = "Es un libro de prueba";
            b1.title = "Prueba";
            b1.isbn = 97884;

            this.session.Save(b1);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(b1.Id > 0);

            Book created = this.session.Get<Book>(b1.Id);

            Assert.AreEqual(b1.Id, created.Id);

        }
    }
}

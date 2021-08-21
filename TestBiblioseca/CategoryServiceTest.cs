using Biblioseca.DataAccess;
using Biblioseca.Model;
using Biblioseca.Service;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TestBiblioseca
{
    [TestClass]
    public class CategoryServiceTest
    {
        private CategoryService categoryService;
        private Mock<CategoryDao> categoryDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.categoryDao = new Mock<CategoryDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void GetAll()
        {

            this.categoryDao.Setup(dao => dao.GetAll()).Returns(GetCategorys());

            this.categoryService = new CategoryService(this.categoryDao.Object);
            IEnumerable<Category> category = this.categoryService.GetAll();

            Assert.IsTrue(category.Any());

        }
        [TestMethod]
        public void GetOneByID()
        {
            const int categoryId = 1;
            this.categoryDao.Setup(dao => dao.Get(categoryId)).Returns(GetCategory());
            this.categoryService = new CategoryService(this.categoryDao.Object);
            Category category = categoryService.GetOneCategory(categoryId);
            Assert.IsNotNull(category);
            Assert.IsInstanceOfType(category, typeof(Category));

        }

        [TestMethod]
        public void Create()
        {
            const string categoryName = "Accion";            
            this.categoryService = new CategoryService(this.categoryDao.Object);
            Category category = categoryService.CreateCategory(categoryName);
            Assert.IsNotNull(category);
            Assert.IsInstanceOfType(category, typeof(Category));
            Assert.AreEqual(category.name, categoryName);

        }


        [TestMethod]
        public void Deleted()
        {

            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            CategoryDao categoryDao = new CategoryDao(sessionFactory);
            this.categoryService = new CategoryService(categoryDao);
            Category a = categoryDao.Get(1);
            categoryService.Delete(1);

            Assert.IsTrue(a.Deleted);

            a.Deleted = false; //para volverlo a poner bien
            categoryDao.Save(a);

        }

        public static Category GetCategory()
        {
            Category cat = new Category
            {
                name = "Terror"
            };

            return cat;
        }

        public static IEnumerable<Category> GetCategorys()
        {
            List<Category> categorys = new List<Category>
            {
                new Category
                {
                    name = "Terror"
                    
                },
                new Category
                {
                    name = "Accion"

                },
                new Category
                {
                    name = "Suspenso"

                },

            };
            return categorys;

        }

    }

    
}

using Biblioseca.DataAccess;
using Biblioseca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Service
{
    public class CategoryService
    {
        private readonly CategoryDao categoryDao;

        public CategoryService(CategoryDao categoryDao)
        {
            this.categoryDao = categoryDao;
        }

        public IEnumerable<Category> GetAll()
        {
            return categoryDao.GetAll();
        }

        public Category GetOneCategory(int categoryId)
        {
            CheckService.BusinessLogic(categoryId <= 0, "El id de la Categoria debe ser mayor a cero");
            return categoryDao.Get(categoryId);

        }

        public Category CreateCategory(string name)
        {
            Category category = new Category();
            category.name = name;
            categoryDao.Save(category);
            return category;

        }

        public bool DeleteCategory(int categoryId)
        {
            CheckService.BusinessLogic(categoryId <= 0, "El id de la Categoria debe ser mayor a cero");
            Category category = categoryDao.Get(categoryId);
            category.MarkAsDeleted();
            categoryDao.Save(category);
            return true;
        }


    }
}

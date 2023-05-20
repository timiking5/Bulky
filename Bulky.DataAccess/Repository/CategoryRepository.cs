using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        public CategoryRepository(ApplicationDBContext dbContext) : base(dbContext) {}

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }
    }
}

using Book_Sphere.Data;
using Book_Sphere.Interfaces;
using Book_Sphere.Models;

namespace Book_Sphere.Repository
{
    public class CategoryRepo : ICatecory
    {
        public ApplicationDbContext context;
        public CategoryRepo(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Add(Category category)
        {
            context.Add(category);
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {

            return context.Categories.Find(id);
        }

        public Category GetCategoryByName(string name)
        {
            return context.Categories.FirstOrDefault(c => c.Name == name);
        }

        public void Remove(Category category)
        {
            context.Remove(category);
        }

        public void Safe()
        {
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            context.Update(category);
        }
    }
}

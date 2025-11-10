using Book_Sphere.Models;

namespace Book_Sphere.Interfaces
{
    public interface ICatecory
    {
        public void Add(Category category);
        public void Remove(Category category);
        public Category GetCategoryById(int id);
        public Category GetCategoryByName(string name);
        public void Update(Category category);
        public List<Category> GetCategories();
        public void Safe();

    }
}

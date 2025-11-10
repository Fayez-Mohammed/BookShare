using Book_Sphere.Models;

namespace Book_Sphere.Interfaces
{
    public interface IBook
    {
        public void Add(Book book);
        public void Remove(Book book);
        public Book GetBookById(int id);
        public Book GetBookByName(string name);
        public void Update(Book book);
        public List<Book> GetBooks();
        public List<Book> GetBooksByCategoryId(int categoryId);
        public List<Book> SearchByTitle(string SearchValue);
        public List<Book> FavorList();

        public void Safe();
    }
}

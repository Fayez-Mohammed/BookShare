using Book_Sphere.Data;
using Book_Sphere.Interfaces;
using Book_Sphere.Models;

namespace Book_Sphere.Repository
{
    public class BookRepo : IBook
    {
        public ApplicationDbContext context;
        public BookRepo(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Add(Book book)
        {
            context.Add(book);
        }

        public List<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public Book GetBookById(int id)
        {

            return context.Books.Find(id);
        }

        public Book GetBookByName(string name)
        {
            return context.Books.FirstOrDefault(c => c.Title == name);
        }

        public void Remove(Book book)
        {
            context.Remove(book);
        }

        public void Safe()
        {
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            context.Update(book);
        }
        public List<Book> GetBooksByCategoryId(int categoryId)
        {
            return context.Books.Where(b => b.CategoryId == categoryId).ToList();
        }

        public List<Book> SearchByTitle(string SearchValue)
        {
            return context.Books.Where(b => b.Title.Contains(SearchValue)).ToList();
        }

        public List<Book> FavorList()
        {
            return context.Books.Where(b => b.Favor == true).ToList();
        }
    }
}

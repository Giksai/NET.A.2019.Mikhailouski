using System;
using BookModel;
using BookServiceProj;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            
            //bookService.AddBookToShop(new Book("978-3-16-123451-0", "Ivanov1", "one", "Minsk", 2000, 1000, 100));
            //bookService.AddBookToShop(new Book("978-3-16-123452-1", "Petrov", "two", "Gomel", 2001, 2000, 200).BookFormat());
            //bookService.AddBookToShop(new Book("978-3-16-123453-2", "Glebov", "three", "Brest", 2002, 3000, 300));
            //bookService.AddBookToShop(new Book("978-3-16-123454-3", "Arkhipov", "four", "Vitebsk", 2003, 4000, 400));

            bookService.PrintAllBooks();  
        }
    }
}

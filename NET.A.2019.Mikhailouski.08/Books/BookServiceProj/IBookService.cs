using BookModel;
using ParameterForSearching;
using System.Collections.Generic;

namespace BookServiceProj
{
    public interface IBookService
    {
        /// <summary>
        /// Adds book to list
        /// </summary>
        void AddBookToShop(Book book);

        /// <summary>
        /// Removes book from list
        /// </summary>
        void RemoveBookFromShop(Book book);

        /// <summary>
        /// Finds book in list
        /// </summary>
        Book FindBook(IFinder parameter);

        /// <summary>
        /// Sorts by some teg
        /// </summary>
        void Sort(IComparer<Book> comparator);

        /// <summary>
        /// Saves information about books
        /// </summary>
        void Save();

        /// <summary>
        /// Returns list of books
        /// </summary>
        IEnumerable<Book> GetAllBooks();

        /// <summary>
        /// Prints all books in memory to the screen
        /// </summary>
        void PrintAllBooks();
    }
}

using System.Collections;
using System.Collections.Generic;
using Books;
using ParameterForSearching;

namespace BookService
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
    }
}
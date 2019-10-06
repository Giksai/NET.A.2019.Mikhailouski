using BookModel;
using System.Collections.Generic;

namespace BookStorageProj
{
    public interface IBookStorage
    {
        /// <summary>
        /// Reads book list from file
        /// </summary>
        IEnumerable<Book> GetBookList();

        /// <summary>
        /// Writes books into list
        /// </summary>
        void SaveBooks(IEnumerable<Book> books);

        /// <summary>
        /// Adds book to the binary file
        /// </summary>
        /// <param name="book"></param>
        void AppendBookToFile(Book book);
    }
}

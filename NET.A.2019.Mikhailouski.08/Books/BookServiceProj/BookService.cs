using System;
using System.Collections.Generic;
using System.Linq;
using BookModel;
using BookStorageProj;
using ParameterForSearching;

namespace BookServiceProj
{
    public class BookService : IBookService
    {
        private readonly IBookStorage bookStorage;
        private List<Book> books = new List<Book>();


        public BookService()
        {
            bookStorage = BookStorage.GetInstance();
            books = bookStorage.GetBookList().ToList();
        }

        public void PrintAllBooks()
        {
            foreach(var book in books)
            {
                Console.WriteLine(book);
            }
        }

        /// <inheritdoc />
        public void AddBookToShop(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException();
            }
            books.Add(book);
            bookStorage.AppendBookToFile(book);
        }

        /// <inheritdoc />
        public void RemoveBookFromShop(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException();
            }
            books.Remove(book);
            Save();
        }

        /// <inheritdoc />
        public Book FindBook(IFinder parameter)
        {
            if (ReferenceEquals(parameter, null))
            {
                throw new ArgumentNullException();
            }
            return parameter.FindBookByTeg();
        }

        /// <inheritdoc />
        public void Sort(IComparer<Book> comparator)
        {
            var booksArray = books.ToArray();

            if (ReferenceEquals(comparator, null))
            {
                Array.Sort(booksArray);
            }
            else
            {
                Array.Sort(booksArray, comparator);
            }
            books.Clear();
            books.AddRange(booksArray);

            Save();
        }

        /// <inheritdoc />
        public void Save()
        {
            bookStorage.SaveBooks(books);
        }

        /// <inheritdoc />
        public IEnumerable<Book> GetAllBooks()
        {
            return bookStorage.GetBookList();
        }

    }
}

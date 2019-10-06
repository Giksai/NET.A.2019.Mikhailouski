using System;

namespace BookModel
{
    public static class Formatter
    {
        public static Book BookFormat(this Book book)
        {
            if (ReferenceEquals(book, null)) throw new ArgumentNullException();
            book.Name = book.Name.ToUpper();
            book.Author = book.Author.ToUpper();
            return book;
        }
    }
}

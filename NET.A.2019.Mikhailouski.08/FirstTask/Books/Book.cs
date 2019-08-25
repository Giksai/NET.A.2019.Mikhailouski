using System;

using System.Globalization;

namespace Books
{
    /// <summary>
    ///     Book modal
    /// </summary>
    
    public class Book : IEquatable<Book>, IComparable, IComparable<Book>, IFormattable
    {
        /// <summary>
        /// Books properties
        /// </summary>
        public string Isbn { get; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string PublishingHouse { get; }
        public int Year { get; }
        public double Price { get; }
        public int Pages { get; }

        public Book(string isbn, string author, string name, string publish, int year, double price, int pages)
        {
            Isbn = isbn;
            Author = author;
            Name = name;
            PublishingHouse = publish;
            Year = year;
            Price = price;
            Pages = pages;
        }

        /// <inheritdoc />
        /// IEquatable override method 
        /// <summary>
        /// </summary>
        public bool Equals(Book book)
        {
            if(ReferenceEquals(book, null))
            {
                return false;
            }
            if (ReferenceEquals(this, book))
            {
                return true;
            }
            return book.Isbn == Isbn;
        }

        /// <summary>
        /// override object class method Equals
        /// </summary>
        public override bool Equals(object bookEq)
        {
            var book = (Book) bookEq;
            if (book == null)
                return false;

            return Isbn == book.Isbn && Author == book.Author && Name == book.Name
                   && PublishingHouse == book.PublishingHouse && Year == book.Year && Pages == book.Pages;
        }

        /// <summary>
        /// override object class method GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }

        /// <summary>
        /// override object class method ToString
        /// </summary>
        public override string ToString()
        {
            return ToString("7", null);
        }

        /// <summary>
        /// Output options
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "5";
            

            switch (format)
            {
                case "1": return "Book: " + Name +" Author: " + Author;
                case "2": return "Book: " + Name + " Author: " + Author + " ISBN: " + Isbn;
                case "3": return "Book: " + Name + " Author: " + Year + " y. " + Author + " ISBN: " + Isbn;
                case "4": return "Book: " + Name + " Author: " + Year + " y. " +Pages +" p. "+ Author + " ISBN: " + Isbn;
                case "5": return "Book: " + Name + " Author: " + Year + " y. " + Pages + " p. " + Author + " ISBN: " + Isbn + " Publishing House : " + PublishingHouse;
                case "6": return "Book: " + Name + " Author: " + Year + " y. " + Pages + " p. " + Author + " ISBN: " + Isbn + " Publishing House : " + PublishingHouse + Price + " y.e ";
                    default: throw new FormatException(String.Format("The {0} format string is not supported.", format));
            }
        }

        /// <summary>
        /// IComparable override method 
        /// </summary>
        public int CompareTo(object bookObj)
        {
            if (ReferenceEquals(bookObj, null)) return 1;
            var book = (Book) bookObj;
            return CompareTo(book);
        }

        public int CompareTo(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                return 1;
            }
            // ReSharper disable once StringCompareIsCultureSpecific.1
            return string.Compare(Name, book.Name);
        }

    }
}
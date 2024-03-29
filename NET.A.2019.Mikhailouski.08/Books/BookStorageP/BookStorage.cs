﻿using System;
using BookModel;
using System.Collections.Generic;
using System.IO;

namespace BookStorageProj
{
    public class BookStorage : IBookStorage
    {
        /// <summary>
        /// Field path is the way of file
        /// </summary>
        private readonly string path = "file.bin";

        private BookStorage() { }

        private static BookStorage instance;

        public static BookStorage GetInstance()
        {
            if (instance == null)
                instance = new BookStorage();
            return instance;
        }

        /// <inheritdoc />
        public IEnumerable<Book> GetBookList()
        {
            List<Book> books = new List<Book>();
            using (var br = new BinaryReader(File.Open(path, FileMode.OpenOrCreate,
                FileAccess.Read, FileShare.Read)))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    var book = Reader(br);
                    books.Add(book);
                }
            }

            return books;
        }

        /// <inheritdoc />
        public void AppendBookToFile(Book book)
        {
            using (var bw = new BinaryWriter(File.Open(path, FileMode.Append,
                FileAccess.Write, FileShare.None)))
            {
                Writer(bw, book);
            }
        }

        /// <inheritdoc />
        public void SaveBooks(IEnumerable<Book> books)
        {

            using (var bw = new BinaryWriter(File.Open(path, FileMode.Create,
                FileAccess.Write, FileShare.None)))
            {
                foreach (var book in books)
                {
                    Writer(bw, book);
                }
            }
        }


        private static void Writer(BinaryWriter binary, Book book)
        {
            binary.Write(book.Isbn);
            binary.Write(book.Author);
            binary.Write(book.Name);
            binary.Write(book.PublishingHouse);
            binary.Write(book.Year);
            binary.Write(book.Price);
            binary.Write(book.Pages);
        }

        private static Book Reader(BinaryReader binary)
        {
            var isbn = binary.ReadString();
            var author = binary.ReadString();
            var name = binary.ReadString();
            var publish = binary.ReadString();
            var year = binary.ReadInt32();
            var price = binary.ReadDouble();
            var pages = binary.ReadInt32();

            return new Book(isbn, author, name, publish, year, price, pages);
        }
    }
}

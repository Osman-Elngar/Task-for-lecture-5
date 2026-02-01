using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    // Book Class
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true;
        }
    }

    // Library Class
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Book added: {book.Title}");
        }

        private Book SearchBook(string keyword)
        {
            return books.FirstOrDefault(b =>
                b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public void BorrowBook(string keyword)
        {
            Book book = SearchBook(keyword);

            if (book == null)
            {
                Console.WriteLine($"Book not found: {keyword}");
            }
            else if (!book.IsAvailable)
            {
                Console.WriteLine($"Book already borrowed: {book.Title}");
            }
            else
            {
                book.IsAvailable = false;
                Console.WriteLine($"Book borrowed successfully: {book.Title}");
            }
        }

        public void ReturnBook(string keyword)
        {
            Book book = SearchBook(keyword);

            if (book == null)
            {
                Console.WriteLine($"Book not found: {keyword}");
            }
            else if (book.IsAvailable)
            {
                Console.WriteLine($"Book was not borrowed: {book.Title}");
            }
            else
            {
                book.IsAvailable = true;
                Console.WriteLine($"Book returned successfully: {book.Title}");
            }
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            library.AddBook(new Book("1984", "George Orwell", "9780451524935"));

            Console.WriteLine("\nSearching and borrowing books...");
            library.BorrowBook("Gatsby");
            library.BorrowBook("1984");
            library.BorrowBook("Harry Potter");

            Console.WriteLine("\nReturning books...");
            library.ReturnBook("Gatsby");
            library.ReturnBook("Harry Potter");

            Console.ReadLine();
        }
    }
}

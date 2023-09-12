namespace MyLibrary.Logic
{
    public class Library
    {
        private List<Book> Books;
        public Library(List<Book> books)
        {
            Books = books ?? new List<Book>();
        }
        private int GetNewBookId()
        {
            return Books?.Any() == true
               ? Books.Max(x => x.Id) + 1
               : 1;
        }

        public void AddBook(BookEditing book)
        {
            var id = GetNewBookId();
            Books.Add(new Book(id, book));
        }
        public List<Book> GetAllBooks()
        {
            return Books;
        }
        public void RemoveBook(int id)
        {
            var currentBook = Books?.FirstOrDefault(x => x.Id == id);
            if (currentBook != null)
                Books?.Remove(currentBook);
        }

        public void UpdateBook(int id, BookEditing book)
        {
            var currentBook = Books?.FirstOrDefault(x => x.Id == id);
            if (currentBook != null)
                currentBook = new Book(id, book);
        }
        public List<Book> SearchByAuthor(string phrase)
        {
            return Books.FindAll(b => b.Author.Equals(phrase, StringComparison.OrdinalIgnoreCase));//виконує пошук та повертає список усіх книг у бібліотеці, у яких автор збігається із заданим автором, при цьому регістр символів не враховується.
        }
        public List<Book> SearchByTitle(string phrase)
        {
            return Books.FindAll(b => b.Title.Contains(phrase, StringComparison.OrdinalIgnoreCase));
        }
        public bool EditBook(int Id, BookEditing updatedBook)
        {
            if (updatedBook == null)
                return false;
            Book bookToUpdate = Books?.FirstOrDefault(b => b.Id == Id);
            if (bookToUpdate == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }

            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.Year = updatedBook.Year;
            bookToUpdate.Genre = updatedBook.Genre;
            bookToUpdate.Language = updatedBook.Language;
            return true;
        }
    }
}

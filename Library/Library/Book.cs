using MyLibrary.Enums;

namespace MyLibrary
{
    public class Book : BookEditing
    {
        public int Id { get; set; }
        public Book(int id, string title, string author, int? year, string isbn, Genre? genre, Language? language) 
            : base(title, author, year, isbn, genre, language)
        {
            Id = id;
        }

        public Book(int id, BookEditing bookEditing)
        :base(bookEditing.Title, bookEditing.Author, bookEditing.Year, bookEditing.ISBN, bookEditing.Genre, bookEditing.Language)
        {
            Id = id;
        }
    }  
}

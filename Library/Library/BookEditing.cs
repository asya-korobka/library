using MyLibrary.Enums;

namespace MyLibrary
{
    public class BookEditing
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public string? ISBN { get; set; }
        public Genre? Genre { get; set; }//тип може бути= null; 
        public Language? Language { get; set; }

        public BookEditing(string title, string author, int? year, string? isbn, Genre? genre, Language? language)
        {
            Title = title;
            Author = author;
            Year = year;
            ISBN = isbn;
            Genre = genre;
            Language = language;
        }

    }
}

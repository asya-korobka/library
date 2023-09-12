using MyLibrary.Enums;
using MyLibrary.Logic;
using System.Linq;

namespace MyLibrary.UserInterface
{
    /// <summary>
    /// Візуальне представлення дій з книгами
    /// </summary>
    public class LibraryUI
    {
        private Library _library; 
        public LibraryUI(Library library)
        {
            _library = library;
        }

        /// <summary>
        /// Вивід меню операціцй з книгами
        /// </summary>
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Оберiть вiдповiдну дiю (0-6).");
                Console.WriteLine("1. Вивести книги");
                Console.WriteLine("2. Додати книгу");
                Console.WriteLine("3. Редагувати книгу");
                Console.WriteLine("4. Видалити книгу");
                Console.WriteLine("5. Знайти книгу за назвою");
                Console.WriteLine("6. Знайти книгу за автором");
                Console.WriteLine("---------------");
                Console.WriteLine("0. Вийти");
                var key = Console.ReadKey(false);
                switch (key.KeyChar)
                {
                    case '0': return;
                    case '1':
                        {
                            ReadBooks();
                            break;
                        }
                    case '2':
                        {
                            AddBook();
                            break;
                        }
                    case '3':
                        {
                            EditBook();
                            break;
                        }
                    case '4':
                        {
                            DeleteBook();
                            break;
                        }
                    case '5':
                        {
                            SearchByTitle();
                            break;
                        }
                    case '6':
                        {
                            SearchByAuthor();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Не вiрно обрано пункт меню...");
                            break;
                        }

                }
                Console.WriteLine("Нажмiть будь-яку клавiшу для продовження...");
                Console.ReadKey(false);
            }
        }

        
        public void SearchByTitle()
        {
            Console.WriteLine("\nВведiть повну або частину назви книги: ");
            var searchPhrase = Console.ReadLine();
            if (string.IsNullOrEmpty(searchPhrase))
                return;
            var books = _library.SearchByTitle(searchPhrase);
            ShowBooks(books);
        }

        public void SearchByAuthor()
        {
            Console.WriteLine("\nВведiть повне або частину прiзвища автора книги: ");
            var searchPhrase = Console.ReadLine();
            if (string.IsNullOrEmpty(searchPhrase))
                return;
            var books = _library.SearchByAuthor(searchPhrase);
            ShowBooks(books);
        }

        public void AddBook()
        {
            Console.WriteLine("\nВведiть даннi нової книги");
            Console.Write("Назва: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Назва не може бути пустою!");
                return;
            }
            Console.Write("Автор: ");
            var author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Не вiрний автор (поле не може бути пустим)!");
                return;
            }
            Console.WriteLine("Оберiть жанр: ");
            foreach(var genreItem in Enum.GetValues(typeof(Genre)))
                Console.WriteLine($"{(int)genreItem}. {(Genre)genreItem}");

            //Genre? genre = null;            
            //var key = Console.ReadKey().KeyChar;            
            //if (Enum.TryParse<Genre>(key.ToString(), out var genreValue))
            //{
            //    genre = genreValue;
            //}

            Genre? genre = (Enum.TryParse<Genre>(Console.ReadKey().KeyChar.ToString(), out var genreValue))
                ? genreValue
                : null;

            Console.Write("\nРiк: ");

            int? year = 
                int.TryParse(Console.ReadLine(), out var yearValue) && (yearValue <= DateTime.Now.Year)
                ? yearValue
                : null;

            Console.Write($"\nОберiть мову:\n");

            foreach (var languageItem in Enum.GetValues(typeof(Language)))
                Console.WriteLine($"{(int)languageItem}. {(Language)languageItem}");

            Language? language =
                Enum.TryParse<Language>(Console.ReadKey().Key.ToString(), out var languageValue)
                ? languageValue
                : null;

            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();

            _library.AddBook(new BookEditing(title, author, year, isbn, genre, language));
            //Console.WriteLine($"ISBN: {book.ISBN}");

        }
        public void EditBook()
        {
            Console.Write("\nВведiть id книги для оновлення даних про неї: ");
            if (!int.TryParse(Console.ReadLine(), out var idToEdit))
            {
                Console.WriteLine("Id вказано не вiрно!");
                return;
            }
            Book bookToEdit = _library.GetAllBooks().FirstOrDefault(b => b.Id == idToEdit);
            if (bookToEdit != null)
            {
                Console.WriteLine("Введiть нову назву книгу: ");
                string newTitle = Console.ReadLine();
                Console.Write("Введiть нового автора: ");
                string newAuthor = Console.ReadLine();
                Console.Write("Введiть рiк: ");
                int? newYear = int.TryParse(Console.ReadLine(), out var newYearValue) && (newYearValue <= DateTime.Now.Year)
                ? newYearValue
                : null;
                {
                    Console.WriteLine("Введiть жанр: ");
                    foreach (var newGenreItem in Enum.GetValues(typeof(Genre)))
                        Console.WriteLine($"{(int)newGenreItem}. {(Genre)newGenreItem}");

                    Genre? newGenre = (Enum.TryParse<Genre>(Console.ReadKey().KeyChar.ToString(), out var newGenreValue))
                    ? newGenreValue
                    : null;
                    Console.WriteLine("Введiть мову: ");
                    foreach (var newLanguageItem in Enum.GetValues(typeof(Language)))
                    Console.WriteLine($"{(int)newLanguageItem}. {(Language)newLanguageItem}");

                    Language? newLanguage = Enum.TryParse<Language>(Console.ReadLine(), out var newLanguageValue)
                    ? newLanguageValue
                    : null;


                    if (_library.EditBook(idToEdit, new BookEditing(newTitle, newAuthor, newYear, "", newGenre, newLanguage)))
                    {
                        Console.WriteLine("Book edited successfully!");
                    }
                }
            }
        }

        public void DeleteBook()
        {
            Console.Write("\nВведiть Id книги для видалення: ");
            if (int.TryParse(Console.ReadLine(), out var id) && _library.GetAllBooks()?
                .FirstOrDefault(x => x.Id == id) != null)
            {
                _library.RemoveBook(id);
                Console.WriteLine("Успiшно видалено");
            }
            else
            {
                Console.WriteLine("Не вiрний Id книги.");
            }
        }

        public void ReadBooks()
        {
            ShowBooks(_library.GetAllBooks());
        }
        private void ShowBooks(List<Book> books)
        {
            Console.WriteLine();
            if (books?.Any() != true)
            {
                Console.WriteLine("Жодної книги не знайдено!");
                return;
            }
            
            foreach (var book in books)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Iдентифiкатор: {book.Id}");
                Console.WriteLine($"Назва: {book.Title}");
                Console.WriteLine($"Автор: {book.Author}");
                Console.WriteLine($"Жанр: {book.Genre}");
                Console.WriteLine($"Рiк: {book.Year}");
                Console.WriteLine($"ISBN: {book.ISBN}");

            }
            Console.WriteLine("-----------------------------");
        }
      
    }
}

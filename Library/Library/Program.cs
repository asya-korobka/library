using MyLibrary;
using MyLibrary.Logic;
using MyLibrary.UserInterface;

var library = new Library(new List<Book>());
var libUI = new LibraryUI(library);
libUI.ShowMenu();

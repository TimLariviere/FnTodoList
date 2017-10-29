namespace FnTodoList

open Xamarin.Forms

type App() =
    inherit Application(MainPage = NavigationPage(TodoItemPage()))

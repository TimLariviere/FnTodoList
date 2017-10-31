namespace FnTodoList

open Xamarin.Forms

type App() =
    inherit Application(MainPage = NavigationPage(TodoItemPage(Some (System.Guid.Parse("7708e05b-cd6d-4918-982d-3adae3233cd2")))))


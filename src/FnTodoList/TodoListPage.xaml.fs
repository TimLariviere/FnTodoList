namespace FnTodoList

open Xamarin.Forms
open Xamarin.Forms.Xaml

type Todo =
    {
        Text: string
    }

type TodoListPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<TodoListPage>)
    do this.BindingContext <- [
        { Text = "Hello World" };
        { Text = "Hello World1" };
        { Text = "Hello World2" }
    ]

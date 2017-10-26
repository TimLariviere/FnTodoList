namespace FnTodoList

open Xamarin.Forms
open Xamarin.Forms.Xaml

type Todo =
    {
        Text: string
    }

type HomePage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HomePage>)
    do this.BindingContext <- [
        { Text = "Hello World" };
        { Text = "Hello World1" };
        { Text = "Hello World2" }
    ]

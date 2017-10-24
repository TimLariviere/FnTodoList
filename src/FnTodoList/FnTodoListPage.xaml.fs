namespace FnTodoList

open Xamarin.Forms
open Xamarin.Forms.Xaml

type FnTodoListPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<FnTodoListPage>)

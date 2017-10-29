namespace FnTodoList

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml

type TodoItemPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<TodoItemPage>)

    let titleEntry = this.FindByName("TitleEntry") :> Entry
    let contentEditor = this.FindByName("ContentEditor") :> Editor
    let saveToolbarItem = this.FindByName("SaveToolbarItem") :> ToolbarItem

    let onTitleTextChanged = EventHandler<_>(fun _ args ->
        this.Title <- titleEntry.Text
    )
    let onSaveClicked = EventHandler(fun _ args ->
        titleEntry.Text <- "Clicked"
    )

    override this.OnAppearing() =
        titleEntry.Text <- "Title"
        contentEditor.Text <- "Content"
        titleEntry.TextChanged.AddHandler onTitleTextChanged
        saveToolbarItem.Clicked.AddHandler onSaveClicked

    override this.OnDisappearing() =
        titleEntry.TextChanged.RemoveHandler onTitleTextChanged
        saveToolbarItem.Clicked.RemoveHandler onSaveClicked


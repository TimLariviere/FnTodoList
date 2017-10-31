namespace FnTodoList

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FnTodoList.Core

type TodoListPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<TodoListPage>)

    // Controls
    let todoListView = this.FindByName "TodoListView" :> ListView
    let newNoteToolbarItem = this.FindByName "NewNoteToolbarItem" :> ToolbarItem

    // Methods
    let navigateToDetail id =
        this.Navigation.PushAsync(TodoItemPage(id)) |> ignore

    // Event handlers
    let onTodoListViewItemTapped = EventHandler<ItemTappedEventArgs>(fun _ args ->
        let note = args.Item :?> Note
        navigateToDetail (Some note.Id)
    )
    let onNewNoteToolbarItemClicked = EventHandler(fun _ args ->
        navigateToDetail None
    )

    // Lifecycle
    override this.OnAppearing() =
        this.BindingContext <- DataContext.Notes

        todoListView.ItemTapped.AddHandler onTodoListViewItemTapped
        newNoteToolbarItem.Clicked.AddHandler onNewNoteToolbarItemClicked

    override this.OnDisappearing() =
        todoListView.ItemTapped.RemoveHandler onTodoListViewItemTapped
        newNoteToolbarItem.Clicked.RemoveHandler onNewNoteToolbarItemClicked

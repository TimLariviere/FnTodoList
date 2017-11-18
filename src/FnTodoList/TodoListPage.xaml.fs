namespace FnTodoList

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FnTodoList.Core
open FnTodoList.Core.DomainTypes

type TodoListPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<TodoListPage>)
    
    static let mutable _notInitialized = true

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
        async {
            if _notInitialized then
                do! (UseCases.initializeAppData()
                    |> AsyncRop.evaluateAsync
                      (fun x -> this.DisplayAlert("Loaded", "All data loaded", "OK") |> Async.AwaitTask |> ignore)
                      (fun err -> this.DisplayAlert("Error", "Error while initializing the application", "OK") |> Async.AwaitTask |> ignore)
                    |> AsyncRop.ignoreAsync)
                    
                _notInitialized <- false
            else
                ()
              
            this.BindingContext <- DataContext.Notes
            todoListView.ItemTapped.AddHandler onTodoListViewItemTapped
            newNoteToolbarItem.Clicked.AddHandler onNewNoteToolbarItemClicked
        } |> ignore
    
    override this.OnDisappearing() =
        todoListView.ItemTapped.RemoveHandler onTodoListViewItemTapped
        newNoteToolbarItem.Clicked.RemoveHandler onNewNoteToolbarItemClicked

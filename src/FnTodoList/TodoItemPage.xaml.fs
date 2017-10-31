namespace FnTodoList

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FnTodoList.Core
open FnTodoList.Core.UseCases

type TodoItemPage(id: Guid option) as this =
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

    let createData() =
        this.Title <- "New note"
        titleEntry.Text <- String.Empty
        contentEditor.Text <- String.Empty

    let loadData note =
        this.Title <- "Edit note"
        titleEntry.Text <- note.Title
        contentEditor.Text <- note.Content


    override this.OnAppearing() =
        titleEntry.TextChanged.AddHandler onTitleTextChanged
        saveToolbarItem.Clicked.AddHandler onSaveClicked

        match id with
        | Some id -> 
            match tryFindNoteById DataContext.Notes id with
            | Some note -> loadData note
            | None -> createData()
        | None -> createData()

    override this.OnDisappearing() =
        titleEntry.TextChanged.RemoveHandler onTitleTextChanged
        saveToolbarItem.Clicked.RemoveHandler onSaveClicked


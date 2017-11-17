namespace FnTodoList

open System
open Xamarin.Forms
open Xamarin.Forms.Xaml
open FnTodoList.Core
open FnTodoList.Core.DomainTypes

type TodoItemPage(id: int option) as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<TodoItemPage>)

    // Fields
    let mutable _id = -1

    // Controls
    let titleEntry = this.FindByName("TitleEntry") :> Entry
    let contentEditor = this.FindByName("ContentEditor") :> Editor
    let saveToolbarItem = this.FindByName("SaveToolbarItem") :> ToolbarItem

    // Methods
    let initForNewNote() =
        this.Title <- "New note"
        titleEntry.Text <- String.Empty
        contentEditor.Text <- String.Empty

    let load note =
        this.Title <- "Edit note"
        titleEntry.Text <- note.Title
        contentEditor.Text <- note.Content

    let init() =
        _id <- match id with
               | Some id -> id
               | None -> -1

        if Option.isSome id then
            match tryFindNoteById DataContext.Notes (Option.get id) with
            | Some note -> load note
            | None -> initForNewNote()
        else
            initForNewNote()

    let updatePageTitle newTitle =
        this.Title <- newTitle

    let getModifiedNote() =
        {
            Id = _id
            Title = titleEntry.Text
            Content = contentEditor.Text
        }

    let save() =
        DataContext.Notes <- getModifiedNote() |> upsert DataContext.Notes
        Ok true


    // Event handlers
    let onTitleTextChanged = EventHandler<_>(fun _ args ->
        updatePageTitle titleEntry.Text
    )
    let onSaveClicked = EventHandler(fun _ args ->
        match save() with
        | Ok x ->
            this.DisplayAlert("Saved", "", "OK") |> ignore
            this.Navigation.PopAsync() |> ignore
        | Error err ->
            this.DisplayAlert("Not saved", "An error occured while saving", "OK") |> ignore
    )

    // Lifecycle
    override this.OnAppearing() =
        init()

        titleEntry.TextChanged.AddHandler onTitleTextChanged
        saveToolbarItem.Clicked.AddHandler onSaveClicked

    override this.OnDisappearing() =
        titleEntry.TextChanged.RemoveHandler onTitleTextChanged
        saveToolbarItem.Clicked.RemoveHandler onSaveClicked


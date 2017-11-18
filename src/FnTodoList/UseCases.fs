namespace FnTodoList
        
module SqlConn = 
    open FnTodoList.Platform
    open SQLite
    open Xamarin.Forms
    
    let getSqliteConnection() =
        let dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePathAsync("FnTodoList.db")
        new SQLiteConnection(dbPath)

module UseCases =
    open FnTodoList.Core
    
    let initializeAppData() =
        let setDataContext notes = DataContext.Notes <- (notes |> List.ofSeq)
        
        NoteService.getAllNotes (fun() -> SqlConn.getSqliteConnection() |> NoteRepository.getAll)
        |> AsyncRop.teeAsync setDataContext
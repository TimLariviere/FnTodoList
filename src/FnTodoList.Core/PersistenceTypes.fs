namespace FnTodoList.Core

module PersistenceTypes =
    open System
    open SQLite
    
    type PersistenceError = | PersistenceException of Exception

    type NoteEntity() =
        [<PrimaryKey>]
        member val Id = 0 with get,set
        member val Title = String.Empty with get,set
        member val Content = String.Empty with get,set
        
    type getAllNotes = unit -> Async<Result<seq<NoteEntity>, PersistenceError>>
    type createNote = NoteEntity -> Async<Result<NoteEntity, PersistenceError>>
    type updateNote = NoteEntity -> Async<Result<NoteEntity, PersistenceError>>
    type deleteNote = NoteEntity -> Async<Result<NoteEntity, PersistenceError>>
    
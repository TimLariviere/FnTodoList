namespace FnTodoList.Core

module NoteRepository =
    open FnTodoList.Core.PersistenceTypes
    open FnTodoList.Core.PersistenceHelpers
    open SQLite
    open System.Linq

    let getAll (connection: SQLiteConnection) = async {
        try
            return Ok (connection.Table<NoteEntity>().ToList() |> seq)
        with
        | exn -> return Error (PersistenceException exn)
    }
        
    let create (connection: SQLiteConnection) (note: NoteEntity) = async {
        try
            let newId = connection.Insert(note)
            return Ok (cloneNoteWithNewId note newId)
        with
        | exn -> return Error (PersistenceException exn)
    }
        
    let update (connection: SQLiteConnection) (note: NoteEntity) = async {
        try
            let id = connection.Update(note)
            return Ok note
        with
        | exn -> return Error (PersistenceException exn)
    }
    
    let delete (connection: SQLiteConnection) (note: NoteEntity) = async {
        try
            let id = connection.Delete(note)
            return Ok note
        with
        | exn -> return Error (PersistenceException exn)
    }
namespace FnTodoList.Core

module NoteService =
    open SQLite
    open System
    open FnTodoList.Core.PersistenceHelpers
    open FnTodoList.Core.PersistenceTypes

    type NoteServiceError = | PersistenceError of PersistenceError

    let getAllNotes (getAllEntitiesFunc: getAllNotes) = async {
        let! result = getAllEntitiesFunc()
        return
            match result with
            | Ok x -> Ok (x |> Seq.map persistenceToDomain)
            | Error err -> Error (PersistenceError err)
    }
    
    let private doAsync (func: NoteEntity -> Async<Result<NoteEntity, PersistenceError>>) note = async {
         let! result = func (domainToPersistence note)
         return
             match result with
             | Ok x -> Ok (persistenceToDomain x)
             | Error err -> Error (PersistenceError err)
     }
    
    let createNote (createEntityFunc: createNote) note = doAsync createEntityFunc note
    let updateNote (updateEntityFunc: updateNote) note = doAsync updateEntityFunc note
    let deleteNote (deleteEntityFunc: deleteNote) note = doAsync deleteEntityFunc note
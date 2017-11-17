namespace FnTodoList.Core

open SQLite

module UseCases =
    open FnTodoList.Core.Entities

    let tryFindNoteById list noteId =
        list |> List.tryFind (fun n -> n.Id = noteId)

    let upsert list note =
        let index = list |> List.tryFindIndex (fun n -> n.Id = note.Id)
        match index with
        | Some idx -> 
            list |> List.mapi (fun i n -> if i = idx then note else n)
        | None ->
            note :: list
    
    let convertNoteEntitiesToBusiness (entity: NoteEntity) =
        {
            Id = entity.Id
            Title = entity.Title
            Content = entity.Content
        }
            
    let loadAllTodosAsync (connection: SQLiteAsyncConnection) = async {
        let! entities = connection.Table<NoteEntity>().ToListAsync() |> Async.AwaitTask
        return entities |> Seq.map convertNoteEntitiesToBusiness |> Ok
    }
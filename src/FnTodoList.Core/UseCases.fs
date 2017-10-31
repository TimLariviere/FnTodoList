namespace FnTodoList.Core

module UseCases =
    let tryFindNoteById list noteId =
        list |> List.tryFind (fun n -> n.Id = noteId)

    let upsert list note =
        let index = list |> List.tryFindIndex (fun n -> n.Id = note.Id)
        match index with
        | Some idx -> 
            list |> List.mapi (fun i n -> if i = idx then note else n)
        | None ->
            note :: list
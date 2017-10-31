namespace FnTodoList.Core

module UseCases =
    let tryFindNoteById list noteId =
        list |> List.tryFind (fun n -> n.Id = noteId)
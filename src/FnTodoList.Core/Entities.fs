namespace FnTodoList.Core

module Entities =
    open System

    type NoteEntity() =
        member val Id = Guid.Empty with get,set
        member val Title = String.Empty with get,set
        member val Content = String.Empty with get,set
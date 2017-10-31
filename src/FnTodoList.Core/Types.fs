namespace FnTodoList.Core

[<AutoOpen>]
module Types =
    open System

    type Note = {
        Id: Guid
        Title: string
        Content: string
    }
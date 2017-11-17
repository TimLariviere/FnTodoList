namespace FnTodoList.Core

module DomainTypes =
    open System

    type Note = {
        Id: int
        Title: string
        Content: string
    }
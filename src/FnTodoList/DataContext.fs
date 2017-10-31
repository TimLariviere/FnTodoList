namespace FnTodoList

open FnTodoList.Core

type DataContext() =
    static let mutable notes : Note list = [
        { Id = System.Guid.Parse("7708e05b-cd6d-4918-982d-3adae3233cd2"); Title = "This is a Title"; Content = "This is a Content" }
    ]

    static member Notes
        with get() = notes
        and set(value) = notes <- value
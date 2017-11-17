namespace FnTodoList

open FnTodoList.Core.DomainTypes

type DataContext() =
    static let mutable notes : Note list = [
        { Id = 0; Title = "This is a Title"; Content = "This is a Content" }
    ]

    static member Notes
        with get() = notes
        and set(value) = notes <- value
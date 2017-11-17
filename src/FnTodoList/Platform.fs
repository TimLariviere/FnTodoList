namespace FnTodoList

module Platform =
    open System

    type IFileHelper =
        abstract member GetLocalFilePathAsync : string -> string
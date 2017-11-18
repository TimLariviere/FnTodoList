namespace FnTodoList.Core

module AsyncRop =
    let evaluate successFunc errorFunc =
        function
        | Ok x -> Ok (successFunc x)
        | Error error -> Error (errorFunc error)
        
    let evaluateAsync successFunc errorFunc result = async {
        let! r = result
        return evaluate successFunc errorFunc r
    }
    
    let bindAsync f result = async {
        let! r = result
        return Result.bind f r
    }
    
    let teeAsync f result =
        let f' = fun x -> f x |> ignore; Ok x
        bindAsync f' result
        
    let runAsync successFunc errorFunc result = async {
        let! r = result
        match r with
        | Ok x -> successFunc x
        | Error err -> errorFunc err
    }
    
    let ignoreAsync x = async {
        let! r = x
        ()
    }
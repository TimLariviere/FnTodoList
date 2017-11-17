namespace FnTodoList.Core

module PersistenceHelpers =
    open FnTodoList.Core.DomainTypes
    open FnTodoList.Core.PersistenceTypes

    let cloneNoteWithNewId (note: NoteEntity) id =
        let entity = new NoteEntity()
        entity.Id <- id
        entity.Title <- note.Title
        entity.Content <- note.Content
        entity
            
    let domainToPersistence (note: Note) =
        let entity = new NoteEntity()
        entity.Id <- note.Id
        entity.Title <- note.Title
        entity.Content <- note.Content
        entity
        
    let persistenceToDomain (note: NoteEntity) =
        {
            Id = note.Id
            Title = note.Title
            Content = note.Content
        }
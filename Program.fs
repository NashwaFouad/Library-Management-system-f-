// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
open System

[<EntryPoint>]
let main argv =
    printfn "Hello World!"
    0 // return an integer exit code
    // Function to borrow a book and automatically record the borrow date
let borrowBook title =
    match library.TryFind(title) with
    | Some book when not book.IsBorrowed -> 
        let updatedBook = { book with IsBorrowed = true; BorrowDate = Some DateTime.Now }
        library <- library.Add(title, updatedBook)
        saveLibraryToFile ()
        "Book borrowed successfully"
    | Some book -> "This book is already borrowed"
    | None -> "Book not found"
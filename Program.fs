//////$$$$$$$$$$$$$$$$$$$$$$$ phase(2)

// Function to add a new book
let addBook title author genre =
    let book = { Title = title; Author = author; Genre = genre; IsBorrowed = false; BorrowDate = None }
    library <- library.Add(title, book)
    saveLibraryToFile ()
    "Book added successfully"

// Function to search for a book by title
let searchBook title =
    match library.TryFind(title) with
    | Some book ->
        let status = if book.IsBorrowed then
                         match book.BorrowDate with
                         | Some date -> sprintf "Borrowed on: %s" (date.ToString("yyyy-MM-dd HH:mm"))
                         | None -> "Borrowed"
                     else "Available"
        sprintf "Title: %s\nAuthor: %s\nGenre: %s\nStatus: %s" book.Title book.Author book.Genre status
    | None -> "Book not found

// Function to add a new book
let addBook title author genre =
    let book = { Title = title; Author = author; Genre = genre; IsBorrowed = false; BorrowDate = None }
    library <- library.Add(title, book)
    saveLibraryToFile ()
    "Book added successfully"

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

// Function to display all books
let displayBooks () =
    let booksList = 
        library 
        |> Map.toList 
        |> List.map (fun (title, book) ->
            let status = if book.IsBorrowed then "Borrowed" else "Available"
            sprintf "\r\nTitle: %s%sAuthor: %s%sGenre: %s%sStatus: %s \r\n" 
                    book.Title 
                    System.Environment.NewLine 
                    book.Author 
                    System.Environment.NewLine 
                    book.Genre 
                    System.Environment.NewLine 
                    status
        )
    String.Join("\n \n", booksList)  // This adds two lines between different books

// Event Handlers for Buttons Logic
addButton.Click.Add(fun _ ->
    let title = titleTextBox.Text
    let author = authorTextBox.Text
    let genre = genreTextBox.Text
    if title = "" || author = "" || genre = "" then
        resultLabel.Text <- "Please fill all fields!"
    else
        let result = addBook title author genre
        resultLabel.Text <- result
)

searchButton.Click.Add(fun _ ->
    let title = titleTextBox.Text
    if title = "" then
        resultLabel.Text <- "Please fill at least title field!"
    else
        let result = searchBook title
        resultLabel.Text <- result
)

borrowButton.Click.Add(fun _ ->
    let title = titleTextBox.Text
    if title = "" then
        resultLabel.Text <- "Please fill at least title field!"
    else
        let message = borrowBook title
        resultLabel.Text <- message
)

returnButton.Click.Add(fun _ ->
    let title = titleTextBox.Text
    if title = "" then
        resultLabel.Text <- "Please fill at least title field!"
    else
        let message = returnBook title
        resultLabel.Text <- message
)

deleteButton.Click.Add(fun _ ->
    let title = titleTextBox.Text
    if title = "" then
        resultLabel.Text <- "Please fill at least title field!"
    else
        let message = deleteBook title
        resultLabel.Text <- message
)

displayButton.Click.Add(fun _ ->
    let books = displayBooks ()
    booksDisplay.Text <- books
)

// Add controls to the Form
form.Controls.Add(titleLabel)
form.Controls.Add(titleTextBox)
form.Controls.Add(authorLabel)
form.Controls.Add(authorTextBox)
form.Controls.Add(genreLabel)
form.Controls.Add(genreTextBox)
form.Controls.Add(resultLabel)
form.Controls.Add(booksDisplay)
form.Controls.Add(addButton)
form.Controls.Add(searchButton)
form.Controls.Add(borrowButton)
form.Controls.Add(returnButton)
form.Controls.Add(deleteButton)
form.Controls.Add(displayButton)

// Run the application
Application.Run(form)

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
    | None -> "Book not found"

    // Function to return a book  my task
let returnBook title =
    match library.TryFind(title) with
    | Some book when book.IsBorrowed ->
        let updatedBook = { book with IsBorrowed = false; BorrowDate = None }
        library <- library.Add(title, updatedBook)
        saveLibraryToFile ()
        "Book returned successfully"
    | Some book -> "This book was not borrowed"
    | None -> "Book not found"
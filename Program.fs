// Function to add a new book
let addBook title author genre =
    let book = { Title = title; Author = author; Genre = genre; IsBorrowed = false; BorrowDate = None }
    library <- library.Add(title, book)
    saveLibraryToFile ()
    "Book added successfully"

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
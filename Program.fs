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
      // Create the Form and controls
let form = new Form(Text = "Library Management System",
                     Width = 1000,
                     Height = 700,
                     BackgroundImage = System.Drawing.Image.FromFile(@"C:\pl3 project\book-8833738_640.jpg"))
form.StartPosition <- FormStartPosition.CenterScreen

// Define controls
let titleLabel = new Label(Text = "Title:", Location = new Drawing.Point(10, 10), AutoSize = true)
let titleTextBox = new TextBox(Location = new Drawing.Point(100, 10), Width = 200)

let authorLabel = new Label(Text = "Author:", Location = new Drawing.Point(10, 40), AutoSize = true)
let authorTextBox = new TextBox(Location = new Drawing.Point(100, 40), Width = 200)

let genreLabel = new Label(Text = "Genre:", Location = new Drawing.Point(10, 70), AutoSize = true)
let genreTextBox = new TextBox(Location = new Drawing.Point(100, 70), Width = 200)

let resultLabel = new Label(Text = "", Location = new Drawing.Point(10, 200), AutoSize = true)
let booksDisplay = new TextBox(Text = "Box List",Location = new Drawing.Point(40, 350), Width = 900, Height = 300, Multiline = true, ReadOnly = true,BackColor=System.Drawing.Color.Beige)

let addButton = new Button(Text = "Add Book", Location = new Drawing.Point(10, 150), Width = 100,Height=40,BackColor=System.Drawing.Color.LightGreen)
let searchButton = new Button(Text = "Search Book", Location = new Drawing.Point(120, 150), Width = 100,Height=40, BackColor = System.Drawing.Color.BlanchedAlmond)
let borrowButton = new Button(Text = "Borrow Book", Location = new Drawing.Point(230, 150), Width = 100,Height=40,BackColor=System.Drawing.Color.BlanchedAlmond)
let returnButton = new Button(Text = "Return Book", Location = new Drawing.Point(340, 150), Width = 100,Height=40,BackColor=System.Drawing.Color.BlanchedAlmond)
let deleteButton = new Button(Text = "Delete Book", Location = new Drawing.Point(450, 150), Width = 100,Height=40,BackColor=System.Drawing.Color.Red)
let displayButton = new Button(Text = "Display Books", Location = new Drawing.Point(560, 150), Width = 100,Height=40,BackColor=System.Drawing.Color.CadetBlue)

// Load library data from file when starting the program
loadLibraryFromFile ()
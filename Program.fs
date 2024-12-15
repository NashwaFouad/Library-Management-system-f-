open System
open System.Windows.Forms
open Newtonsoft.Json
open System.IO

// Define the Book Record type
type Book = {
    Title: string
    Author: string
    Genre: string
    IsBorrowed: bool
    BorrowDate: DateTime option
}

// Map to store books by title
let mutable library = Map.empty<string, Book>

// Path to store the JSON file
let filePath = "library.json"

// Function to save books to a file
let saveLibraryToFile () =
    let json = JsonConvert.SerializeObject(library)
    File.WriteAllText(filePath, json)

// Function to load books from a file
let loadLibraryFromFile () =
    if File.Exists(filePath) then
        let json = File.ReadAllText(filePath)
        library <- JsonConvert.DeserializeObject<Map<string, Book>>(json)
    else
        library <- Map.empty


        
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

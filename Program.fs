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
    String.Join("\n \n", book)  // This add two lines between different books
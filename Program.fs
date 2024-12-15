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

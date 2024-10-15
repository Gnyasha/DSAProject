# Phonebook Application Documentation

## Project Group Members
1. 224099310 - Goodson Nyasha Mutongi 
2. 224074946 - Laimi N Shandjuuka
3. 224082027 - JB Theron
4. 224017888 - Saara M N Ndakeva
5. 223087742 - Teofilus NK Mumbashu

## Project Overview
This Phonebook Application is a simple yet efficient phonebook management system built using **Blazor** and **MudBlazor**. It allows users to **insert**, **search**, **delete**, **update**, and **sort contacts**, with an emphasis on efficient data structures such as **Hash Tables** and **Tries** for storage and retrieval. We are not connecting to a database as of now

### Key Features
- **Contact Management**: Create, update, delete, and search contacts.
- **Modular Design**: The app is structured into multiple modules, each focusing on a specific functionality.
- **Custom Validation**: Phone numbers are validated to ensure they start with '0' and contain exactly 5 digits.
- **Dialog Management**: Using `MudBlazor` dialogs for creating and editing contacts, providing a smooth UI/UX experience.
- **Detailed Pseudocode**: Outlines the underlying logic, helping users and developers understand the data structure operations.

## Table of Contents
- [Modules](#modules)
- [Project Structure](#project-structure)
- [Functions](#functions)
- [Pseudocode](#pseudocode)
- [Setup Instructions](#setup-instructions)
- [Dependencies](#dependencies)
- [Contributing](#contributing)
- [License](#license)

---

## Modules
The application is divided into four primary modules:

### 1. Data Storage Module
Handles contact storage using a **Hash Table** and **Trie**, providing efficient insertion, searching, and deletion:
- **Hash Table Module**: Supports operations such as insertion, searching, deletion, and updating based on contact names.
- **Trie Module**: Enables efficient prefix-based searching, allowing contacts to be accessed and managed quickly by name.

### 2. Operations Module
Contains core functions that operate on the data and manage contacts:
- **InsertContact**: Adds a new contact to both the Hash Table and Trie.
- **SearchContact**: Searches for a contact by name.
- **DeleteContact**: Deletes contacts from both data structures.
- **UpdateContact**: Updates an existing contact’s name and/or phone number.
- **SortContacts**: Sorts contacts alphabetically using the Trie or by sorting Hash Table output (optional).

### 3. Utility Module
This module provides helper functions to support the operations:
- **ValidatePhoneNumber**: Ensures phone numbers start with '0' and contain exactly 5 digits.
- **DisplayContacts**: Displays contacts alphabetically by iterating through the Trie or Hash Table.

### 4. Backup & Restore Module
- **Manuel Backup**:Enables users to back up their contacts manually to a local or cloud location.
- **Auto Backup**: Automatically backs up contacts at predefined intervals.
- **Restore**:Restores contacts from a previously saved backup file or cloud service.

## Project Structure
The project is organized into multiple folders, ensuring modularity and separation of concerns as discussed above:
```plaintext
📂 PhonebookBlazorApp
├── 📁 DataStorageModule
│   ├── HashTable.cs         # Implements hash table-based contact storage
│   └── Trie.cs              # Implements trie-based contact storage for prefix-based searching
├── 📁 OperationsModule
│   └── ContactOperations.cs # Core functions like insertion, deletion, update, and search
├── 📁 UtilityModule
│   └── PhoneNumberValidator.cs # Custom validation logic for phone numbers
├── 📁 Components
│   ├── ContactList.razor        # Lists contacts and provides edit/delete options
│   ├── ContactEditDialog.razor  # Dialog for editing contacts
│   └── ContactCreateDialog.razor# Dialog for creating new contacts
├── 📁 wwwroot
│   └── css
│       └── app.css            # Additional styling
├── Program.cs                 # Entry point for the Blazor application
├── App.razor                  # Main app layout component
└── README.md                  # Project documentation
```

---

## Functions

### Data Storage Module
- **InitializeHashTable()**: Initializes the hash table for contact storage.
- **InitializeTrie()**: Initializes the Trie for storing contacts based on name prefixes.

### Operations Module
- **InsertContact(name, phone)**: Adds a contact to both the Hash Table and Trie.
- **SearchContact(name)**: Searches for a contact by name.
- **DeleteContact(name)**: Deletes a contact by name from both the Hash Table and Trie.
- **UpdateContact(currentName, currentPhone, newName, newPhone)**: Updates an existing contact’s details.
- **SortContacts()**: Sorts contacts alphabetically using Trie’s inherent order or by sorting the Hash Table output.

### Utility Module
- **ValidatePhoneNumber(phone)**: Checks if a phone number starts with '0' and is exactly 5 digits long.
- **DisplayContacts()**: Lists all contacts in alphabetical order by performing a depth-first traversal of the Trie or sorting the Hash Table output.

### Backup & Restore Module
-**Backup**: A user enables automatic cloud backups, which sync their contact list every day.

-**Restore**: If the user loses their phone, they can restore all contacts on a new device by logging into their cloud account and selecting the most recent backup.

---

## Pseudocode

### 1. Insert Contact
```plaintext
FUNCTION InsertContact(root, name, phone)
    node ← root
    FOR each character in name
        IF node.children[character] IS NULL
            node.children[character] ← new TRIE_NODE
        END IF
        node ← node.children[character]
    END FOR
    node.isEndOfWord ← TRUE
    node.phone ← phone
END FUNCTION
```

### 2. Search Contact
```plaintext
FUNCTION SearchContact(root, name)
    node ← root
    FOR each character in name
        IF node.children[character] IS NULL
            RETURN "Contact Not Found"
        END IF
        node ← node.children[character]
    END FOR
    IF node.isEndOfWord IS TRUE
        RETURN node.phone
    ELSE
        RETURN "Contact Not Found"
    END IF
END FUNCTION
```

### 3. Display All Contacts
```plaintext
FUNCTION DisplayContacts(node, prefix)
    IF node.isEndOfWord IS TRUE
        PRINT prefix + " : " + node.phone
    END IF
    FOR each character in node.children
        IF node.children[character] IS NOT NULL
            DisplayContacts(node.children[character], prefix + character)
        END IF
    END FOR
END FUNCTION
```

### 4. Delete Contact
```plaintext
FUNCTION DeleteContact(node, name, depth)
    IF node IS NULL
        RETURN FALSE
    END IF
    IF depth = LENGTH(name)
        IF node.isEndOfWord IS TRUE
            node.isEndOfWord ← FALSE
            RETURN node.hasNoChildren()
        END IF
        RETURN FALSE
    END IF
    index ← name[depth]
    IF DeleteContact(node.children[index], name, depth + 1) IS TRUE
        node.children[index] ← NULL
        RETURN (node.hasNoChildren() AND NOT node.isEndOfWord)
    END IF
    RETURN FALSE
END FUNCTION
```

### 5. Update Contact
```plaintext
FUNCTION UpdateContact(root, name, newPhone)
    node ← root
    FOR each character in name
        IF node.children[character] IS NULL
            RETURN "Contact Not Found"
        END IF
        node ← node.children[character]
    END FOR
    IF node.isEndOfWord IS TRUE
        node.phone ← newPhone
        RETURN "Contact updated successfully"
    ELSE
        RETURN "Contact Not Found"
    END IF
END FUNCTION
```

---

### 6. Flowchart
![image](https://github.com/user-attachments/assets/e3e82dba-3bfb-4d3a-8d58-2339d6da0ee0)



## Setup Instructions

### Prerequisites
- **.NET SDK**: Download the latest version from [here](https://dotnet.microsoft.com/download).
- **Visual Studio IDE**: For development tasks [here](https://visualstudio.microsoft.com/vs/community/).

### Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/phonebook-blazor-app.git
   cd phonebook-blazor-app
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

5. Open a browser and navigate to `http://localhost:5000` to access the application.

---

## Dependencies
- **MudBlazor**: For UI components like dialogs, buttons, and text fields.
- **.NET 8.0 or later**: Required for the Blazor framework to run.

---



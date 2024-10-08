﻿using DSAProject.DataStorageModule;
using DSAProject.Models;

namespace DSAProject.OperationsModule
{
    public class ContactOperations
    {
        private readonly HashTable hashTable;
        private readonly Trie trie;

        public ContactOperations()
        {
            hashTable = new HashTable();
            trie = new Trie();
            Initialize();
        }

        // Initializes both data structures
        private void Initialize()
        {
            hashTable.InitializeHashTable();
            trie.InitializeTrie();
        }

        // Inserts a new contact into both the HashTable and Trie
        public void InsertContact(string name, string phone)
        {
            var contact = new Contact(name, phone);
            hashTable.InsertContact(contact);
            trie.InsertContact(contact);
            Console.WriteLine($"Contact '{name}' inserted successfully.");
        }

        // Searches for a contact by name in both HashTable and Trie
        public Contact SearchContact(string name)
        {
            // You can use either structure; here we use Trie for prefix-based searches
            Contact contact = trie.SearchContact(name);
            if (contact != null)
            {
                Console.WriteLine($"Contact found: {contact.Name} : {contact.Phone}");
                return contact;
            }
            Console.WriteLine($"Contact '{name}' not found.");
            return null;
        }

        public IEnumerable<Contact> SearchContacts(string namePrefix)
        {
            var contacts = trie.SearchContactsByPrefix(namePrefix);
            if (contacts.Any())
            {
                Console.WriteLine($"Contacts found for prefix '{namePrefix}':");
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"- {contact.Name} : {contact.Phone}");
                }
                return contacts;
            }
            Console.WriteLine($"No contacts found with prefix '{namePrefix}'.");
            return Enumerable.Empty<Contact>();
        }


        // Deletes a contact by name from both HashTable and Trie
        public bool DeleteContact(string name)
        {
            bool hashTableDeleted = hashTable.DeleteContact(name);
            bool trieDeleted = trie.DeleteContact(name);

            if (hashTableDeleted && trieDeleted)
            {
                Console.WriteLine($"Contact '{name}' deleted successfully.");
                return true;
            }
            Console.WriteLine($"Contact '{name}' not found or could not be deleted.");
            return false;
        }

        // Updates a contact's phone number in both HashTable and Trie
        public bool UpdateContact(string currentName, string currentPhone, string newName, string newPhone)
        {
            // Check for the contact in the hash table first
            bool hashTableUpdated = hashTable.UpdateContact(currentName, currentPhone, newName, newPhone);
            bool trieUpdated = trie.UpdateContact(currentName, currentPhone, newName, newPhone);

            // Confirm successful update only if both structures could update the contact
            if (hashTableUpdated && trieUpdated)
            {
                Console.WriteLine($"Contact '{currentName}' updated to '{newName}' successfully.");
                return true;
            }

            Console.WriteLine($"Contact '{currentName}' with phone '{currentPhone}' not found or could not be updated.");
            return false;
        }


        // Sorts contacts alphabetically and returns a list of sorted contacts
        public List<Contact> SortContacts()
        {
            // Since the Trie can store contacts in alphabetical order, we use it here
            var sortedContacts = trie.GetContactsByPrefix("");
            sortedContacts.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
            Console.WriteLine("Contacts sorted alphabetically.");
            return sortedContacts;
        }

        // Displays all contacts sorted alphabetically
        public void DisplayContacts()
        {
            List<Contact> contacts = SortContacts();
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts to display.");
                return;
            }

            Console.WriteLine("Contacts:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Name} : {contact.Phone}");
            }
        }
    }
}

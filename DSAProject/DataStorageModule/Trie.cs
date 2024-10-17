using DSAProject.Models;

namespace DSAProject.DataStorageModule
{
    /// <summary>
    /// This class implements a trie data structure to store contacts
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// This is a node in the trie
        /// </summary>
        public class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; set; }
            public bool IsEndOfWord { get; set; }
            public Contact Contact { get; set; }

            public TrieNode()
            {
                Children = new Dictionary<char, TrieNode>();
                IsEndOfWord = false;
                Contact = null;
            }
        }

        /// <summary>
        /// The root node of the trie
        /// </summary>
        public TrieNode root;

        /// <summary>
        /// Constructor to initialize the trie
        /// </summary>
        public Trie()
        {
            root = new TrieNode();
        }

        /// <summary>
        /// Initializes the trie
        /// </summary>
        public void InitializeTrie()
        {
            root = new TrieNode();
        }

        /// <summary>
        ///  Inserts a new contact into the trie
        /// </summary>
        /// <param name="contact"></param>
        public void InsertContact(Contact contact)
        {
            TrieNode node = root;
            foreach (char ch in contact.Name)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new TrieNode();
                }
                node = node.Children[ch];
            }
            node.IsEndOfWord = true;
            node.Contact = contact;
        }

        /// <summary>
        /// Searches for a contact by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Contact SearchContact(string name)
        {
            TrieNode node = root;
            foreach (char ch in name)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return null; // Contact not found
                }
                node = node.Children[ch];
            }
            return node.IsEndOfWord ? node.Contact : null;
        }

        /// <summary>
        /// Searches for contacts by prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<Contact> SearchContactsByPrefix(string prefix)
        {
            var results = new List<Contact>();
            if (string.IsNullOrEmpty(prefix))
                return results; // No prefix, return empty list

            var node = root;

            foreach (var ch in prefix)
            {
                if (node.Children.TryGetValue(ch, out var nextNode))
                {
                    node = nextNode;
                }
                else
                {
                    return results; // No match, return empty list
                }
            }

            // Perform a depth-first traversal from this node to find all contacts
            FindAllContacts(node, prefix, results);
            return results;
        }

        /// <summary>
        /// Finds all contacts starting from a given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="prefix"></param>
        /// <param name="results"></param>
        private void FindAllContacts(TrieNode node, string prefix, List<Contact> results)
        {
            if (node.IsEndOfWord)
            {
                results.Add(node.Contact);
            }

            foreach (var child in node.Children)
            {
                FindAllContacts(child.Value, prefix + child.Key, results);
            }
        }

        /// <summary>
        /// Deletes a contact by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DeleteContact(string name)
        {
            return DeleteContactHelper(root, name, 0);
        }

        private bool DeleteContactHelper(TrieNode node, string name, int depth)
        {
            if (node == null)
            {
                return false;
            }

            if (depth == name.Length)
            {
                if (node.IsEndOfWord)
                {
                    node.IsEndOfWord = false;
                    return node.Children.Count == 0;
                }
                return false;
            }

            char ch = name[depth];
            if (DeleteContactHelper(node.Children[ch], name, depth + 1))
            {
                node.Children.Remove(ch);
                return node.Children.Count == 0 && !node.IsEndOfWord;
            }

            return false;
        }

        /// <summary>
        /// Updates a contact's phone number
        /// </summary>
        /// <param name="currentName"></param>
        /// <param name="currentPhone"></param>
        /// <param name="newName"></param>
        /// <param name="newPhone"></param>
        /// <returns></returns>
        public bool UpdateContact(string currentName, string currentPhone, string newName, string newPhone)
        {
            var node = root;
            foreach (var ch in currentName)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return false;
                }
                node = node.Children[ch];
            }

            if (node.IsEndOfWord && node.Contact.Phone == currentPhone)
            {
                // If names are different, we need to remove the old contact and add the new one
                if (currentName != newName)
                {
                    DeleteContact(currentName); // Custom method to remove old contact

                    Contact newContact = new Contact(newName, newPhone);
                    InsertContact(newContact);
                }
                else
                {
                    node.Contact.Phone = newPhone; // Update phone if name is the same
                }

                return true;
            }

            return false; // Contact not found 
        }

        /// <summary>
        /// Retrieves all contacts with a given prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<Contact> GetContactsByPrefix(string prefix)
        {
            TrieNode node = root;
            List<Contact> contacts = new List<Contact>();

            foreach (char ch in prefix)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return contacts; // No contacts with this prefix
                }
                node = node.Children[ch];
            }

            GetAllContactsFromNode(node, contacts);
            return contacts;
        }

        /// <summary>
        /// Finds all contacts starting from a given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="contacts"></param>
        private void GetAllContactsFromNode(TrieNode node, List<Contact> contacts)
        {
            if (node == null)
                return;

            if (node.IsEndOfWord)
            {
                contacts.Add(node.Contact);
            }

            foreach (var child in node.Children)
            {
                GetAllContactsFromNode(child.Value, contacts);
            }
        }
    }
}

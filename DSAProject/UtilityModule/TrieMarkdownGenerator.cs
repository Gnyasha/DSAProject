using static DSAProject.DataStorageModule.Trie;
using System.Text;

namespace DSAProject.UtilityModule
{
    public class TrieMarkdownGenerator
    {
        private TrieNode root;

        public TrieMarkdownGenerator(TrieNode root)
        {
            this.root = root;
        }

        public string GenerateMarkdown()
        {
            var markdown = new StringBuilder();
            GenerateMarkdownRecursive(root, markdown, 0);
            return markdown.ToString();
        }

        private void GenerateMarkdownRecursive(TrieNode node, StringBuilder markdown, int depth)
        {
            foreach (var child in node.Children)
            {
                // Indentation for each depth level
                markdown.Append(new string(' ', depth * 2));

                // Append the current character
                markdown.Append("- ");
                markdown.Append(child.Key);

                if (child.Value.IsEndOfWord)
                {
                    // Add the full contact name and phone number if it's an end of word
                    markdown.Append($" (End of \"{child.Value.Contact.Name}\" - Phone: {child.Value.Contact.Phone})");
                }

                markdown.AppendLine();

                // Recursive call for the next level
                GenerateMarkdownRecursive(child.Value, markdown, depth + 1);
            }
        }
    }

}

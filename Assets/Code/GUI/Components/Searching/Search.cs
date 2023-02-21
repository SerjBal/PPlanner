namespace SerjBal.GUI.Components.Searching
{
    public class Search
    {
        string text = "Extension methods have all the capabilities of regular static methods.";
        private string textToSearch = "";
        bool ignoreCaseSearchResult = factMessage.StartsWith("extension", System.StringComparison.CurrentCultureIgnoreCase);
        Console.WriteLine($"Starts with \"extension\"? {ignoreCaseSearchResult} (ignoring case)");

        bool endsWithSearchResult = factMessage.EndsWith(".", System.StringComparison.CurrentCultureIgnoreCase);
        Console.WriteLine($"Ends with '.'? {endsWithSearchResult}");
    }
}
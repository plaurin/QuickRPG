public class Navigation
{
    public string? Game { get; set; }
    public List<string> Nav { get; } = [];

    public string Path => $"> {Game}";
}
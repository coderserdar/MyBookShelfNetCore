namespace MyBookShelf.Web.Helpers;

public class SpecialJsonResult
{
    public string Id { get; set; }
    public string Header { get; set; }
    public string MessageText { get; set; }
    public bool IsNewRecords { get; set; }
    public string MessageType { get; set; }
    public bool IsError { get; set; }
}
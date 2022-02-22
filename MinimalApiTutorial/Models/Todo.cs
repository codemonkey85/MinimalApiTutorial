namespace MinimalApiTutorial.Models;

public class Todo
{
    [Key]
    [Column]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [Column]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [Column]
    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; } = false;
}

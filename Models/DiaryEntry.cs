using System.ComponentModel.DataAnnotations;

namespace AspProject.Models;

public class DiaryEntry
{
    public int Id { get; set; }
    [StringLength(128, ErrorMessage = "Название должно быть не длиннее {1}.")]
    public string? Head { get; set; }
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "Текст должен быть от {2} до {1}.")]
    public string? Text { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

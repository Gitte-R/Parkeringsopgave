namespace EventService.Models
{
    public record Event(DateTimeOffset OcurredAt, string Name, object Content);
 
}

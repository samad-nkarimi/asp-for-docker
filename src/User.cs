
public class User
{
    public int Id { get; set; }
    public Guid GuidId { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}

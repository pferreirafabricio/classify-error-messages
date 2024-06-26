namespace ClassifyErrorMessages.Core.Entities;

public class Company(string name, string document)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public string Document { get; set; } = document;
}

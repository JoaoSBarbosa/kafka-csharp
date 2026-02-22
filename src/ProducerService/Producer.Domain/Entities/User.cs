using Producer.Domain.Enums;

namespace Producer.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string FistName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public UserStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User()
    {
    }

    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        Email = email;
        FistName = firstName;
        LastName = lastName;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsSent()
    {
        Status = UserStatus.EventSent;
    }
}
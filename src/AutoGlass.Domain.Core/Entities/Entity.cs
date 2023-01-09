using Flunt.Notifications;

namespace AutoGlass.Domain.Core.Entities;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity() // Emtpy for EF
    { }

    protected Entity(Guid? id)
    {
        if (id == Guid.Empty || id is null)
            Id = Guid.NewGuid();
        else
            Id = (Guid)id;
    }

    public Guid Id { get; private set; }
}
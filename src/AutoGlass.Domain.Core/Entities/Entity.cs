using Flunt.Notifications;

namespace AutoGlass.Domain.Core.Entities;

public abstract class Entity : Notifiable<Notification>
{
	protected Entity()
	{
		Id = Guid.NewGuid();
	}

    public Guid Id { get; private set; }
}
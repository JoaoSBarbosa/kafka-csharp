using System.Collections;
using Shared.Contracts.Commons;

namespace Shared.Contracts.Topics;

public class KafkaTopics : Enumeration
{
    public static readonly KafkaTopics Registered = new(1, "registered_user");
    public static readonly KafkaTopics Notifications = new(2, "notificacion_user");

    private KafkaTopics(int id, string name) : base(id, name)
    {
    }
}
namespace acciovac.Application.Behaviors.Messages.Queries.GetUserMessages
{
    public sealed record MessageDto(
        Guid Id,
        Guid UserId,
        string Subject,
        string Body,
        bool IsRead,
        DateTime? ReadAt,
        bool IsResolved,
        DateTime? ResolvedAt,
        DateTime CreatedAt);
}

namespace acciovac.Application.Behaviors.Messages.Queries.GetAllMessages
{
    public sealed record GetAllMessageDto(
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
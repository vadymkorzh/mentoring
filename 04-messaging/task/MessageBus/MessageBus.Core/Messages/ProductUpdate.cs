namespace MessageBus.Core.Messages;

public class ProductUpdate : MessagePayload
{
    public required int ProductId { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Amount { get; init; }
}

namespace CartService.DAL.Models;

public class Cart(int id)
{
    private readonly List<CartItem> _items = [];

    public int Id { get; } = id;

    public IList<CartItem> GetItems() => _items.AsReadOnly();

    public void AddItem(CartItem item) => _items.Add(item);

    public bool RemoveItem(int itemId)
    {
        int index = _items.FindIndex(i => i.Id == itemId);
        if (index < 0)
        {
            return false;
        }

        _items.RemoveAt(index);
        return true;
    }
}
namespace CartService.DAL.Models;

public class Cart(string key)
{
    private readonly List<CartItem> _items = [];

    public string Key { get; } = key;

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
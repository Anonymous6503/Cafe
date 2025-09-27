public class Order
{
    public Customer customer;
    public MenuItemData menuItem;

    public Order(Customer customer, MenuItemData menuItem)
    {
        this.customer = customer;
        this.menuItem = menuItem;
    }
}
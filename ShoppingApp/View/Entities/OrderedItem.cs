namespace ShoppingApp.ViewModel
{
    public class OrderedItem
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public string InfoLine { get; set; }
        public OrderedItem(int id, int cost, string infoLine)
        {
            Id = id;
            Cost = cost;
            InfoLine = infoLine;
        }
    }
}
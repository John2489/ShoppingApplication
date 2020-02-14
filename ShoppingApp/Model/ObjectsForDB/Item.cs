namespace ShoppingApp.Model.ObjectsForDB
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }
        public int ReservedQuantity { get; set; }
    }
}

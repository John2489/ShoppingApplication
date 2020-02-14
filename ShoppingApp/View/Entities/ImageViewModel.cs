namespace ShoppingApp.ViewModel
{
    public class ImageViewModel
    {
        public ImageViewModel(int id, string brand, string series, byte[] image, int cost, int quantiry)
        {
            Id = id;
            Brand = brand;
            Series = series;
            Image = image;
            Cost = cost;
            Quantity = quantiry;
            QuantityOrdered = 10;
        }
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Series { get; set; }
        public byte[] Image { get; set; }
        public int Cost { get; set; }
        public int Quantity { get; set; }
        public int QuantityOrdered { get; set; }
        public object[] Font { get; set; } = new object[2] { null, "Add" };
        public bool NotOrdered { get; set; } = true;
    }
}

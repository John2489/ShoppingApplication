namespace ShoppingApp.Model.ObjectsForDB
{
    public partial class OrderedObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Order { get; set; }
        public string DeliveryInfo { get; set; }
    }
}

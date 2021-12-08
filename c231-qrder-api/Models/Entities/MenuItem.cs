namespace c231_qrder.Models
{
    public class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsOverwritten { get; set; }
    }
}

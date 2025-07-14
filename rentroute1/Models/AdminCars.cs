namespace rentroute1.Models

{
    public class AdminCars
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageData { get; set; } // Base64-encoded image data
        public string Details { get; set; }
        public string ModelType { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}

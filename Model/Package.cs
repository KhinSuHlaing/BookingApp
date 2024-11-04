namespace BookingApp.Model
{
    public class Package
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public int Credit { get; set; }
        public decimal Price { get; set; }
        public DateOnly ExpDate { get; set; }
        public string Country { get; set; }

        public bool Enable { get; set; }


    }
}

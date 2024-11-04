namespace BookingApp.Model
{
    public class UserPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public bool isExpired { get; set; }       
        public DateOnly ExpireDate { get; set; }
        public int RemainingCredit { get; set; }
        
    }
}

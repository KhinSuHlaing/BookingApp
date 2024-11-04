namespace BookingApp.Model
{
    public class UserPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int isExpired { get; set; }       
        public DateTime ExpireDate { get; set; }
        public int RemainingCredit { get; set; }
        
    }
}

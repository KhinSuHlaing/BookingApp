namespace BookingApp.Model
{
    public class ScheduleDTO
    {
        public int Id { get; set; } = 0;
        public string ClassName { get; set; }
        public int? UserId { get; set; }
        public bool? isCancel { get; set; }
        public bool? isInWaitList { get; set; }
        public DateTime? WaitingTime { get; set; }
        public bool? isRefunded { get; set; }
        public bool? isAvailable { get; set; }
        public string? Country { get; set; }

        public int? WaitingUserId { get; set; }

    }
}

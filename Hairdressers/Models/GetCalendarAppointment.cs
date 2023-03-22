namespace Hairdressers.Models {
    public class GetCalendarAppointment {

        public int user_id { get; set; }
        public int hairdresser_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public int[] services { get; set; }

    }
}

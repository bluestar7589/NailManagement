namespace NailManagement.Models
{
    public class ServiceReportViewModel
    {
        /// <summary>
        /// The list of payment report with payment information
        /// </summary>
        public List<ServiceTopDTO> Services { get; set; }

        /// <summary>
        /// Date from the range of date
        /// </summary>
        public DateOnly DateFrom { get; set; }

        /// <summary>
        /// Date to the range of date
        /// </summary>
        public DateOnly DateTo { get; set; }
    }
}

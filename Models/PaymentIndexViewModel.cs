namespace NailManagement.Models
{
    public class PaymentIndexViewModel
    {
        /// <summary>
        /// The list of payment report with payment information
        /// </summary>
        public List<PaymentDTO> Payments { get; set; }

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

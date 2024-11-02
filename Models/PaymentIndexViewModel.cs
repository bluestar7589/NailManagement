namespace NailManagement.Models
{
    public class PaymentIndexViewModel
    {
        public List<PaymentDTO> Payments { get; set; }

        public DateOnly DateFrom { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly DateTo { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}

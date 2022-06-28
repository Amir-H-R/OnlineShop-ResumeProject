namespace Application.Services.FinancialServices.Commands.RequestPayment
{
    public class PaymentRequestDto
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }
        public long? PhoneNumber { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }
        public bool IsPaid { get; set; }
        public long PaymentRequestId { get; set; }
        public DateTime? PayDate { get; set; }
    }
}

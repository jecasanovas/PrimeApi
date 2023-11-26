namespace BLL.Dtos
{
    public class PaymentInfoDto
    {
        public int Id { get; set; }
        public int UserInfoId { get; set; }
        public string Name { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string MonthExp { get; set; }
        public string YearExp { get; set; }
        public string CCV { get; set; }

    }
}

namespace BLL.Dtos
{
    public class AdressesDto
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public char TypeOfDirection { get; set; }
        public string CP { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public int UserInfoId { get; set; }
        public char TypeOfDocument { get; set; }
        public string Document { get; set; }
    }

}


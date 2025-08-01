namespace MyCompany.Models
{
    //предназначен для передачи на клиент. скрывает важную информацию которую пользователю видеть не нужно
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DescriptionShort { get; set; }
        public string? PhotoFileName { get; set; }
        public string? Type { get; set; }
    }
}

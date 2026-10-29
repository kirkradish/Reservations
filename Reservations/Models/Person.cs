namespace Reservations.Models
{
    public class Person
    {
        public int Id {  get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; } = string.Empty;
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

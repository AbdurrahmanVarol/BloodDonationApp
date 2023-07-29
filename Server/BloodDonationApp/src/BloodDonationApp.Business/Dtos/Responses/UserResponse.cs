namespace BloodDonationApp.Business.Dtos.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int BloodGroupId { get; set; }
        public Guid? HospitalId { get; set; }
        public int CityId { get; set; }
        public int GenderId { get; set; }
    }
}

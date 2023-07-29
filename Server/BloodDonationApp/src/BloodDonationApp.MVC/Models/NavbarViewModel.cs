using BloodDonationApp.Entities.Enums;

namespace BloodDonationApp.MVC.Models
{
    public class NavbarViewModel
    {
        public required bool IsAuthenticated { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public Roles UserRole { get; internal set; }
    }
}

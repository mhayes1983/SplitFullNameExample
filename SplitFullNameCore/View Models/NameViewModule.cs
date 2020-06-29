using SplitFullNameCore.Interfaces;

namespace SplitFullNameCore.ViewModels
{
    public class NameViewModule : IName
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}

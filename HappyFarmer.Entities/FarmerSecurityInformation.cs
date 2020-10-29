using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerSecurityInformation : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string AdversitingRule { get; set; }
        public string TermsOfUse { get; set; }
        public string PrivacyPolicy { get; set; }
        public string FAQ { get; set; }
    }
}

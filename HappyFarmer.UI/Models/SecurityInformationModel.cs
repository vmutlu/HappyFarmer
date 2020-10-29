using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class SecurityInformationModel
    {
        public int Id { get; set; }
        public string AdversitingRule { get; set; }
        public string TermsOfUse { get; set; }
        public string PrivacyPolicy { get; set; }
        public string FAQ { get; set; }
    }
}

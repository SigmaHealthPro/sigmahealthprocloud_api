using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Responses
{
    public class ProfileResponse
    {
        public Guid ProfileId { get; set; }
        public string? ProfileName { get; set; }
        public string? IconCode { get; set; }
        public int? ViewOrder { get; set; }
        public ICollection<FeatureResponse> Features { get; set; } = new List<FeatureResponse>();
    }
    public class FeatureResponse
    {
        public Guid FeatureId { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureLink { get; set; }
        public string? IconCode { get; set; }
        public bool HasSubFeature { get; set; }
        public int? ViewOrder { get; set; }
        public List<SubFeatureResponse> SubFeatures { get; set; } = new List<SubFeatureResponse>();
    }

    public class SubFeatureResponse
    {
        public Guid SubFeatureId { get; set; }
        public string? SubFeatureName { get; set; }
        public string? IconCode { get; set; }
        public int? ViewOrder { get; set; }
        public string? SubFeatureLink { get; set; }
    }
}

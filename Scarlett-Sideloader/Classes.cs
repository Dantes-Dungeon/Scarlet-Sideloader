using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scarlett_Sideloader
{

    public class NeededSubmissionInfo
    { 
        public string id { get; set; }
    }
    public class PublisherInfo
    { 
        public string id { get; set; }
        public int sellerId { get; set; }
    }

    public class AppInfo
    {
        public string offerID { get; set; } = null;
        public AppFeatures features { get; set; }
        public string type { get; set; } = "Application";
        public string Name { get; set; }

    }

    public class AppFeatures
    {
        public bool ciso { get; set; } = false;
        public bool demo { get; set; } = false;
        public bool framework { get; set; } = false;
        public bool game { get; set; } = false;
        public bool multiSku { get; set; } = false;
    }

    public class NeededAppInfo { 
        public string id { get; set; }
        public string bigId { get; set; }
    }

    public class NeededGroupInfo
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CreateGroupInfo
    {
        public String[] members { get; set; }
        public string name { get; set; }
        public string groupType { get; set; } = "User";
        public String[] dnATags { get; set; } = { "AppFlighting" };
        public string groupUsage { get; set; } = "ForPackageFlight, ForLimitedDistribution";
    }
}

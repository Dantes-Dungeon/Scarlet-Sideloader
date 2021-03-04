using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scarlett_Sideloader
{
    public class SubmitInfo 
    {
        public bool isAutoPromotionEnabled { get; set; } = false;
        public bool isCertParallelPublishingEnabled { get; set; } = false;
    }

    public class UpdateStatus 
    {
        public int fileStatus { get; set; } = 3;
        public string fileKey { get; set; } = "Keynis";
    }

    public class ScreenshotResponse 
    {
        public string FileId { get; set; }
        public string UploadSasUrl { get; set; }
        public string UploadSasExpirationUtc { get; set; }
    }
    public class ScreenshotInfo 
    {
        public string fileKey { get; set; } = "Keynis";
        public string fileName { get; set; } = "blank.png";
    }
    public class ListingInfo
    {
        public string RequestVerificationToken { get; set; }
        public string ListingId { get; set; }
    }

    public class TempValidateClass
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string SourceVersion { get; set; }
    }

    public class XBLaccInfo
    {
        public string AccountId { get; set; }
        public string Moniker { get; set; }
        public string OpenTierSandboxId { get; set; }
    }
    
    public class CommitalInfo
    { 
        public string Id { get; set; }
    }

    public class NeededMetadata
    {
        public string ChunkSize { get; set; }
    }
    public class Identity
    {
        public string Name { get; set; }
        public string PackageFamilyName { get; set; }
        public string Publisher { get; set; }
        public string PublisherDisplayName { get; set; }
    }
    public class uploadinfo
    { 
        public string XfusId { get; set; }
        public string SasUrl { get; set; }
    }

    public class NeededUploadInfo
    {
        public string Id { get; set; }
        public uploadinfo UploadInfo { get; set; }
    }

    public class CreateUploadInfo 
    {
        public string FileName { get; set; }            
        public string MarketGroupId { get; set; } = null;
    }

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

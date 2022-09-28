using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CommandLine.Binding;
using System.CommandLine;

namespace Scarlett_Sideloader
{

    public class CommandArguments
    {
        public string cookie { get; set; }
        public FileInfo file { get; set; }
        public string? name { get; set; }
        public string screenshotname { get; set; }
        public string description { get; set; }
        public bool app { get; set; }
        public bool publicapp { get; set; }
        public string? emails { get; set; }
        public string? groups { get; set; }
        public bool original { get; set; }
        public bool force { get; set; }

        public int attempts { get ;set; }
    }

    public class CommandArgumentsBinder : BinderBase<CommandArguments>
    {
        private readonly Argument<string> _cookieArgument;
        private readonly Argument<FileInfo> _fileArgument;
        private readonly Option<string?> _nameOption;
        private readonly Option<string> _descriptionOption;
        private readonly Option<string> _screenshotOption;
        private readonly Option<bool> _appOption;
        private readonly Option<bool> _publicOption;
        private readonly Option<string?> _emailsOption;
        private readonly Option<string?> _groupsOption;
        private readonly Option<bool> _originalOption;
        private readonly Option<bool> _forceOption;
        private readonly Option<int> _retryOption;

        public CommandArgumentsBinder(Argument<string> cookieArgument, Argument<FileInfo> fileArgument, Option<string> nameOption, Option<string> descriptionOption, Option<string> screenshotOption, Option<bool> appOption, Option<bool> publicOption, Option<string> emailsOption, Option<string> groupsOption, Option<bool> originalOption, Option<bool> forceOption, Option<int> retryOption)
        {
            _cookieArgument = cookieArgument;
            _fileArgument = fileArgument;
            _nameOption = nameOption;
            _descriptionOption = descriptionOption;
            _screenshotOption = screenshotOption;
            _appOption = appOption;
            _publicOption = publicOption;
            _emailsOption = emailsOption;
            _groupsOption = groupsOption;
            _originalOption = originalOption;
            _forceOption = forceOption;
            _retryOption = retryOption;
        }

        protected override CommandArguments GetBoundValue(BindingContext bindingContext) =>
            new CommandArguments
            {
                cookie = bindingContext.ParseResult.GetValueForArgument(_cookieArgument),
                file = bindingContext.ParseResult.GetValueForArgument(_fileArgument),
                name = bindingContext.ParseResult.GetValueForOption(_nameOption),
                description = bindingContext.ParseResult.GetValueForOption(_descriptionOption),
                screenshotname = bindingContext.ParseResult.GetValueForOption(_screenshotOption),
                app = bindingContext.ParseResult.GetValueForOption(_appOption),
                publicapp = bindingContext.ParseResult.GetValueForOption(_publicOption),
                emails = bindingContext.ParseResult.GetValueForOption(_emailsOption),
                groups = bindingContext.ParseResult.GetValueForOption(_groupsOption),
                original = bindingContext.ParseResult.GetValueForOption(_originalOption),
                force = bindingContext.ParseResult.GetValueForOption(_forceOption),
                attempts = bindingContext.ParseResult.GetValueForOption(_retryOption)
            };
    }

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

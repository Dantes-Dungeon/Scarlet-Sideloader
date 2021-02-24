using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Xml.Linq;
using System.Web;
using System.Diagnostics;

namespace Scarlett_Sideloader
{
    class Program
    {
        public static Uri partneruri = new Uri("https://partner.microsoft.com/");
        public static Uri xboxuri = new Uri("https://upload.xboxlive.com/");

        public static HttpClient client;

        static async Task<int> Main(string[] args)
        {
            var cmd = new RootCommand
            {
                new Argument<string>("cookie", "Your asp.net.cookies"),
                new Argument<FileInfo>("file", "The path to your appx, msix, appxbundle and msixbundle"),
                new Option<string?>(aliases: new String[] {"--name", "-N", "-n"}, description: "Name to use for the app store page (if left blank it will be randomly generated)."),
                new Option<bool>(aliases: new String[] {"--app", "-A", "-a"},  description: "Install as an app rather than a game (defaults to game)."),
                new Option<string?>(aliases: new String[] {"--emails", "-E", "-e"}, description: "Emails to whitelist, seperated by commas."),
                new Option<string?>(aliases: new String[] {"--groups", "-G", "-g"}, description: "Groups to whitelist, seperated by commas."),
                new Option<bool>(aliases: new String[] {"--original", "-O", "-o"}, description: "Keep package file as original."),
            };

            cmd.Handler = CommandHandler.Create<string, FileInfo, string?, bool, string?, string?, bool, IConsole>(HandleInput);

            return await cmd.InvokeAsync(args);
        }

        static public void HandleInput(string cookie, FileInfo file, string? name, bool app, string? emails, string? groups, bool original, IConsole console)
        {
            string filename = file.Name;
            string filepath = file.FullName;
            if ((emails == null) && (groups == null))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must  set either a list of groups to whitelist or a list of emails to whitelist, you cannot leave both blank");
                return;
            }
            //set aspnet cookie and create http client
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(partneruri, new Cookie(".AspNet.Cookies", cookie));
            HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            client = new HttpClient(handler);

            //write some test stuff (delete before prod)
            Console.WriteLine(name);
            Console.WriteLine(app);
            Console.WriteLine(emails);
            Console.WriteLine(groups);
            Console.WriteLine(original);

            //pull needed publisher info
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Pulling publisher info: ");
            PublisherInfo publisherinfo = GetPublisherInfo();
            if (publisherinfo == null)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to pull publisher info, cookie is likely invalid");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }

            //get all needed group info
            //create empty lists of groups and emails
            String[] emaillist;
            String[] grouplist;
            List<NeededGroupInfo> Neededgroups = new List<NeededGroupInfo>();

            //strip whitespace out of groups and emails, not a function due to it only being done twice
            if (emails != null)
            {
                emails = new string(emails.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                emaillist = emails.Split(",");
                string groupname = RandomString(6);
                CreateGroupInfo newgroup = new CreateGroupInfo() {
                    name = groupname,
                    members = emaillist
                };
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Creating new group from emails: ");
                NeededGroupInfo createdgroup = CreateGroup(newgroup);
                if (createdgroup == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to create group, cookie is likely invalid");
                    return;
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success!\n");
                }
                Neededgroups.Add(createdgroup);
            }
            if (groups != null)
            {
                groups = new string(groups.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                grouplist = groups.Split(",");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Pulling group info: ");
                List<NeededGroupInfo> groupsjson = GetAllGroups();
                if (groupsjson == null)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to pull group info, cookie is likely invalid");
                    return;
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success!\n");
                }
                foreach (NeededGroupInfo groupInfo in groupsjson)
                {
                    if (grouplist.Contains(groupInfo.name)) 
                    {
                        Neededgroups.Add(groupInfo);
                    }
                }
            }
            string appname;
            if (name != null)
            {
                appname = name;
            } else {
                appname = RandomString(16);
            }
            AppInfo createappinfo = new AppInfo() 
            { 
                Name = appname, 
                features = new AppFeatures() 
                { 
                    game = !app
                } 
            };

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Creating APP with name {appname}: ");
            NeededAppInfo createdappinfo = CreateApp(createappinfo);
            if (createappinfo == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create app, name is likely already taken");
                return;
            } else {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Creating submission for {appname}: ");
            if (CreateSubmission(createdappinfo) == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create submission for app, cookie is likely invalid");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }

            List<string> groupids = new List<string>();
            foreach (NeededGroupInfo groupinfo in Neededgroups)
            {
                groupids.Add(groupinfo.id);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Getting submission info for {appname}: ");
            List<NeededSubmissionInfo> returnedsubmissions = GetSubmissionInfo(createdappinfo);
            if (returnedsubmissions != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to get submission info, cookie is likely invalid");
                return;
            }

            NeededSubmissionInfo neededsubmissioninfo = returnedsubmissions[0];

            StoreInfo storeinfo = new StoreInfo()
            {
                ProductName = appname,
                BigId = createdappinfo.bigId,
                PublisherId = publisherinfo.sellerId,
                Visibility = new APPVisibility() 
                {
                    GroupIds = groupids
                }
            };

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Setting Pricing and Availibility for {appname}: ");
            if (SetAvailibility(storeinfo, neededsubmissioninfo))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to set pricing and availibility for app, cookie is likely invalid");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Getting identity info for {appname}: ");

            Identity identity = GetIdentityInfo(createdappinfo);
            if (identity != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to get identity info for app, cookie is likely invalid");
                return;
            }

            //create age rating application
            AgeRatingApplication ageratingapplication = new AgeRatingApplication() { product = new Product() { alias = appname } };
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Setting Age Ratings for {appname}: ");
            if (SetAgeRatings(createdappinfo, neededsubmissioninfo, ageratingapplication))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to set age ratings for app, cookie is likely invalid");
            }

            //patch the package
            if (!original) 
            {
                bool uploadfile = false;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Starting to patch {filename}: ");
                if ((file.Extension == ".appxbundle") || (file.Extension == ".msixbundle"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Detected Bundle Package\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Extracting {filename}: ");
                    string bundlepath = Path.Join(Path.GetTempPath(), "bundle");
                    ZipFile.ExtractToDirectory(file.FullName, bundlepath, true);
                    XDocument AppxBundleManifest = XDocument.Load(Path.Join(bundlepath, "AppxMetadata\\AppxBundleManifest.xml"));
                    XNamespace ns = AppxBundleManifest.Root.GetDefaultNamespace();
                    XElement packages = AppxBundleManifest.Root.Element(XName.Get("Packages", ns.NamespaceName));
                    if (packages != null)
                    {
                        foreach (XElement package in packages.Elements())
                        {
                            if ((package.Attribute("Type").Value == "application"))
                            {
                                if (package.Attribute("Architecture").Value == "x64")
                                {
                                    filepath = Path.Join(bundlepath, HttpUtility.UrlPathEncode(package.Attribute("FileName").Value));
                                    filename = HttpUtility.UrlPathEncode(package.Attribute("FileName").Value);
                                }
                            }
                        }
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Failed to find packages from package bundle\n");
                        return;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success!\n");
                }
                else if ((file.Extension == ".appxupload") || (file.Extension == ".msixupload"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Detected upload file (please use the --original flag in future)\n");
                    uploadfile = true;
                }
                else if ((file.Extension == ".appx") || (file.Extension == ".msix"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Detected package file\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Unknown filetype\n");
                    return;
                }
                
                if (!uploadfile)
                {
                    //patch appxmanifest
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Patching appxmanifest for {filename}: ");
                    string packagepath = Path.Join(Path.GetTempPath(), "package");
                    ZipFile.ExtractToDirectory(filepath, packagepath, true);
                    XDocument AppxManifest = XDocument.Load(Path.Join(packagepath, "AppxManifest.xml"));
                    string defaultnamespace = AppxManifest.Root.GetDefaultNamespace().NamespaceName;
                    XElement identityelement = AppxManifest.Root.Element(XName.Get("Identity", defaultnamespace));
                    identityelement.Attribute("Name").Value = identity.Name;
                    identityelement.Attribute("Publisher").Value = identity.Publisher;
                    identityelement.Attribute("ProcessorArchitecture").Value = "x64";
                    string[] Version = identityelement.Attribute("Version").Value.Split(".");
                    Version[3] = "0";
                    identityelement.Attribute("Version").Value = string.Join(".", Version);
                    XElement displayname = AppxManifest.Root.Element(XName.Get("Properties", defaultnamespace)).Element(XName.Get("DisplayName", defaultnamespace));
                    displayname.Value = appname;
                    XElement publisherdisplayname = AppxManifest.Root.Element(XName.Get("Properties", defaultnamespace)).Element(XName.Get("PublisherDisplayName", defaultnamespace));
                    publisherdisplayname.Value = identity.PublisherDisplayName;
                    AppxManifest.Save(Path.Join(packagepath, "AppxManifest.xml"));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Generating patched {filename}: ");
                    if (File.Exists(Path.Join(Directory.GetCurrentDirectory(), "Appxpacker", "MakeAppx.exe")))
                    {
                        string appxpath = Path.Join(Path.GetTempPath(), $"Patched-{filename}");
                        if (MakeAppx(packagepath, appxpath))
                        {
                            filepath = appxpath;
                            filename = $"Patched-{filename}";
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Success!\n");
                        } else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Failed to create patched appx\n");
                            return;
                        }
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("MakeAppx.exe was not found\n");
                        return;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Getting upload info for {appname}: ");
            CreateUploadInfo createuploadinfo = new CreateUploadInfo() { FileName = filename };
            NeededUploadInfo neededuploadinfo = GetUploadInfo(createuploadinfo, neededsubmissioninfo);
            if (neededuploadinfo != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to get upload info for app, cookie is likely invalid");
            }
            string token = HttpUtility.ParseQueryString(new Uri(neededuploadinfo.UploadInfo.SasUrl).Query).Get("token");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Setting Metadata for {appname}: ");

            //read package as a filestream
            FileStream packagestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            //set the metadata for the file this has to be done here as unfortunately there are too many variables to pass, it is bad practice I know but its just too much to put into a function
            string longurl = $"https://upload.xboxlive.com/upload/setmetadata/{neededuploadinfo.UploadInfo.XfusId}";
            var uriBuilder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["filename"] = filename;
            query["fileSize"] = packagestream.Length.ToString();
            query["token"] = token;
            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Options, longurl);
            var response = client.SendAsync(request);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to set metadata for app, cookie is likely invalid");
                return;
            }

            request = new HttpRequestMessage(HttpMethod.Post, longurl);
            response = client.SendAsync(request);
            response.Wait();

            NeededMetadata neededmetadata;
            if (!response.Result.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to set metadata for app, cookie is likely invalid");
                return;
            }

            string responseresult = response.Result.Content.ReadAsStringAsync().Result;
            neededmetadata = JsonConvert.DeserializeObject<NeededMetadata>(responseresult);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Success!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Calculating number of chunks needed for {appname}: ");
            //chunksize in bytes
            int chunksize = Convert.ToInt32(neededmetadata.ChunkSize);
            //number of chunks
            decimal chunksnum = packagestream.Length / chunksize;
            int chunks = Convert.ToInt32(Math.Ceiling(chunksnum))+1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Success!\n");

            for (int i = 0; i < chunks; i++)
            {
                int chunknum = i + 1;
                if (!UploadChunk(token, chunknum, null, neededuploadinfo, HttpMethod.Options))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to setmetadata for chunk");
                    return;
                }
            }

                for (int i = 0; i < chunks; i++)
            {
                long position = (i * (long)chunksize);
                int toRead = (int)Math.Min(packagestream.Length - position + 1, chunksize);
                byte[] buffer = new byte[toRead];
                packagestream.Read(buffer, 0, buffer.Length);
                int chunknum = i + 1;   
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Uploading chunk number {chunknum} of {chunks}: ");
                if (UploadChunk(token, chunknum, buffer, neededuploadinfo, HttpMethod.Post))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success!\n");
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to upload chunk");
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Marking upload as finished: ");

            if (!UploadFinished(token, neededuploadinfo, HttpMethod.Options))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to mark upload as finished");
                return;
            }

            if (UploadFinished(token, neededuploadinfo, HttpMethod.Post))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to mark upload as finished");
                return;
            }

            CommitalInfo commitalinfo = new CommitalInfo() { Id = neededuploadinfo.Id};

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Commiting upload: ");
            if (CommitUpload(commitalinfo))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to commit upload");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Set target platforms: ");

            if (SetPlatforms(neededsubmissioninfo))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Success!\n");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to set target platforms");
                return;
            }
            Console.WriteLine();

            // TODO: a great app
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static PublisherInfo GetPublisherInfo()
        {
            var response = client.GetAsync((partneruri.ToString() + $"/en-us/dashboard/account/v3/api/accessmanagement/userinfobypuid"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<PublisherInfo>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static List<NeededGroupInfo> GetAllGroups()
        {
            var response = client.GetAsync((partneruri.ToString() + "/dashboard/monetization/group-management/api/groups"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<NeededGroupInfo>>(responseresult);
            } else {
                return null;
            }
        }

        static NeededGroupInfo CreateGroup(CreateGroupInfo creategroupinfo)
        {
            var stringPayload = JsonConvert.SerializeObject(creategroupinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + "/dashboard/monetization/group-management/api/groups/"), content);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<NeededGroupInfo>(responseresult);
            }
            else
            {
                Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);
                return null;
            }
        }

        static NeededAppInfo CreateApp(AppInfo appinfo)
        {
            var stringPayload = JsonConvert.SerializeObject(appinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + "/en-US/dashboard/product/api/products"), content);
            response.Wait();    
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<NeededAppInfo>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static NeededAppInfo CreateSubmission(NeededAppInfo appinfo)
        {
            var stringPayload = "{ \"publishingDetailsVisible\":false}";
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + $"/en-us/dashboard/product/api/{appinfo.id}/submissions"), content);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<NeededAppInfo>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static List<NeededSubmissionInfo> GetSubmissionInfo(NeededAppInfo createdappinfo)
        {
            var response = client.GetAsync((partneruri.ToString() + $"/en-us/dashboard/product/api/{createdappinfo.id}/submissions"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<NeededSubmissionInfo>>(responseresult);
            }
            else
            {
                return null;
            }
        }

        

        static bool SetAvailibility(StoreInfo storeinfo, NeededSubmissionInfo neededsubmissioninfo)
        {
            var stringPayload = JsonConvert.SerializeObject(storeinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + $"/en-us/dashboard/availability/api/product/{storeinfo.BigId}/submissions/{neededsubmissioninfo.id}"), content);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool SetAgeRatings(NeededAppInfo neededAppInfo, NeededSubmissionInfo neededsubmissioninfo, AgeRatingApplication ageratingapplication)
        {
            var stringPayload = JsonConvert.SerializeObject(ageratingapplication);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync((partneruri.ToString() + $"/en-US/dashboard/ageratings/api/products/{neededAppInfo.bigId}/settings?submissionId={neededsubmissioninfo.id}"), content);
            response.Wait();    
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static NeededUploadInfo GetUploadInfo(CreateUploadInfo createuploadinfo, NeededSubmissionInfo neededsubmissioninfo)
        {
            var stringPayload = JsonConvert.SerializeObject(createuploadinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + $"/dashboard/packages/api/pkg/v2.0/packages?packageSetId=PS-{neededsubmissioninfo.id}&storage=xfus"), content);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<NeededUploadInfo>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static bool SetPlatforms(NeededSubmissionInfo neededsubmissioninfo)
        {
            var stringPayload = JsonConvert.SerializeObject(new TargetPlatforms());
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, (partneruri.ToString() + $"/dashboard/packages/api/pkg/v2.0/packagesets/PS-{neededsubmissioninfo.id}?groupId=Base"));
            request.Content = content;
            var response = client.SendAsync(request);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool UploadChunk(string token, int chunknum, byte[] chunk, NeededUploadInfo neededuploadinfo, HttpMethod httpmethod) 
        {
            var uriBuilder = new UriBuilder((xboxuri + $"/upload/uploadchunk/{neededuploadinfo.UploadInfo.XfusId}"));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["blockNumber"] = chunknum.ToString();
            query["runUploadSynchronously"] = true.ToString();
            query["token"] = token;
            uriBuilder.Query = query.ToString();
            string uristring = uriBuilder.ToString();
            var request = new HttpRequestMessage(httpmethod, uristring);
            if (chunk != null)
            {
                request.Content = new ByteArrayContent(chunk);
            }
            var response = client.SendAsync(request);
            try
            {
                response.Wait();
            }
            catch
            {
                return false;
            }
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool UploadFinished(string token, NeededUploadInfo neededuploadinfo, HttpMethod httpmethod)
        {
            var uriBuilder = new UriBuilder((xboxuri + $"/upload/finished/{neededuploadinfo.UploadInfo.XfusId}"));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["callback"] = null;
            query["token"] = token;
            uriBuilder.Query = query.ToString();
            string uristring = uriBuilder.ToString();
            var request = new HttpRequestMessage(httpmethod, uristring);
            var response = client.SendAsync(request);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool CommitUpload(CommitalInfo commitalinfo)
        {
            var stringPayload = JsonConvert.SerializeObject(commitalinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + $"/dashboard/packages/api/pkg/v2.0/packages/{commitalinfo.Id}/commit"), content);
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Identity GetIdentityInfo(NeededAppInfo neededappinfo)
        {
            var response = client.GetAsync((partneruri.ToString() + $"/dashboard/packages/api/pkg/v2.0/packageidentities?productbigid={neededappinfo.bigId}"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Identity>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static string RunProcess(string fileName, string args)
        {
            string text = "";
            string result = "";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            System.Diagnostics.Process process2 = process;
            process2.Start();
            while (!process2.StandardOutput.EndOfStream)
            {
                text = process2.StandardOutput.ReadLine();
                if (text.Length > 0)
                {
                    result = text;
                }
            }
            return result;
        }
        //reused code from appx packer, its shit I know but I don't have the energy to write soemthing better
        static bool MakeAppx(string inputfolder, string outputpath)
        {
            string makeappxpath = Path.Join(Directory.GetCurrentDirectory(), "Appxpacker", "MakeAppx.exe");
            string args = $"pack -d \"{inputfolder}\" -p \"{outputpath}\" -l";
    
            if (File.Exists(outputpath))
            {
                File.Delete(outputpath);
            }

            if (RunProcess(makeappxpath, args).ToLower().Contains("succeeded"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
    }
}

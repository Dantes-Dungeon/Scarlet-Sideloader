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

namespace Scarlett_Sideloader
{
    class Program
    {
        public static Uri partneruri = new Uri("https://partner.microsoft.com/");

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
            Console.WriteLine();

            // TODO: a great app
        }

        static PublisherInfo GetPublisherInfo()
        {
            var response = client.GetAsync((partneruri.ToString() + $"/en-us/dashboard/account/v3/api/accessmanagement/userinfobypuid"));
            response.Wait();
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                
                string responseresult = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<NeededGroupInfo>(responseresult);
            }
            else
            {
                return null;
            }
        }

        static NeededAppInfo CreateApp(AppInfo appinfo)
        {
            var stringPayload = JsonConvert.SerializeObject(appinfo);
            var content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync((partneruri.ToString() + "/en-US/dashboard/product/api/products"), content);
            response.Wait();    
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
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
            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                Console.WriteLine(response.Result.Content.ReadAsStringAsync().Result);
                return false;
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

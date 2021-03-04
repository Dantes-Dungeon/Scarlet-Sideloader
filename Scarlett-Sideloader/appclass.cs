using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scarlett_Sideloader
{
    public class Language
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }

    public class Languages
    {
        public List<Language> SupportedLanguages { get; set; } = new List<Language>() { new Language() { Selected = true, Id = 4, DisplayName = "English (United States)" } };
        public List<Language> OptionalLanguages { get; set; } = new List<Language>() { };
        public string AppId { get; set; }
        public string SubId { get; set; }
        public bool IsEditable { get; set; } = true;
        public int ProductType { get; set; } = 4;
    }

    public class LocalizedTitleName
    {
        public string Locale { get; set; } = "en-US";
        public string Value { get; set; }
    }

    public class EnableClass
    {
        public string ProductType { get; set; } = "Game";
        public string XboxLiveTier { get; set; } = "Open";
        public List<LocalizedTitleName> LocalizedTitleNames { get; set; } = new List<LocalizedTitleName>() { new LocalizedTitleName(){} };
        public List<string> TargetPlatforms { get; set; } = new List<string>() { "XboxOne", "WindowsOneCore" };
        public string DevDisplayLocale { get; set; } = "en-US";
    }


    public class displayclaims
    {
        public string eid { get; set; }
        public string enm { get; set; }
        public string eam { get; set; }
        public string eat { get; set; }
        public string eai { get; set; }
        public string epi { get; set; }
        public string ept { get; set; }
        public string esi { get; set; }
        public string eap { get; set; }
        public string eps { get; set; }
    }

    public class ReturnedAuthInfo
    {
        public string Token { get; set; }
        public displayclaims DisplayClaims { get; set; }
    }

    public class PropertiesClass
    {
        public string DseAppId { get; set; }
        public string Sandboxes { get; set; } = "CERT CERT.DEBUG RETAIL ALL HIDDEN HISTORY";
    }

    public class XBLAuthInfo
    {
        public PropertiesClass Properties { get; set; } = new PropertiesClass();
        public string TokenType { get; set; } = "http://oauth.net/grant_type/jwt/1.0/bearer";
        public string RelyingParty { get; set; } = "http://developer.xboxlive.com";
    }

    public class AlternateId
    {
        public string Value { get; set; }
        public int AlternateIdType { get; set; }
    }

    public class XBLids
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public string MsaAppId { get; set; }
        public string PfnId { get; set; }
        public int TitleId { get; set; }
        public string XboxLiveTier { get; set; }
        public bool IsTest { get; set; }
        public List<AlternateId> AlternateIds { get; set; }
        public string PrimaryServiceConfigId { get; set; }
        public string ProductName { get; set; }
    }

    public class SupportedTargetPlatforms
    {
        public string Desktop { get; set; } = "Enabled";
        public string Mobile { get; set; } = "Enabled";
        public string Holographic { get; set; } = "Enabled";
        public string Team { get; set; } = "Enabled";
        public string Xbox { get; set; } = "Enabled";
        public string Core { get; set; } = "Enabled";
    }

    public class TargetPlatforms
    {
        public SupportedTargetPlatforms SupportedTargetPlatforms { get; set; } = new SupportedTargetPlatforms();
    }


    public class Product
    {
        public string alias { get; set; }
    }

    public class Questionnaire
    {
        public string version { get; set; } = "8.1";
        public bool completed { get; set; } = true;
        public List<int> responses { get; set; } = new List<int>() {2558,2618,2628,2630,2370,2632,2334,2223,2634};
}

    public class AgeRatingApplication
    {
        public Product product { get; set; } = new Product();
        public string ratingType { get; set; } = "application";
        public string mode { get; set; } = "questionnaire";
        public List<string> availablemodes { get; set; } = new List<string>(){ "questionnaire", "import" };
        public bool @readonly { get; set; } = false;
        public string status { get; set; } = "NotStarted";
        public string cultureName { get; set; } = "en-US";
        public Questionnaire questionnaire { get; set; } = new Questionnaire();
        public object marketsMissingRatings { get; set; } = null;
        public object marketsWithRCRatings { get; set; } = null;
        public bool complete { get; set; } = false;
        public bool inProgress { get; set; } = false;
    }


    //storeinfo

    public class HourOption
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class TimeZoneOption
    {
        public string Text { get; set; }
        public bool Value { get; set; }
    }

    public class Market
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; } = "World Wide";
        public bool XBox { get; set; } = false;
        public bool IsEnabled { get; set; } = true;
        public bool IsSelected { get; set; } = false;
        public object GroupId { get; set; } = null;
        public string TaxRemitStatus { get; set; } = "ISV";
        public object States { get; set; } = null;
    }

    public class DomainData
    {
        public List<HourOption> HourOptions { get; set; } = new List<HourOption>() 
        { 
            new HourOption() { Text = "midnight", Value = "0" },
            new HourOption() { Text = "01:00", Value = "1" },
            new HourOption() { Text = "02:00", Value = "2" },
            new HourOption() { Text = "03:00", Value = "3" },
            new HourOption() { Text = "04:00", Value = "4" },
            new HourOption() { Text = "05:00", Value = "5" },
            new HourOption() { Text = "06:00", Value = "6" },
            new HourOption() { Text = "07:00", Value = "7" },
            new HourOption() { Text = "08:00", Value = "8" },
            new HourOption() { Text = "09:00", Value = "9" },
            new HourOption() { Text = "10:00", Value = "10" },
            new HourOption() { Text = "11:00", Value = "11" },
            new HourOption() { Text = "12:00", Value = "12" },
            new HourOption() { Text = "13:00", Value = "13" },
            new HourOption() { Text = "14:00", Value = "14" },
            new HourOption() { Text = "15:00", Value = "15" },
            new HourOption() { Text = "16:00", Value = "16" },
            new HourOption() { Text = "17:00", Value = "17" },
            new HourOption() { Text = "18:00", Value = "18" },
            new HourOption() { Text = "19:00", Value = "19" },
            new HourOption() { Text = "20:00", Value = "20" },
            new HourOption() { Text = "21:00", Value = "21" },
            new HourOption() { Text = "22:00", Value = "22" },
            new HourOption() { Text = "23:00", Value = "23" }
        };
        public List<TimeZoneOption> TimeZoneOptions { get; set; } = new List<TimeZoneOption>() 
        { 
            new TimeZoneOption() { Text = "UTC", Value = false},
            new TimeZoneOption() { Text = "Local", Value = true}
        };
        public List<Market> Markets { get; set; } = new List<Market>() 
        {
            new Market() {Id = 86, Code = "AF", Name="Afghanistan", XBox = false},
            new Market() {Id = 87, Code = "AX", Name="Åland Islands", XBox = false},
            new Market() {Id = 88, Code = "AL", Name="Albania", XBox = false},
            new Market() {Id = 32, Code = "DZ", Name="Algeria", XBox = false},
            new Market() {Id = 89, Code = "AS", Name="American Samoa", XBox = false},
            new Market() {Id = 90, Code = "AD", Name="Andorra", XBox = false},
            new Market() {Id = 91, Code = "AO", Name="Angola", XBox = false},
            new Market() {Id = 92, Code = "AI", Name="Anguilla", XBox = false},
            new Market() {Id = 93, Code = "AQ", Name="Antarctica", XBox = false},
            new Market() {Id = 94, Code = "AG", Name="Antigua and Barbuda", XBox = false},
            new Market() {Id = 33, Code = "AR", Name="Argentina", XBox = true},
            new Market() {Id = 95, Code = "AM", Name="Armenia", XBox = false},
            new Market() {Id = 96, Code = "AW", Name="Aruba", XBox = false},
            new Market() {Id = 10, Code = "AU", Name="Australia", XBox = true},
            new Market() {Id = 11, Code = "AT", Name="Austria", XBox = true},
            new Market() {Id = 145, Code = "AZ", Name="Azerbaijan", XBox = false},
            new Market() {Id = 146, Code = "BS", Name="Bahamas", XBox = false},
            new Market() {Id = 34, Code = "BH", Name="Bahrain", XBox = false},
            new Market() {Id = 147, Code = "BD", Name="Bangladesh", XBox = false},
            new Market() {Id = 148, Code = "BB", Name="Barbados", XBox = false},
            new Market() {Id = 149, Code = "BY", Name="Belarus", XBox = false},
            new Market() {Id = 12, Code = "BE", Name="Belgium", XBox = true},
            new Market() {Id = 150, Code = "BZ", Name="Belize", XBox = false},
            new Market() {Id = 151, Code = "BJ", Name="Benin", XBox = false},
            new Market() {Id = 152, Code = "BM", Name="Bermuda", XBox = false},
            new Market() {Id = 153, Code = "BT", Name="Bhutan", XBox = false},
            new Market() {Id = 97, Code = "BO", Name="Bolivia", XBox = false},
            new Market() {Id = 98, Code = "BQ", Name="Bonaire", XBox = false},
            new Market() {Id = 99, Code = "BA", Name="Bosnia and Herzegovina", XBox = false},
            new Market() {Id = 100, Code = "BW", Name="Botswana", XBox = false},
            new Market() {Id = 101, Code = "BV", Name="Bouvet Island", XBox = false},
            new Market() {Id = 13, Code = "BR", Name="Brazil", XBox = true},
            new Market() {Id = 102, Code = "IO", Name="British Indian Ocean Territory", XBox = false},
            new Market() {Id = 237, Code = "VG", Name="British Virgin Islands", XBox = false},
            new Market() {Id = 103, Code = "BN", Name="Brunei", XBox = false},
            new Market() {Id = 35, Code = "BG", Name="Bulgaria", XBox = false},
            new Market() {Id = 104, Code = "BF", Name="Burkina Faso", XBox = false},
            new Market() {Id = 105, Code = "BI", Name="Burundi", XBox = false},
            new Market() {Id = 108, Code = "CV", Name="Cabo Verde", XBox = false},
            new Market() {Id = 106, Code = "KH", Name="Cambodia", XBox = false},
            new Market() {Id = 107, Code = "CM", Name="Cameroon", XBox = false},
            new Market() {Id = 14, Code = "CA", Name="Canada", XBox = true},
            new Market() {Id = 109, Code = "KY", Name="Cayman Islands", XBox = false},
            new Market() {Id = 110, Code = "CF", Name="Central African Republic", XBox = false},
            new Market() {Id = 111, Code = "TD", Name="Chad", XBox = false},
            new Market() {Id = 36, Code = "CL", Name="Chile", XBox = true},
            new Market() {Id = 15, Code = "CN", Name="China", XBox = true},
            new Market() {Id = 158, Code = "CX", Name="Christmas Island", XBox = false},
            new Market() {Id = 159, Code = "CC", Name="Cocos (Keeling) Islands", XBox = false},
            new Market() {Id = 37, Code = "CO", Name="Colombia", XBox = true},
            new Market() {Id = 154, Code = "KM", Name="Comoros", XBox = false},
            new Market() {Id = 155, Code = "CG", Name="Congo", XBox = false},
            new Market() {Id = 156, Code = "CD", Name="Congo (DRC)", XBox = false},
            new Market() {Id = 157, Code = "CK", Name="Cook Islands", XBox = false},
            new Market() {Id = 38, Code = "CR", Name="Costa Rica", XBox = false},
            new Market() {Id = 160, Code = "CI", Name="Côte d'Ivoire", XBox = false},
            new Market() {Id = 39, Code = "HR", Name="Croatia", XBox = false},
            new Market() {Id = 161, Code = "CW", Name="Curaçao", XBox = false},
            new Market() {Id = 40, Code = "CY", Name="Cyprus", XBox = false},
            new Market() {Id = 41, Code = "CZ", Name="Czechia", XBox = true},
            new Market() {Id = 42, Code = "DK", Name="Denmark", XBox = true},
            new Market() {Id = 113, Code = "DJ", Name="Djibouti", XBox = false},
            new Market() {Id = 114, Code = "DM", Name="Dominica", XBox = false},
            new Market() {Id = 115, Code = "DO", Name="Dominican Republic", XBox = false},
            new Market() {Id = 116, Code = "EC", Name="Ecuador", XBox = false},
            new Market() {Id = 43, Code = "EG", Name="Egypt", XBox = false},
            new Market() {Id = 117, Code = "SV", Name="El Salvador", XBox = false},
            new Market() {Id = 118, Code = "GQ", Name="Equatorial Guinea", XBox = false},
            new Market() {Id = 119, Code = "ER", Name="Eritrea", XBox = false},
            new Market() {Id = 44, Code = "EE", Name="Estonia", XBox = false},
            new Market() {Id = 248, Code = "SZ", Name="Eswatini", XBox = false},
            new Market() {Id = 120, Code = "ET", Name="Ethiopia", XBox = false},
            new Market() {Id = 121, Code = "FK", Name="Falkland Islands", XBox = false},
            new Market() {Id = 122, Code = "FO", Name="Faroe Islands", XBox = false},
            new Market() {Id = 123, Code = "FJ", Name="Fiji", XBox = false},
            new Market() {Id = 45, Code = "FI", Name="Finland", XBox = true},
            new Market() {Id = 8, Code = "FR", Name="France", XBox = true},
            new Market() {Id = 124, Code = "GF", Name="French Guiana", XBox = false},
            new Market() {Id = 125, Code = "PF", Name="French Polynesia", XBox = false},
            new Market() {Id = 126, Code = "TF", Name="French Southern Territories", XBox = false},
            new Market() {Id = 127, Code = "GA", Name="Gabon", XBox = false},
            new Market() {Id = 128, Code = "GM", Name="Gambia", XBox = false},
            new Market() {Id = 129, Code = "GE", Name="Georgia", XBox = false},
            new Market() {Id = 7, Code = "DE", Name="Germany", XBox = true},
            new Market() {Id = 130, Code = "GH", Name="Ghana", XBox = false},
            new Market() {Id = 131, Code = "GI", Name="Gibraltar", XBox = false},
            new Market() {Id = 46, Code = "GR", Name="Greece", XBox = true},
            new Market() {Id = 132, Code = "GL", Name="Greenland", XBox = false},
            new Market() {Id = 133, Code = "GD", Name="Grenada", XBox = false},
            new Market() {Id = 134, Code = "GP", Name="Guadeloupe", XBox = false},
            new Market() {Id = 135, Code = "GU", Name="Guam", XBox = false},
            new Market() {Id = 136, Code = "GT", Name="Guatemala", XBox = false},
            new Market() {Id = 137, Code = "GG", Name="Guernsey", XBox = false},
            new Market() {Id = 138, Code = "GN", Name="Guinea", XBox = false},
            new Market() {Id = 139, Code = "GW", Name="Guinea-Bissau", XBox = false},
            new Market() {Id = 140, Code = "GY", Name="Guyana", XBox = false},
            new Market() {Id = 141, Code = "HT", Name="Haiti", XBox = false},
            new Market() {Id = 142, Code = "HM", Name="Heard Island and McDonald Islands", XBox = false},
            new Market() {Id = 143, Code = "HN", Name="Honduras", XBox = false},
            new Market() {Id = 17, Code = "HK", Name="Hong Kong SAR", XBox = true},
            new Market() {Id = 47, Code = "HU", Name="Hungary", XBox = true},
            new Market() {Id = 144, Code = "IS", Name="Iceland", XBox = false},
            new Market() {Id = 6, Code = "IN", Name="India", XBox = true},
            new Market() {Id = 48, Code = "ID", Name="Indonesia", XBox = false},
            new Market() {Id = 85, Code = "IQ", Name="Iraq", XBox = false},
            new Market() {Id = 18, Code = "IE", Name="Ireland", XBox = true},
            new Market() {Id = 176, Code = "IM", Name="Isle of Man", XBox = false},
            new Market() {Id = 49, Code = "IL", Name="Israel", XBox = true},
            new Market() {Id = 50, Code = "IT", Name="Italy", XBox = true},
            new Market() {Id = 162, Code = "JM", Name="Jamaica", XBox = false},
            new Market() {Id = 4, Code = "JP", Name="Japan", XBox = true},
            new Market() {Id = 164, Code = "JE", Name="Jersey", XBox = false},
            new Market() {Id = 51, Code = "JO", Name="Jordan", XBox = false},
            new Market() {Id = 52, Code = "KZ", Name="Kazakhstan", XBox = false},
            new Market() {Id = 165, Code = "KE", Name="Kenya", XBox = false},
            new Market() {Id = 166, Code = "KI", Name="Kiribati", XBox = false},
            new Market() {Id = 19, Code = "KR", Name="Korea", XBox = true},
            new Market() {Id = 53, Code = "KW", Name="Kuwait", XBox = false},
            new Market() {Id = 167, Code = "KG", Name="Kyrgyzstan", XBox = false},
            new Market() {Id = 168, Code = "LA", Name="Laos", XBox = false},
            new Market() {Id = 54, Code = "LV", Name="Latvia", XBox = false},
            new Market() {Id = 55, Code = "LB", Name="Lebanon", XBox = false},
            new Market() {Id = 169, Code = "LS", Name="Lesotho", XBox = false},
            new Market() {Id = 170, Code = "LR", Name="Liberia", XBox = false},
            new Market() {Id = 56, Code = "LY", Name="Libya", XBox = false},
            new Market() {Id = 171, Code = "LI", Name="Liechtenstein", XBox = false},
            new Market() {Id = 57, Code = "LT", Name="Lithuania", XBox = false},
            new Market() {Id = 20, Code = "LU", Name="Luxembourg", XBox = false},
            new Market() {Id = 172, Code = "MO", Name="Macao SAR", XBox = false},
            new Market() {Id = 174, Code = "MG", Name="Madagascar", XBox = false},
            new Market() {Id = 175, Code = "MW", Name="Malawi", XBox = false},
            new Market() {Id = 58, Code = "MY", Name="Malaysia", XBox = false},
            new Market() {Id = 192, Code = "MV", Name="Maldives", XBox = false},
            new Market() {Id = 193, Code = "ML", Name="Mali", XBox = false},
            new Market() {Id = 59, Code = "MT", Name="Malta", XBox = false},
            new Market() {Id = 177, Code = "MH", Name="Marshall Islands", XBox = false},
            new Market() {Id = 178, Code = "MQ", Name="Martinique", XBox = false},
            new Market() {Id = 179, Code = "MR", Name="Mauritania", XBox = false},
            new Market() {Id = 180, Code = "MU", Name="Mauritius", XBox = false},
            new Market() {Id = 181, Code = "YT", Name="Mayotte", XBox = false},
            new Market() {Id = 21, Code = "MX", Name="Mexico", XBox = true},
            new Market() {Id = 182, Code = "FM", Name="Micronesia", XBox = false},
            new Market() {Id = 183, Code = "MD", Name="Moldova", XBox = false},
            new Market() {Id = 184, Code = "MC", Name="Monaco", XBox = false},
            new Market() {Id = 185, Code = "MN", Name="Mongolia", XBox = false},
            new Market() {Id = 243, Code = "ME", Name="Montenegro", XBox = false},
            new Market() {Id = 186, Code = "MS", Name="Montserrat", XBox = false},
            new Market() {Id = 60, Code = "MA", Name="Morocco", XBox = false},
            new Market() {Id = 187, Code = "MZ", Name="Mozambique", XBox = false},
            new Market() {Id = 188, Code = "MM", Name="Myanmar", XBox = false},
            new Market() {Id = 189, Code = "NA", Name="Namibia", XBox = false},
            new Market() {Id = 190, Code = "NR", Name="Nauru", XBox = false},
            new Market() {Id = 191, Code = "NP", Name="Nepal", XBox = false},
            new Market() {Id = 61, Code = "NL", Name="Netherlands", XBox = true},
            new Market() {Id = 194, Code = "NC", Name="New Caledonia", XBox = false},
            new Market() {Id = 22, Code = "NZ", Name="New Zealand", XBox = true},
            new Market() {Id = 195, Code = "NI", Name="Nicaragua", XBox = false},
            new Market() {Id = 196, Code = "NE", Name="Niger", XBox = false},
            new Market() {Id = 197, Code = "NG", Name="Nigeria", XBox = false},
            new Market() {Id = 198, Code = "NU", Name="Niue", XBox = false},
            new Market() {Id = 199, Code = "NF", Name="Norfolk Island", XBox = false},
            new Market() {Id = 173, Code = "MK", Name="North Macedonia", XBox = false},
            new Market() {Id = 213, Code = "MP", Name="Northern Mariana Islands", XBox = false},
            new Market() {Id = 62, Code = "NO", Name="Norway", XBox = true},
            new Market() {Id = 63, Code = "OM", Name="Oman", XBox = false},
            new Market() {Id = 64, Code = "PK", Name="Pakistan", XBox = false},
            new Market() {Id = 200, Code = "PW", Name="Palau", XBox = false},
            new Market() {Id = 201, Code = "PS", Name="Palestinian Authority", XBox = false},
            new Market() {Id = 202, Code = "PA", Name="Panama", XBox = false},
            new Market() {Id = 203, Code = "PG", Name="Papua New Guinea", XBox = false},
            new Market() {Id = 204, Code = "PY", Name="Paraguay", XBox = false},
            new Market() {Id = 65, Code = "PE", Name="Peru", XBox = false},
            new Market() {Id = 66, Code = "PH", Name="Philippines", XBox = false},
            new Market() {Id = 214, Code = "PN", Name="Pitcairn Islands", XBox = false},
            new Market() {Id = 67, Code = "PL", Name="Poland", XBox = true},
            new Market() {Id = 68, Code = "PT", Name="Portugal", XBox = true},
            new Market() {Id = 69, Code = "QA", Name="Qatar", XBox = false},
            new Market() {Id = 205, Code = "RE", Name="Réunion", XBox = false},
            new Market() {Id = 70, Code = "RO", Name="Romania", XBox = false},
            new Market() {Id = 23, Code = "RU", Name="Russia", XBox = true},
            new Market() {Id = 206, Code = "RW", Name="Rwanda", XBox = false},
            new Market() {Id = 207, Code = "BL", Name="Saint Barthélemy", XBox = false},
            new Market() {Id = 222, Code = "KN", Name="Saint Kitts and Nevis", XBox = false},
            new Market() {Id = 223, Code = "LC", Name="Saint Lucia", XBox = false},
            new Market() {Id = 208, Code = "MF", Name="Saint Martin", XBox = false},
            new Market() {Id = 224, Code = "PM", Name="Saint Pierre and Miquelon", XBox = false},
            new Market() {Id = 225, Code = "VC", Name="Saint Vincent and the Grenadines", XBox = false},
            new Market() {Id = 209, Code = "WS", Name="Samoa", XBox = false},
            new Market() {Id = 210, Code = "SM", Name="San Marino", XBox = false},
            new Market() {Id = 71, Code = "SA", Name="Saudi Arabia", XBox = true},
            new Market() {Id = 212, Code = "SN", Name="Senegal", XBox = false},
            new Market() {Id = 72, Code = "RS", Name="Serbia", XBox = false},
            new Market() {Id = 218, Code = "SC", Name="Seychelles", XBox = false},
            new Market() {Id = 219, Code = "SL", Name="Sierra Leone", XBox = false},
            new Market() {Id = 25, Code = "SG", Name="Singapore", XBox = true},
            new Market() {Id = 215, Code = "SX", Name="Sint Maarten", XBox = false},
            new Market() {Id = 73, Code = "SK", Name="Slovakia", XBox = true},
            new Market() {Id = 74, Code = "SI", Name="Slovenia", XBox = false},
            new Market() {Id = 216, Code = "SB", Name="Solomon Islands", XBox = false},
            new Market() {Id = 217, Code = "SO", Name="Somalia", XBox = false},
            new Market() {Id = 26, Code = "ZA", Name="South Africa", XBox = true},
            new Market() {Id = 220, Code = "GS", Name="South Georgia and South Sandwich Islands", XBox = false},
            new Market() {Id = 27, Code = "ES", Name="Spain", XBox = true},
            new Market() {Id = 75, Code = "LK", Name="Sri Lanka", XBox = false},
            new Market() {Id = 221, Code = "SH", Name="St Helena, Ascension, Tristan da Cunha", XBox = false},
            new Market() {Id = 247, Code = "SR", Name="Suriname", XBox = false},
            new Market() {Id = 163, Code = "SJ", Name="Svalbard", XBox = false},
            new Market() {Id = 28, Code = "SE", Name="Sweden", XBox = true},
            new Market() {Id = 29, Code = "CH", Name="Switzerland", XBox = true},
            new Market() {Id = 30, Code = "TW", Name="Taiwan", XBox = true},
            new Market() {Id = 226, Code = "TJ", Name="Tajikistan", XBox = false},
            new Market() {Id = 227, Code = "TZ", Name="Tanzania", XBox = false},
            new Market() {Id = 76, Code = "TH", Name="Thailand", XBox = false},
            new Market() {Id = 112, Code = "TL", Name="Timor-Leste", XBox = false},
            new Market() {Id = 228, Code = "TG", Name="Togo", XBox = false},
            new Market() {Id = 229, Code = "TK", Name="Tokelau", XBox = false},
            new Market() {Id = 230, Code = "TO", Name="Tonga", XBox = false},
            new Market() {Id = 77, Code = "TT", Name="Trinidad and Tobago", XBox = false},
            new Market() {Id = 78, Code = "TN", Name="Tunisia", XBox = false},
            new Market() {Id = 79, Code = "TR", Name="Turkey", XBox = true},
            new Market() {Id = 231, Code = "TM", Name="Turkmenistan", XBox = false},
            new Market() {Id = 232, Code = "TC", Name="Turks and Caicos Islands", XBox = false},
            new Market() {Id = 233, Code = "TV", Name="Tuvalu", XBox = false},
            new Market() {Id = 234, Code = "UM", Name="U.S. Outlying Islands", XBox = false},
            new Market() {Id = 236, Code = "VI", Name="U.S. Virgin Islands", XBox = false},
            new Market() {Id = 235, Code = "UG", Name="Uganda", XBox = false},
            new Market() {Id = 80, Code = "UA", Name="Ukraine", XBox = false},
            new Market() {Id = 81, Code = "AE", Name="United Arab Emirates", XBox = true},
            new Market() {Id = 31, Code = "GB", Name="United Kingdom", XBox = true},
            new Market() {Id = 3, Code = "US", Name="United States", XBox = true},
            new Market() {Id = 82, Code = "UY", Name="Uruguay", XBox = false},
            new Market() {Id = 244, Code = "UZ", Name="Uzbekistan", XBox = false},
            new Market() {Id = 245, Code = "VU", Name="Vanuatu", XBox = false},
            new Market() {Id = 246, Code = "VA", Name="Vatican City", XBox = false},
            new Market() {Id = 83, Code = "VE", Name="Venezuela", XBox = false},
            new Market() {Id = 84, Code = "VN", Name="Vietnam", XBox = false},
            new Market() {Id = 238, Code = "WF", Name="Wallis and Futuna", XBox = false},
            new Market() {Id = 240, Code = "YE", Name="Yemen", XBox = false},
            new Market() {Id = 241, Code = "ZM", Name="Zambia", XBox = false},
            new Market() {Id = 242, Code = "ZW", Name="Zimbabwe", XBox = false}
        };
    }

    /*public class Links
    {
        public string DatesLearnMore { get; set; }
        public string LimitedDistributionPDPLink { get; set; }
        public string LimitedDistributionForwardLink { get; set; }
        public string AdvancedLimitedDistributionForwardLink { get; set; }
        public string AddOnVisibilityForwardLink { get; set; }
        public string AddOnFutureMarketForwardLink { get; set; }
        public string AddOnDistributionWarningForwardLink { get; set; }
        public string StopSellingForwardLink { get; set; }
        public string FreeTrialForwardLink { get; set; }
        public string SalesMarketsForwardLink { get; set; }
        public string SalePricingLearnMore { get; set; }
        public string FreeToUseLearnMore { get; set; }
        public string MarketReleaseDateLearnMore { get; set; }
        public string OrganizationalLicensingOnlineForwardLink { get; set; }
        public string OrganizationalLicensingOfflineForwardLink { get; set; }
        public string PublishingDateLearnMore { get; set; }
        public string AppFutureMarket { get; set; }
        public string BackpublishGeneralLearnMore { get; set; }
        public string BackpublishScheduleLearnMore { get; set; }
        public string BackpublishPricingLearnMore { get; set; }
        public string PriceOverviewLearnMore { get; set; }
        public string AudienceLearnMore { get; set; }
        public string ProductSetupLearnMore { get; set; }
        public string DiscoverabilityLearnMore { get; set; }
        public string DirectLinkLearnMore { get; set; }
        public string PurchasableInParentAppLearnMore { get; set; }
        public string PurchasableInStoreLearnMore { get; set; }
        public string ProductLockedForConversion { get; set; }
        public string PnAAdvancedSettingsLink { get; set; }
    }
    */
    public class DateTime   
    {
    }

    public class Schedule
    {
        public string PriceTier { get; set; } = "Tier1";   
        public DateTime DateTime { get; set; } = new DateTime();
        public object OpenPrice { get; set; } = null;
        public bool IsOpenPrice { get; set; } = false;
    }

    public class PriceGroup
    {
        public bool IsDefault { get; set; } = true;
        public List<object> Markets { get; set; } = new List<object>();
        public List<Schedule> Schedules { get; set; } = new List<Schedule>() { new Schedule() };
        public string CurrencyCode { get; set; } = "USD";
    }

    public class Pricing
    {
        public List<PriceGroup> PriceGroups { get; set; } = new List<PriceGroup>() { new PriceGroup() };
    }

    public class Trial
    {
        public string TrialType { get; set; } = "NoTrial";
        public List<object> TrialGroups { get; set; } = new List<object>();
        public string EligibilityType { get; set; } = "Everyone";
        public object EligibilityId { get; set; } = null;
        public object TrialDuration { get; set; } = null;
    }

    public class APPVisibility
    {
        //should fiddle with this
        public string DistributionOption { get; set; } = "Retail";
        public string DistributionMode { get; set; } = "Public";
        public string Audience { get; set; } = "PrivateBeta";
        public string BetaAccounts { get; set; } = "";
        public object PrivateCatalog { get; set; } = null;
        public List<string> GroupIds { get; set; }
        public object TurnoffAvailabilitiesBeforeReleaseOrDiscoverableDate { get; set; } = null;
    }

    public class Licensing
    {
        public bool SupportEnterpriseOnline { get; set; } = true;
        public bool SupportEnterpriseOffline { get; set; } = false;
    }

    public class Dates
    {
        public List<object> DateGroups { get; set; } = new List<object>() { };
    }

    public class Deals
    {
        public List<object> Groups { get; set; } = new List<object>() { };
    }

    public class Display
    {
        public List<object> Groups { get; set; } = new List<object>() { };
    }

    public class Revenue
    {
        public bool IsRevenueSkuEnabled { get; set; } = false;
        public object LegacyRevenueSku { get; set; } = null;
        public object ThresholdRevenueSku { get; set; } = null;
    }

    public class AudienceSettings
    {
        public object PlanMemberLimits { get; set; } = null;
        public object OfferMemberLimits { get; set; } = null;
        public int PlanLimit { get; set; } = 0;
        public List<object> AllowedMemberTypes { get; set; } = new List<object>();
        public List<object> MemberTypesUINotSupported { get; set; } = new List<object>();
        public bool SupportsAadMsaId { get; set; } = false;
        public bool SupportsBackendSubscriptionId { get; set; } = false;
        public bool SupportsHideKey { get; set; } = false;
        public bool SupportsTenantId { get; set; } = false;
        public bool SupportsUISubscriptionId { get; set; } = false;
        public bool IsSupported { get; set; } = false;
    }
    /*
    public class PreviewAudienceSettings
    {
        public object PlanMemberLimits { get; set; } = null;
        public object OfferMemberLimits { get; set; } = null;
        public int PlanLimit { get; set; } = 0;
        public List<object> AllowedMemberTypes { get; set; } = new List<object>();
        public List<object> MemberTypesUINotSupported { get; set; } = new List<object>();
        public bool SupportsAadMsaId { get; set; } = false;
        public bool SupportsBackendSubscriptionId { get; set; } = false;
        public bool SupportsHideKey { get; set; } = false;
        public bool SupportsTenantId { get; set; } = false;
        public bool SupportsUISubscriptionId { get; set; } = false;
        public bool IsSupported { get; set; } = false;
    }

    public class PrivateAudienceSettings
    {
        public object PlanMemberLimits { get; set; }
        public object OfferMemberLimits { get; set; }
        public int PlanLimit { get; set; }
        public List<object> AllowedMemberTypes { get; set; }
        public List<object> MemberTypesUINotSupported { get; set; }
        public bool SupportsAadMsaId { get; set; }
        public bool SupportsBackendSubscriptionId { get; set; }
        public bool SupportsHideKey { get; set; }
        public bool SupportsTenantId { get; set; }
        public bool SupportsUISubscriptionId { get; set; }
        public bool IsSupported { get; set; }
    }*/

    public class TrialSettings
    {
        public List<object> SupportedMonths { get; set; } = new List<object>();
        public bool IsSupported { get; set; } = false;
    }

    public class ValidationSetting
    {
        public bool ValidationEnabled { get; set; } = false;
        public bool IncludeDeletedSkuWhenValidation { get; set; } = false;
        public object RuleSettings { get; set; } = null;
    }

    public class PublishingRecordSettings
    {
        public string PriceTrackingMode { get; set; } = "Untracked";
        public object StoredPriceScheduleVersion { get; set; } = null;
        public bool LookupOnlyGoLiveSubmissions { get; set; } = false;
        public bool AlwaysUpdateToLatestSubmissionOnGet { get; set; } = false;
        public bool UseLightWeightSubmissions { get; set; } = true;
    }

    public class FeatureAvailabilityStorageSettings
    {
        public bool UsesComplexPriceInV0 { get; set; } = false;
        public bool IsV1Applicable { get; set; } = true;
        public object DefaultGetIfVersionNotSpecified { get; set; } = null;
        public bool IsV3Applicable { get; set; } = false;
    }

    public class ConversionSettings
    {
        public bool HasShadowSubmission { get; set; } = true;
        public bool IgnoreVariantSkuAlignmentCheck { get; set; } = false;
    }

    public class EnabledCloudMarketSets
    {
        public List<int> Default { get; set; } = new List<int>() { 3, 32, 33, 10, 11, 34, 147, 12, 13, 35, 14, 36, 15, 37, 38, 39, 40, 41, 42, 43, 44, 45, 8, 7, 46, 136, 17, 47, 144, 6, 48, 85, 18, 49, 50, 4, 51, 52, 165, 53, 54, 55, 171, 57, 20, 58, 59, 179, 21, 60, 61, 22, 197, 62, 63, 64, 65, 66, 67, 68, 69, 70, 23, 71, 72, 25, 73, 74, 26, 19, 27, 28, 29, 30, 76, 77, 78, 79, 80, 81, 31, 84, 240, 56, 75, 82, 83, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 137, 138, 139, 140, 141, 142, 143, 145, 146, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 166, 167, 168, 169, 170, 172, 173, 174, 175, 176, 177, 178, 180, 181, 182, 183, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 241, 242, 244, 245, 247, 248, 90, 184, 210, 243, 246 };
    }

    public class CloudMarketSettings
    {
        public EnabledCloudMarketSets EnabledCloudMarketSets { get; set; } = new EnabledCloudMarketSets();
        public List<int> AllowedMarketSet { get; set; } = new List<int>() { 3, 32, 33, 10, 11, 34, 147, 12, 13, 35, 14, 36, 15, 37, 38, 39, 40, 41, 42, 43, 44, 45, 8, 7, 46, 136, 17, 47, 144, 6, 48, 85, 18, 49, 50, 4, 51, 52, 165, 53, 54, 55, 171, 57, 20, 58, 59, 179, 21, 60, 61, 22, 197, 62, 63, 64, 65, 66, 67, 68, 69, 70, 23, 71, 72, 25, 73, 74, 26, 19, 27, 28, 29, 30, 76, 77, 78, 79, 80, 81, 31, 84, 240, 56, 75, 82, 83, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 137, 138, 139, 140, 141, 142, 143, 145, 146, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 166, 167, 168, 169, 170, 172, 173, 174, 175, 176, 177, 178, 180, 181, 182, 183, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 241, 242, 244, 245, 247, 248, 90, 184, 210, 243, 246 };
    }

    public class ForwardLinks
    {
        public int MeteredBilling { get; set; } = 0;
        public int Pricing { get; set; } = 0;
        public int PricingCurrencyConversion { get; set; } = 0;
        public int PrivateAudience { get; set; } = 0;
    }

    public class PageIntro 
    {
        public int ForwardLinkId { get; set; } = 0;
        public object Intro { get; set; } = null;
        public object LinkText { get; set; } = null;
    }

    /*public class OfferPageIntro
    {
        public int ForwardLinkId { get; set; } = 0;
        public object Intro { get; set; } = null;
        public object LinkText { get; set; } = null;
    }

    public class PlanPageIntro
    {
        public int ForwardLinkId { get; set; } 
        public object Intro { get; set; }
        public object LinkText { get; set; }
    }*/

    public class AvailabilityConfig
    {
        public bool IsModernMarketplaceProduct { get; set; } = false;
        public bool SupportsDefaultFeatureAvailability { get; set; } = true;
        public bool SupportsDefaultFreePrice { get; set; } = false;
        public bool SupportsZeroFeatureAvailability { get; set; } = false;
        public bool ReturnFeatureAvailabilityModuleInstance { get; set; } = false;
        public bool ReturnDefaultFeatureAvailabilityModuleInstance { get; set; } = false;
        public string UIPlatformVersion { get; set; } = "AngularJs";
        public string DomainDataModel { get; set; } = "Default";
        public bool SupportsPricingModel { get; set; } = false;
        public bool SupportsRecurringPrice { get; set; } = false;
        public bool SupportsMonthlyBillingTerm { get; set; } = false;
        public bool SupportsAnnualBillingTerm { get; set; } = false;
        public AudienceSettings PreviewAudienceSettings { get; set; } = new AudienceSettings();
        public AudienceSettings PrivateAudienceSettings { get; set; } = new AudienceSettings();
        //change?
        public bool SkipMarketValidationApplicable { get; set; } = true;
        public bool SkipCloudValidationApplicable { get; set; } = false;
        public bool AzurePricingValidationApplicable { get; set; } = false;
        public bool SupportsHidePlan { get; set; } = false;
        public bool SupportsMarketsSelection { get; set; } = false;
        public bool SupportsPreorder { get; set; } = true;
        public TrialSettings TrialSettings { get; set; } = new TrialSettings();
        public bool SupportsPackages { get; set; } = true;
        public bool SupportsClear { get; set; } = false;
        public bool SupportsEnhancedModuleStatus { get; set; } = false;
        public bool ClearHidePlan { get; set; } = false;
        public bool ClearRecurringPrice { get; set; } = false;
        public bool SupportsAzureOverviewSummary { get; set; } = false;
        public bool FilterOutDisabledMarkets { get; set; } = false;
        public bool AllowPrivateAudienceNone { get; set; } = false;
        public bool IsSupportedByPcs { get; set; } = true;
        public bool SupportsLegacySkuFiltering { get; set; } = false;
        public bool HasSubmissionWithLinks { get; set; } = false;
        public bool SupportsVariantAvailabilityLockedPrices { get; set; } = false;
        public bool SupportsVariants { get; set; } = false;
        public bool SupportsStopSellingMarkets { get; set; } = false;
        public bool SupportsCustomMeters { get; set; } = false;
        public int MaxCustomMeters { get; set; } = 0;
        public bool AutogenerateStopSoldPricesForNewCadences { get; set; } = false;
        public string DefaultMarketEnableStatus { get; set; } = "EnableAll";
        public string DefaultSubGeoEnableStatus { get; set; } = "DisableAll";
        public bool UsesLockV2 { get; set; } = false;
        public object DefaultApplicableClouds { get; set; } = null;
        public bool SupportsValidationEngine { get; set; } = false;
        public ValidationSetting ValidationSetting { get; set; } = new ValidationSetting();
        public PublishingRecordSettings PublishingRecordSettings { get; set; } = new PublishingRecordSettings();
        public FeatureAvailabilityStorageSettings FeatureAvailabilityStorageSettings { get; set; } = new FeatureAvailabilityStorageSettings();
        public ConversionSettings ConversionSettings { get; set; } = new ConversionSettings();
        public bool HasAvailabilityInstances { get; set; } = true;
        public bool SupportsComputeHashCode { get; set; } = false;
        public int HashCodeAlgorithmVersion { get; set; } = 0;
        public bool HashVariantsWithProduct { get; set; } = true;
        public bool CreateDefaultPublishingRecord { get; set; } = true;
        public bool SupportsMultiSku { get; set; } = false;
        public bool SupportsProductLevelMarkets { get; set; } = false;
        public bool SupportsMarketCode { get; set; } = false;
        public bool SupportsLegacyMarketsSettings { get; set; } = true;
        public bool SupportsMigrateAdvanceProducts { get; set; } = false;
        public bool RoundFeatureAvailabilityPrice { get; set; } = false;
        public bool SupportsSeats { get; set; } = false;
        public bool SupportsEligibilityStatus { get; set; } = true;
        public bool SupportsComputingRecordInVariantAvailability { get; set; } = false;
        public bool VisualizeScheduleEnabled { get; set; } = false;
        public bool SupportsReseller { get; set; } = false;
        public bool SupportsResellerAzureApp { get; set; } = false;
        public object SupportsResellerSelections { get; set; } = null;
        //might have to change this
        public bool EnableAllMarkets { get; set; } = true;
        public bool SupportsConsultingServiceExpandedMarkets { get; set; } = false;
        public bool SupportsBusinessCentralExpandedMarkets { get; set; } = false;
        public CloudMarketSettings CloudMarketSettings { get; set; } = new CloudMarketSettings();
        public bool ShowUIValidationsOnFirstLoad { get; set; } = false;
        public bool SkipAccountDataInSummary { get; set; } = false;
        public bool SupportsCompareFeature { get; set; } = false;
        public bool IsFreePlayDaysEnabled { get; set; } = true;
        public bool IsPrivateAudienceAdvanceSchedulesEnabled { get; set; } = false;
        public bool MigrationBugFix { get; set; } = false;
        public int AzurePricingUsdUpperLimit { get; set; } = 0;
        public bool SupportsNewMigrationConverter { get; set; } = false;
        public bool SupportsAvailabilityModule { get; set; } = true;
        public bool IsImageAvailabilitySupported { get; set; } = false;
        public bool SupportsPercentagePricing { get; set; } = false;
        public bool SupportsCorePricing { get; set; } = false;
        public string DefaultChannelSelection { get; set; } = "NotSet";
        public bool DisableResellerSelection { get; set; } = false;
        public bool SupportsListingPrice { get; set; } = false;
        public bool SupportsStatesAndProvinces { get; set; } = false;
        public bool SupportsTaxRemit { get; set; } = false;
        public bool SupportsResellerStitch { get; set; } = false;
        public bool OnlyAllowAzureConsumptionRate { get; set; } = false;
        public bool OnlyAllowByol { get; set; } = false;
        public bool SkipPermissionCheck { get; set; } = false;
        public long SupportNumberAfterDecimalPoint { get; set; } = 2147483647;
        public bool DefaultPriceOnly { get; set; } = false;
        public bool SupportsNewMigrationAudienceNaming { get; set; } = false;
        public object FreePriceMarket { get; set; } = null;
        public bool SupportsIotPricing { get; set; } = false;
        public ForwardLinks ForwardLinks { get; set; } = new ForwardLinks();
        public bool IgnorePricingUnitsForLockedPrices { get; set; } = true;
        public bool IgnoreMergeMigratedSubmissionAndPublishingRecord { get; set; } = false;
        public bool UppercaseHideKeyAllowed { get; set; } = false;
        public bool ValidCurrencySettingForPricing { get; set; } = false;
        public PageIntro OfferPageIntro { get; set; } = new PageIntro();
        public PageIntro PlanPageIntro { get; set; } = new PageIntro();
        public bool SupportsNewCoreTest { get; set; } = false;
        public bool SupportsNewCorePreGa { get; set; } = false;
        public bool CanFixCorePrice { get; set; } = false;
        public bool SupportsHideWVD { get; set; } = false;
        public bool CanConvertToOpenPriceMode { get; set; } = false;
        public bool DelayAvailabilityLock { get; set; } = false;
        public bool DelayResellerLock { get; set; } = false;
        public object ReferenceVariantId { get; set; } = null;
        public bool SupportsBillingTag { get; set; } = false;
        public bool CreateNewFavAsNewOpenPrice { get; set; } = false;
        public bool SupportsMultiMarketPublishing { get; set; } = false;
        public object PageAlias { get; set; } = null;
        public bool AudienceDeduplication { get; set; } = false;
        public bool AudienceDoNotUseBulkUpdate { get; set; } = false;
    }

    public class StoreInfo
    {
        public object ParentBigId { get; set; } = null;
        public string ProductType { get; set; } = "App";
        public string SubType { get; set; } = "NotSet";
        public object BranchId { get; set; } = null;
        public object BranchName { get; set; } = null;
        public object ProductId { get; set; } = null;
        public string ProductName { get; set; }
        public object InstanceId { get; set; } = null;
        //fiddle with this
        public string SandboxId { get; set; } = "RETAIL";
        public object SkuId { get; set; } = null;
        //change this to submit I thingk
        public bool IsEditable { get; set; } = true;
        public bool IsContentIsolationEnabled { get; set; } = false;
        //wonder what this does
        public bool IsAdvancedFeatureEnabled { get; set; } = false;
        public bool IsResellerPricing { get; set; } = false;
        public bool IsResellerSalesEnabled { get; set; } = false;
        public bool HasMultipleSkus { get; set; } = false;
        public bool IsInPreview { get; set; } = false;
        public bool HasEnterpriseAccess { get; set; } = true;
        public bool IsPriceReviewRequired { get; set; } = false;
        public bool HasDcaPayload { get; set; } = false;
        public bool EnableFutureMarkets { get; set; } = true;
        public bool IsOpenPriceEnabled { get; set; } = true;
        public int DomainDataVersion { get; set; } = 1;
        public bool IsSpecialPricingEnabled { get; set; } = false;
        public bool CanHaveEarlyUnlock { get; set; } = false;
        public object TurnoffAvailabilitiesBeforeReleaseOrDiscoverableDate { get; set; } = null;
        public List<object> AvailabilityErrors { get; set; } = new List<object>();
        public object ArcWarnings { get; set; } = null;
        public DomainData DomainData { get; set; } = new DomainData();
        //public Links Links { get; set; }
        public Pricing Pricing { get; set; } = new Pricing();
        public object PricingV2 { get; set; } = null;
        public Trial Trial { get; set; } = new Trial();
        public APPVisibility Visibility { get; set; } = new APPVisibility();
        public object VisibilityV2 { get; set; } = null;
        public Licensing Licensing { get; set; } = new Licensing();
        public Dates Dates { get; set; } = new Dates();
        public Deals Deals { get; set; } = new Deals();
        public Display Display { get; set; } = new Display();
        public Revenue Revenue { get; set; } = new Revenue();
        public object HashCode { get; set; } = null;
        public object CertHashCode { get; set; } = null;
        public string BigId { get; set; }
        public bool IsJaguar { get; set; } = false;
        public object PublishedDateTime { get; set; } = null;
        public object PublishedDate { get; set; } = null;
        public object PublishedTime { get; set; } = null;
        public bool IsValid { get; set; } = false;
        public int PublisherId { get; set; }
        public AvailabilityConfig AvailabilityConfig { get; set; } = new AvailabilityConfig();
        public bool NewOpenPriceMode { get; set; } = false;
        public List<int> EnabledMarkets { get; set; } = new List<int>() { 3, 32, 33, 10, 11, 34, 147, 12, 13, 35, 14, 36, 15, 37, 38, 39, 40, 41, 42, 43, 44, 45, 8, 7, 46, 136, 17, 47, 144, 6, 48, 85, 18, 49, 50, 4, 51, 52, 165, 53, 54, 55, 171, 57, 20, 58, 59, 179, 21, 60, 61, 22, 197, 62, 63, 64, 65, 66, 67, 68, 69, 70, 23, 71, 72, 25, 73, 74, 26, 19, 27, 28, 29, 30, 76, 77, 78, 79, 80, 81, 31, 84, 240, 56, 75, 82, 83, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 137, 138, 139, 140, 141, 142, 143, 145, 146, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 166, 167, 168, 169, 170, 172, 173, 174, 175, 176, 177, 178, 180, 181, 182, 183, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 241, 242, 244, 245, 247, 248, 90, 184, 210, 243, 246 };
    }


}

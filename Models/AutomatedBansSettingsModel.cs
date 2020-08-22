using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TCAdminBanManagement.Models
{
    public class AutomatedBansSettingsModel
    {
        [Display(Description = "Enable Automatic Bans for failed login attempts", Name = "Enable automatic banning?")]
        public bool Enabled { get; set; } = true;
        
        [Display(Description = "Maximum attempts before the system will ban the IP address", Name = "Maximum Attempts")]
        public int MaxAttempts { get; set; } = 4;

        [Display(Description = "How long to ban the IP Address after it has reached the maximum attempts (0 for indefinitely)", Name = "Ban Length")]
        public int BanForMinutes { get; set; } = 5;
        
        [Display(Description = "How long inbetween attempts (0 to disable)", Name = "Minutes Inbetween Attempts")]
        public int MinutesInbetween { get; set; } = 5;
        
        [Display(Description = "The reason that will be used when banning", Name = "Ban Reason")]
        public string BanReason { get; set; } = "[Ban Management] You have been automatically banned for multiple login attempts.";

        public static AutomatedBansSettingsModel Get()
        {
            var autoBansString = TCAdmin.SDK.Utility.GetDatabaseValue("BanManagement.AutoBans");
            return !string.IsNullOrEmpty(autoBansString)
                ? JsonConvert.DeserializeObject<AutomatedBansSettingsModel>(autoBansString)
                : new AutomatedBansSettingsModel();
        }

        public static void Set(AutomatedBansSettingsModel model)
        {
            TCAdmin.SDK.Utility.SetDatabaseValue("BanManagement.AutoBans", JsonConvert.SerializeObject(model));
        }
    }
}
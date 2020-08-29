using System;
using System.ComponentModel.DataAnnotations;
using TCAdminBanManagement.Models.Objects;

namespace TCAdminBanManagement.Models
{
    public class BannedIpViewModel
    {
        public int Id { get; set; }
        
        public string IpAddress { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public DateTime ExpiresAt { get; set; }
        
        public string Reason { get; set; }
        
        public EBanType BanType { get; set; }

        public static BannedIpViewModel Map(BannedIp bannedIp)
        {
            return new BannedIpViewModel
            {
                Id = bannedIp.Id,
                IpAddress = bannedIp.IpAddress,
                ExpiresAt = bannedIp.ExpiresAt.ToUniversalTime(),
                Reason = bannedIp.Reason,
                BanType = bannedIp.BanType
            };
        }
    }
}
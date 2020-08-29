using TCAdmin.SDK;
using TCAdminBanManagement.Models.Objects;

namespace TCAdminBanManagement.Models
{
    public enum EBanType
    {
        Web,
        Ftp,
        All
    }

    public static class BanTypeHelper
    {
        public static bool IsBannedFrom(EBanType eBanType)
        {
            var banInfo = BannedIp.GetBan(Utility.GetWebClientIp());
            return IsBannedFrom(banInfo.BanType, eBanType);
        }
        
        public static bool IsBannedFrom(EBanType hasEBan, EBanType bannedFrom)
        {
            return hasEBan == EBanType.All || hasEBan == bannedFrom;
        }
    }
}
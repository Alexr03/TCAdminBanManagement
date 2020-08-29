using System;
using System.Net;
using TCAdmin.SDK.Integration;
using TCAdminBanManagement.Models;
using TCAdminBanManagement.Models.Objects;

namespace TCAdminBanManagement.Events
{
    public class LoginProtection : CommandBase
    {
        public override CommandResponse ProcessCommand(object sender, IntegrationEventArgs args)
        {
            BannedIp.UnbanExpired();
            if (!(args.Arguments[0] is IPAddress ipAddress))
            {
                return new CommandResponse(Globals.ModuleId, ReturnStatus.Ok);
            }
            
            var authenticationSuccess = bool.Parse(args.Arguments[1].ToString());
            if (!authenticationSuccess)
            {
                new BannedIp
                {
                    IpAddress = ipAddress.ToString(),
                    Reason = "Failed Login Attempt",
                }.ExecuteModuleCommand("FailedLogin");
            }

            if (!BannedIp.IsIpBanned(ipAddress.ToString()))
            {
                return new CommandResponse(Globals.ModuleId, ReturnStatus.Ok);
            }
            
            var bannedIp = BannedIp.GetBan(ipAddress.ToString());
            if (args.Command == "FtpLogin")
            {
                if (BanTypeHelper.IsBannedFrom(bannedIp.BanType, EBanType.Ftp))
                {
                    bannedIp.ExecuteModuleCommand("AttemptedAccess", new {BanType = EBanType.Ftp});
                    throw new Exception($"IP {ipAddress} is banned for: {bannedIp.Reason} || Expiry (UTC): {bannedIp.ExpiresAt:F}");
                }
            }
            else if (args.Command == "PanelLogin")
            {
                if (BanTypeHelper.IsBannedFrom(bannedIp.BanType, EBanType.Web))
                {
                    bannedIp.ExecuteModuleCommand("AttemptedAccess", new {BanType = EBanType.Web});
                    throw new Exception($"IP {ipAddress} is banned for: {bannedIp.Reason} || Expiry (UTC): {bannedIp.ExpiresAt:F}");
                }
            }
            
            return new CommandResponse(Globals.ModuleId, ReturnStatus.Ok);
        }
    }
}
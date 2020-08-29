using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetTools;
using TCAdmin.Interfaces.Database;
using TCAdmin.SDK.Objects;

namespace TCAdminBanManagement.Models.Objects
{
    public class BannedIp : ObjectBase
    {
        public static DateTime DefaultDateTime = new DateTime(599266080000000000L);

        public BannedIp()
        {
            this.TableName = "tcmodule_banned_ips";
            this.KeyColumns = new[] {"id"};
            this.UseApplicationDataField = true;
            this.EnableCacheForObjectList = false;
            this.SetValue("id", -1);
        }

        public BannedIp(int id) : this()
        {
            this.SetValue("id", id);
            this.ValidateKeys();
            if (!this.Find())
            {
                throw new Exception("Cannot find banned IP with ID " + id);
            }
        }

        public int Id
        {
            get => this.GetIntegerValue("id");
            internal set => this.SetValue("id", value);
        }

        public string IpAddress
        {
            get => this.GetStringValue("ipAddress");
            set => this.SetValue("ipAddress", value);
        }

        public string Reason
        {
            get => this.GetStringValue("reason");
            set => this.SetValue("reason", value);
        }
        
        public EBanType EBanType
        {
            get => (EBanType)this.GetIntegerValue("banType");
            set => this.SetValue("banType", (int)value);
        }

        public DateTime ExpiresAt
        {
            get => this.GetDateTimeValue("expiresAt").ToUniversalTime();
            set => this.SetValue("expiresAt", value);
        }

        public static List<BannedIp> GetBannedIps()
        {
            return new BannedIp().GetObjectList(new WhereList()).Cast<BannedIp>().ToList();
        }

        public static bool IsIpBanned(string ipAddress)
        {
            var bannedIps = GetBannedIps();
            foreach (var bannedIp in bannedIps)
            {
                if (!IPAddressRange.TryParse(bannedIp.IpAddress, out var ipRange)) continue;
                if (ipRange.Contains(IPAddress.Parse(ipAddress)))
                {
                    return true;
                }
            }

            return false;
        }

        public static BannedIp GetBan(string ipAddress)
        {
            var bannedIps = GetBannedIps();
            foreach (var bannedIp in bannedIps)
            {
                if (!IPAddressRange.TryParse(bannedIp.IpAddress, out var ipRange)) continue;
                if (ipRange.Contains(IPAddress.Parse(ipAddress)))
                {
                    return bannedIp;
                }
            }

            return null;
        }

        public static void UnbanExpired()
        {
            var bannedIps = GetBannedIps();
            foreach (var bannedIp in bannedIps.Where(bannedIp =>
                bannedIp.ExpiresAt != DefaultDateTime && bannedIp.ExpiresAt.Year != 0001 &&
                DateTime.Compare(bannedIp.ExpiresAt, DateTime.UtcNow) < 0))
            {
                bannedIp.Delete();
            }
        }

        public static BannedIp Map(BannedIpViewModel model)
        {
            return new BannedIp
            {
                Id = model.Id,
                IpAddress = model.IpAddress,
                Reason = model.Reason ?? string.Empty,
                ExpiresAt = model.ExpiresAt.ToUniversalTime(),
                EBanType = EBanType.All
            };
        }

        public void UpdateWith(BannedIpViewModel model)
        {
            Id = model.Id;
            IpAddress = model.IpAddress;
            Reason = model.Reason ?? "";
            ExpiresAt = model.ExpiresAt.ToUniversalTime();
            EBanType = model.EBanType;
        }
    }
}
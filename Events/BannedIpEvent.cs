using System;
using System.Linq;
using TCAdmin.Interfaces.Logging;
using TCAdmin.SDK;
using TCAdmin.SDK.Integration;
using TCAdminBanManagement.Models;
using TCAdminBanManagement.Models.Objects;

namespace TCAdminBanManagement.Events
{
    public class BannedIpEvent : CommandBase
    {
        public override CommandResponse ProcessCommand(object sender, IntegrationEventArgs args)
        {
            var banInfo = (BannedIp) sender;
            switch (args.Command)
            {
                case "AttemptedAccess":
                    LogManager.WriteToLog($"{args.Command}",
                        $"{banInfo.IpAddress} attempted to access {args.Arguments[0]}", true,
                        LogType.Information, "BanManagement");
                    break;
                case "FailedLogin":
                {
                    var autoBanSettings = AutomatedBansSettingsModel.Get();
                    if (!autoBanSettings.Enabled)
                    {
                        return new CommandResponse(Globals.ModuleId, ReturnStatus.Ok);
                    }

                    Globals.FailedAttempts.Add(new Tuple<DateTime, string>(DateTime.UtcNow, banInfo.IpAddress));
                    var pastLoginAttempts = Globals.FailedAttempts.Where(x => x.Item2 == banInfo.IpAddress).ToList();
                    if (pastLoginAttempts.Count > autoBanSettings.MaxAttempts)
                    {
                        if (autoBanSettings.MinutesInbetween != 0)
                        {
                            autoBanSettings.MinutesInbetween = int.MaxValue;
                        }

                        if (pastLoginAttempts.All(x =>
                            x.Item1.CompareTo(DateTime.UtcNow.AddMinutes(autoBanSettings.MinutesInbetween)) < 0))
                        {
                            try
                            {
                                banInfo = new BannedIp
                                {
                                    IpAddress = banInfo.IpAddress,
                                    Reason = autoBanSettings.BanReason,
                                    EBanType = EBanType.All,
                                    ExpiresAt = autoBanSettings.BanForMinutes == 0
                                        ? DateTime.MinValue.ToUniversalTime()
                                        : DateTime.UtcNow.AddMinutes(autoBanSettings.BanForMinutes)
                                };
                                banInfo.GenerateKey();
                                banInfo.Save();
                                Globals.FailedAttempts.RemoveAll(x => x.Item2 == banInfo.IpAddress);
                            }
                            catch (Exception e)
                            {
                                LogManager.WriteToLog(args.Command, $"Could not ban {banInfo.IpAddress} - {e}", true, LogType.Console, "BanManagement");
                                throw;
                            }
                        }
                    }
                    break;
                }
            }

            return new CommandResponse(Globals.ModuleId, ReturnStatus.Ok);
        }
    }
}
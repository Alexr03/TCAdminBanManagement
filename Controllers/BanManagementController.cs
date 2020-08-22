using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TCAdmin.SDK.Objects;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdminBanManagement.Models;
using TCAdminBanManagement.Models.Objects;
using Utility = TCAdmin.SDK.Utility;

namespace TCAdminBanManagement.Controllers
{
    public class BanManagementController : BaseController
    {
        public ActionResult Index()
        {
            BannedIp.UnbanExpired();
            return View();
        }

        public ActionResult AutomatedBans()
        {
            return View(AutomatedBansSettingsModel.Get());
        }
        
        [HttpPost]
        public ActionResult AutomatedBans(AutomatedBansSettingsModel model)
        {
            AutomatedBansSettingsModel.Set(model);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Banned()
        {
            BannedIp.UnbanExpired();
            return View();
        }

        public ActionResult BannedIpsSet([DataSourceRequest] DataSourceRequest request, BannedIpViewModel model)
        {
            BannedIp bannedIp;
            if (model.Id != 0)
            {
                bannedIp = new BannedIp(model.Id);
                bannedIp.UpdateWith(model);
                bannedIp.Save();
                bannedIp.ExecuteModuleCommand("BannedIp");
            }
            else
            {
                bannedIp = BannedIp.Map(model);
                bannedIp.GenerateKey();
                bannedIp.Save();
            }
            
            return Json(new[] {model}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult BannedIpsDestroy([DataSourceRequest] DataSourceRequest request, BannedIpViewModel model)
        {
            var banInfo = new BannedIp(model.Id);
            banInfo.Delete();

            return Json(new[] {model}.ToDataSourceResult(request, ModelState));
        }

        public ActionResult BannedIpsRead([DataSourceRequest] DataSourceRequest request)
        {
            BannedIp.UnbanExpired();
            var bannedIps = BannedIp.GetBannedIps();
            return Json(bannedIps.Select(BannedIpViewModel.Map).ToDataSourceResult(request));
        }

        public static bool IsBanned()
        {
            var webClientIp = Utility.GetWebClientIp();
            return BannedIp.IsIpBanned(webClientIp);
        }

        public static bool IsBanned(User user)
        {
            var ipAddress = user.LastLoginIp;
            return BannedIp.IsIpBanned(ipAddress);
        }
    }
}
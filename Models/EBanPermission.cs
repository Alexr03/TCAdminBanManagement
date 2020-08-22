using TCAdmin.SDK.Objects;
using TCAdmin.SDK.Security;

namespace TCAdminBanManagement.Models
{
    public enum EBanPermission
    {
        ViewBans = 1,
        AddBan,
        RemoveBan,
        ManageAutoBans
    }

    public static class PermissionHelper
    {
        public static bool CurrentUserHasPermission(EBanPermission permission)
        {
            var user = TCAdmin.SDK.Session.GetCurrentUser();
            return user.UserType == UserType.Admin ||
                   SecurityManager.CurrentUserHasPermission(Globals.ModuleId, (int) permission);
        }
    }
}
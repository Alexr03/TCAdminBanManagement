using System;
using System.Collections.Generic;

namespace TCAdminBanManagement
{
    public static class Globals
    {
        public const string ModuleId = "918250d5-e6d5-4398-9434-44b0a17cd231";

        public static readonly List<Tuple<DateTime, string>> FailedAttempts = new List<Tuple<DateTime, string>>();
    }
}
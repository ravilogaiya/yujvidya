using System;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace yujvidya
{
    public class HangfireNoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
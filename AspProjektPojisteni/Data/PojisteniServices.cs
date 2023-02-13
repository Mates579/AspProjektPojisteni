using AspProjektPojisteni.Triggers;
using Microsoft.EntityFrameworkCore;

namespace AspProjektPojisteni.Data
{
    public class PojisteniServices
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseTriggers(triggerOptions =>
                {
                    triggerOptions
                    .AddTrigger<PolicyholderTrigger>()
                    .AddTrigger<InsuranceEventTrigger>();
                });
            });
            return services;
        }
    }
}

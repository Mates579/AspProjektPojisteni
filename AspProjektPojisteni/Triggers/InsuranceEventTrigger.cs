using AspProjektPojisteni.Data;
using AspProjektPojisteni.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;

namespace AspProjektPojisteni.Triggers
{
    public class InsuranceEventTrigger : IBeforeSaveTrigger<InsuranceEvent>
    {
        private readonly ApplicationDbContext db;

        public async Task BeforeSave(ITriggerContext<InsuranceEvent> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                InsuranceEvent insuranceEvent = context.Entity;

                Insurance insurance = await db.Insurance.FirstOrDefaultAsync(i => i.ID == insuranceEvent.InsuranceID);

                if (insuranceEvent.Payout! > insurance.InsuranceRate)
                {
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}

using AspProjektPojisteni.Data;
using AspProjektPojisteni.Models;
using EntityFrameworkCore.Triggered;
using System.Text;

namespace AspProjektPojisteni.Triggers
{
    public class PolicyholderTrigger : IBeforeSaveTrigger<Policyholder>
    {
        private readonly ApplicationDbContext db;
        public Task BeforeSave(ITriggerContext<Policyholder> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                Policyholder policyholder = context.Entity;

                policyholder.FullName = new StringBuilder()
                    .Append(policyholder.DegreeBefore)
                    .Append(" ")
                    .Append(policyholder.FirstName)
                    .Append(" ")
                    .Append(policyholder.LastName)
                    .Append(" ")
                    .Append(policyholder.DegreeAfter)
                    .ToString()
                    .Trim();
            }
            return Task.CompletedTask;
        }
    }
}

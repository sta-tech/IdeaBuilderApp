using MediatR;
using System.Linq;
using System.Threading.Tasks;

using Ideas.Domain.Core;

namespace Ideas.Infrastructure
{
    internal static class MediatorExtension
    {
        internal static async Task DispatchDomainEventsAsync(this IMediator mediator, IdeasContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
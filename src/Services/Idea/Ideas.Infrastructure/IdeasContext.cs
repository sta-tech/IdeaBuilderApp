using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Ideas.Domain.AggregatesModel;
using Ideas.Domain.Core;
using Ideas.Infrastructure.EntityConfigurations;

namespace Ideas.Infrastructure
{
    public class IdeasContext
        : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public const string DEFAULT_SCHEMA = "ideas";
        public DbSet<Idea> Ideas { get; set; }

        public IdeasContext(DbContextOptions<IdeasContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdeaEntityTypeConfiguration());
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

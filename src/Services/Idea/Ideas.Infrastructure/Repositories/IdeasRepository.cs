using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ideas.Domain.AggregatesModel;
using Ideas.Domain.Core;

namespace Ideas.Infrastructure.Repositories
{
    public class IdeasRepository
    {
        private readonly IdeasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public IdeasRepository(IdeasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Idea Add(Idea idea)
        {
            return  _context.Ideas.Add(idea).Entity;
        }

        public async Task<Idea> GetAsync(Guid ideaId)
        {
            var idea = await _context.Ideas.FirstOrDefaultAsync(o => o.Id == ideaId);
            if (idea == null)
            {
                idea = _context.Ideas.Local.FirstOrDefault(o => o.Id == ideaId);
            }
            return idea;
        }

        public async Task<IEnumerable<Idea>> GetAllAsync()
        {
            var ideas = await _context.Ideas.ToListAsync();
            return ideas;
        }
    }
}

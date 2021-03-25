using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ideas.API.Models;

namespace Ideas.API.Application.Queries
{
    public interface IIdeaQueries
    {
        Task<IEnumerable<IdeaSummaryViewModel>> GetIdeasAsync();
    }
}
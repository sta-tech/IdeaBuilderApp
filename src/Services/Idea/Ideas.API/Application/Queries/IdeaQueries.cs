using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ideas.API.Models;
using Ideas.Domain.AggregatesModel;
using Ideas.Infrastructure.Repositories;

namespace Ideas.API.Application.Queries
{
    public class IdeaQueries: IIdeaQueries
    {
        private readonly IdeasRepository _ideasRepository;

        public IdeaQueries(IdeasRepository ideasRepository)
        {
            _ideasRepository = ideasRepository;
        }

        public async Task<IEnumerable<IdeaSummaryViewModel>> GetIdeasAsync()
        {
            var ideas = await _ideasRepository.GetAllAsync();
            return ideas.Select(MapModel).ToList();
        }

        private IdeaSummaryViewModel MapModel(Idea idea)
        {
            var viewModel = new IdeaSummaryViewModel();

            viewModel.Id = idea.Id;
            viewModel.CreatedAt = idea.CreatedAt;
            viewModel.Title = idea.Title;
            viewModel.Description = idea.Description;

            return viewModel;
        }
    }
}
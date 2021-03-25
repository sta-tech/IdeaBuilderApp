using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Ideas.API.Application.Queries;
using Ideas.API.Models;

namespace Ideas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdeasController : ControllerBase
    {
        private readonly IIdeaQueries _ideaQueries;
        private readonly ILogger<IdeasController> _logger;

        public IdeasController(IIdeaQueries ideaQueries, ILogger<IdeasController> logger)
        {
            _ideaQueries = ideaQueries;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdeaSummaryViewModel>>> Get()
        {
            var ideas = await _ideaQueries.GetIdeasAsync();
            return Ok(ideas);
        }
    }
}

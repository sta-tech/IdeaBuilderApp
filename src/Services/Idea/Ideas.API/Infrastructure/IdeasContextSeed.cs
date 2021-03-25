using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Ideas.Infrastructure;

namespace Ideas.API.Infrastructure
{
    internal class IdeasContextSeed
    {
        internal async Task SeedAsync(IdeasContext context, ILogger<IdeasContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(IdeasContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy( ILogger<IdeasContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
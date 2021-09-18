using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.HealthCheck
{
    public class UpstreamConnectionsCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = false;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://google.com");
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                healthCheckResultHealthy = true;

            if (healthCheckResultHealthy)
            {
                return await Task.FromResult(HealthCheckResult.Healthy("healthy: Contoso Client"));
            }

            return await Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus,"unhealthy: Contoso Client"));
        }
    }
}

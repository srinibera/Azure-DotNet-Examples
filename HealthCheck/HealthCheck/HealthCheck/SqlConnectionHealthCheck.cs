using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.HealthCheck
{
    public class SqlConnectionHealthCheck : IHealthCheck
    {
        public string ConnectionString { get; }

        private string TestQuery => "Select 1";

        public SqlConnectionHealthCheck()            
        {
        }

        public SqlConnectionHealthCheck(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;

                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}

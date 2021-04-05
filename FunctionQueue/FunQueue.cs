using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PlantCollection.Domain.Model.Entities;

namespace FunctionQueue
{
    public static class FunQueue
    {
        [FunctionName("FunQueue")]
        public static void Run([QueueTrigger("function-update-date-queue", Connection = "AzureWebJobsStorage")] string id, ILogger log)
        {
            log.LogInformation($"DEV FUNQUEUE CALLED");
            log.LogInformation($"{id}");

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Plant] SET [PageViews] = [PageViews] + 1 WHERE [Id] = id;";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }
        }
    }
}

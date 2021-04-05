using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionQueue
{
    public static class PageViewsFunction
    {
        [FunctionName("PageViewsFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var plantId = data?.Id;

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Plant] SET [PageViews] = [PageViews] + 1 WHERE [Id] = {plantId};";

                using SqlCommand cmd = new SqlCommand(textSql, conn);
                var rowsAffected = cmd.ExecuteNonQuery();
                log.LogInformation($"rowsAffected: {rowsAffected}");
            }

            return new OkResult();
        }
    }
}


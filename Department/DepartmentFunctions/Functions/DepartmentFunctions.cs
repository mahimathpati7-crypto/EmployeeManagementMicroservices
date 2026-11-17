using DepartmentFunctions.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DepartmentFunctions.Functions;

public sealed class DepartmentFunctions
{
    private readonly DepartmentDbContext _dbContext;
    private readonly ILogger<DepartmentFunctions> _logger;

    public DepartmentFunctions(
        DepartmentDbContext dbContext,
        ILogger<DepartmentFunctions> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [Function("GetDepartmentCount")]
    public async Task<HttpResponseData> GetDepartmentCount(
        [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "departments/count")] HttpRequestData request)
    {
        try
        {
            var count = await _dbContext.Departments.CountAsync();
            var response = request.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(new
            {
                count,
                message = "Department count retrieved successfully."
            });

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to read department count.");

            var response = request.CreateResponse(
                HttpStatusCode.ServiceUnavailable);

            await response.WriteAsJsonAsync(new
            {
                message = "Department database is unavailable."
            });

            return response;
        }
    }
}

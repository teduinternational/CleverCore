using CleverCore.Application.Dapper.Interfaces;
using CleverCore.Application.Dapper.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CleverCore.Application.Dapper.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _configuration;

        public ReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                DynamicParameters dynamicParameters = new DynamicParameters();
                DateTime now = DateTime.Now;

                DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                dynamicParameters.Add("@fromDate", string.IsNullOrEmpty(fromDate) ? firstDayOfMonth.ToString("MM/dd/yyyy") : fromDate);
                dynamicParameters.Add("@toDate", string.IsNullOrEmpty(toDate) ? lastDayOfMonth.ToString("MM/dd/yyyy") : toDate);


                return await sqlConnection.QueryAsync<RevenueReportViewModel>(
                    "GetRevenueDaily", dynamicParameters, commandType: CommandType.StoredProcedure);

            }
        }
    }
}

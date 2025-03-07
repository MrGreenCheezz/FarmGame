using System.Diagnostics.Metrics;
using System;
using FarmAppWebServer.Models;
using FarmAppWebServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FarmAppWebServer.Misc
{
    public class DataCache
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private List<PlantTypesData> _plantTypes;

        public DataCache(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            LoadData();
        }

        private void LoadData()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FarmAppDbContext>();
            _plantTypes = context.PlantTypesDataValues.AsNoTracking().ToList();
        }

        public IEnumerable<PlantTypesData> GetPlantsTypes() => _plantTypes;
    }
}

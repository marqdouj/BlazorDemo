using AspireDemo.ApiService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireDemo.ApiService.Data
{
    internal static class PIMSRepository
    {
        public static async Task<List<VPM>> VPMsAsync(this PIMSContext db)
        {
            var data = await db.VPM.AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();

            return data;
        }

        public static async Task<List<VPMEvent>> VPMEventsAsync(this PIMSContext db, int vpmId)
        {
            var data = await db.VPMEvent.AsNoTracking()
                .Where(e => e.VPMId == vpmId)
                .OrderBy(e => e.DFS).ThenBy(e => e.VPMEventTypeId)
                .ToListAsync();

            return data;
        }

        public static async Task<List<VPMEventType>> VPMEventTypesAsync(this PIMSContext db)
        {
            var data = await db.VPMEventType.AsNoTracking()
                .OrderBy(e => e.Description)
                .ToListAsync();

            return data;
        }

        public static async Task<List<VPMGps>> VPMGpsAsync(this PIMSContext db, int vpmId)
        {
            var data = await db.VPMGps.AsNoTracking()
                .Where(e => e.VPMId == vpmId)
                .OrderBy(e => e.DFS)
                .ToListAsync();

            return data;
        }

        public static async Task<List<VPMGpsType>> VPMGpsTypesAsync(this PIMSContext db)
        {
            var data = await db.VPMGpsType.AsNoTracking()
                .OrderBy(e => e.Description)
                .ToListAsync();

            return data;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using AspireDemo.ApiService.Data;
using Entities = AspireDemo.ApiService.Data.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PIMS.ApiService.ApiEndPoints
{
    internal static class PIMS
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
        };

        public static void MapPIMS(this WebApplication app, bool isDevelopment)
        {
            app.MapGet("/vpm", async (PIMSContext db) =>
            {
                var results = await db.VPMsAsync();
                return TypedResults.Ok(results);
            })
            .WithName("VPMs");

            app.MapGet("/vpm/events", async (int vpmId, PIMSContext db) =>
            {
                var results = await db.VPMEventsAsync(vpmId);
                return TypedResults.Ok(results);
            })
            .WithName("VPMEvents");

            app.MapGet("/vpm/events/types", async (PIMSContext db) =>
            {
                var results = await db.VPMEventTypesAsync();
                return TypedResults.Ok(results);
            })
            .WithName("VPMEventTypes");

            app.MapGet("/vpm/gps", async (int vpmId, PIMSContext db) =>
            {
                var results = await db.VPMGpsAsync(vpmId);
                return TypedResults.Ok(results);
            })
            .WithName("VPMGps");

            app.MapGet("/vpm/gps/types", async (PIMSContext db) =>
            {
                var results = await db.VPMGpsTypesAsync();
                return TypedResults.Ok(results);
            })
            .WithName("VPMGpsTypes");

            if (isDevelopment)
            {
                app.MapGet("/vpm/seed", async (PIMSContext db) =>
                {
                    var folder = Path.Combine(app.Environment.ContentRootPath, "Data", "Seed", "Json");

                    Console.WriteLine("Seed Folder: {0}", folder);

                    await SeedVPM(db, folder);
                    await SeedVPMEventType(db, folder);
                    await SeedVPMGpsType(db, folder);
                    await SeedVPMGps(db, folder);
                    await SeedVPMEvent(db, folder);

                    return;
                })
                .WithName("Seed");
            }
        }

        private static async Task SeedVPM(PIMSContext db, string folder)
        {
            var model = await db.VPM.FirstOrDefaultAsync();

            if (model is null)
            {
                Console.WriteLine("VPM has no data - seeding.");

                var json = File.ReadAllText(Path.Combine(folder, "VPM.json"));
                var items = JsonSerializer.Deserialize<List<Entities.VPM>>(json, jsonSerializerOptions) ?? [];

                await db.VPM.AddRangeAsync(items);
                var count = await db.SaveChangesAsync();
                Console.WriteLine($"VPM records saved = {count}.");
            }
            else
            {
                Console.WriteLine("VPM has data - skipping seed.");
            }
        }

        private static async Task SeedVPMEvent(PIMSContext db, string folder)
        {
            for (int i = 1; i < 4; i++)
            {
                var model = await db.VPMEvent.FirstOrDefaultAsync(e => e.VPMId == i);

                if (model is null)
                {
                    Console.WriteLine($"VPMEvent-{i} has no data - seeding.");

                    var json = File.ReadAllText(Path.Combine(folder, $"VPMEvent-{i}.json"));
                    var items = JsonSerializer.Deserialize<List<Entities.VPMEvent>>(json, jsonSerializerOptions) ?? [];

                    await db.VPMEvent.AddRangeAsync(items);
                    var count = await db.SaveChangesAsync();
                    Console.WriteLine($"VPMEvent-{i} records saved = {count}.");
                }
                else
                {
                    Console.WriteLine($"VPMEvent-{i} has data - skipping seed.");
                }
            }
        }

        private static async Task SeedVPMEventType(PIMSContext db, string folder)
        {
            var model = await db.VPMEventType.FirstOrDefaultAsync();

            if (model is null)
            {
                Console.WriteLine("VPMEventType has no data - seeding.");

                var json = File.ReadAllText(Path.Combine(folder, "VPMEventType.json"));
                var items = JsonSerializer.Deserialize<List<Entities.VPMEventType>>(json, jsonSerializerOptions) ?? [];

                await db.VPMEventType.AddRangeAsync(items);
                var count = await db.SaveChangesAsync();
                Console.WriteLine($"VPMEventType records saved = {count}.");
            }
            else
            {
                Console.WriteLine("VPMEventType has data - skipping seed.");
            }
        }

        private static async Task SeedVPMGps(PIMSContext db, string folder)
        {
            for (int i = 1; i < 4; i++)
            {
                var model = await db.VPMGps.FirstOrDefaultAsync(e => e.VPMId == i);

                if (model is null)
                {
                    Console.WriteLine($"VPMGps-{i} has no data - seeding.");

                    var json = File.ReadAllText(Path.Combine(folder, $"VPMGps-{i}.json"));
                    var items = JsonSerializer.Deserialize<List<Entities.VPMGps>>(json, jsonSerializerOptions) ?? [];

                    await db.VPMGps.AddRangeAsync(items);
                    var count = await db.SaveChangesAsync();
                    Console.WriteLine($"VPMGps-{i} records saved = {count}.");
                }
                else
                {
                    Console.WriteLine($"VPMGps-{i} has data - skipping seed.");
                }
            }
        }

        private static async Task SeedVPMGpsType(PIMSContext db, string folder)
        {
            var model = await db.VPMGpsType.FirstOrDefaultAsync();

            if (model is null)
            {
                Console.WriteLine("VPMGpsType has no data - seeding.");

                var json = File.ReadAllText(Path.Combine(folder, "VPMGpsType.json"));
                var items = JsonSerializer.Deserialize<List<Entities.VPMGpsType>>(json, jsonSerializerOptions) ?? [];

                await db.VPMGpsType.AddRangeAsync(items);
                var count = await db.SaveChangesAsync();
                Console.WriteLine($"VPMGpsType records saved = {count}.");
            }
            else
            {
                Console.WriteLine("VPMGpsType has data - skipping seed.");
            }
        }
    }
}

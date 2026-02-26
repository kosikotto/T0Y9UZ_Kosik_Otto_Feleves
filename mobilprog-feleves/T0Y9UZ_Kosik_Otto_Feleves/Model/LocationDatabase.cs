using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public class LocationDatabase : ILocationDatabase
    {
        SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "locations.db3");

        SQLiteAsyncConnection database;

        public LocationDatabase()
        {
            database = new SQLiteAsyncConnection(databasePath, Flags);
            database.CreateTableAsync<SavedLocation>().Wait();
        }
        public async Task CreateLocationAsync(SavedLocation location)
        {
            await database.InsertAsync(location);
        }

        public async Task DeleteLocationAsync(SavedLocation location)
        {
            await database.DeleteAsync(location);
        }

        public Task<SavedLocation> GetLocationAsync(SavedLocation location)
        {
            var tmp = database.Table<SavedLocation>().Where(i => i.Location == location.Location).FirstOrDefaultAsync();
            return tmp;
        }

        public async Task<List<SavedLocation>> GetLocationsAsync()
        {
            return await database.Table<SavedLocation>().ToListAsync();
        }

        public async Task UpdateLocationAsync(SavedLocation location)
        {
            await database.UpdateAsync(location);
        }

        public async Task Clear()
        {
            await database.DeleteAllAsync<SavedLocation>();
            await database.CloseAsync();
            File.Delete(databasePath);
        }
    }
}

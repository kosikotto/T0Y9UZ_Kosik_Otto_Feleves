using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public interface ILocationDatabase
    {
        //ReadAll
        Task<List<SavedLocation>> GetLocationsAsync();

        //Read
        Task<SavedLocation> GetLocationAsync(SavedLocation location);

        //Create
        Task CreateLocationAsync(SavedLocation location);

        //Delete
        Task DeleteLocationAsync(SavedLocation location);

        //Update
        Task UpdateLocationAsync(SavedLocation location);

        //Clear
        Task Clear();
    }
}

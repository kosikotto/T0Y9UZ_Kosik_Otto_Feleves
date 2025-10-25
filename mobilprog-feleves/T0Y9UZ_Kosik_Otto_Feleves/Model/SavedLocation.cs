using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T0Y9UZ_Kosik_Otto_Feleves.Model
{
    public partial class SavedLocation:ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        public string location;

        public SavedLocation()
        {
        }

        public SavedLocation(string location)
        {
            this.Location = location;
        }
    }
}

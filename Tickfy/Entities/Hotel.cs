using System.Collections;
using Tickfy.Enums;

namespace Tickfy.Entities;

public class Hotel
{
    public int Id { get; set; }
    public StarRating StarRating { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    //public bool IsActive { get; set; }
    
    public string Description { get; set; } = string.Empty;
    public ICollection<Room> Rooms { get; set; } = [];
    public ICollection<Driver> Drivers { get; set; } = [];


}


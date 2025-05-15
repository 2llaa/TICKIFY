using System.Text.Json.Serialization;
using Tickfy.Enums;

namespace Tickfy.Contracts.Classes;

public class CreateClassResponse
{
    public int Id { get; set; }
    public String className { get; set; }
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
    public int Capacity { get; set; }
};

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TICKIFY.Data.Enums
{
    public enum RoomStatus
    {
        Available,    // Ready for booking
        Booked,       // Currently occupied
        Maintenance,  // Under repair/cleaning
        OutOfOrder    // Not usable
    }
}

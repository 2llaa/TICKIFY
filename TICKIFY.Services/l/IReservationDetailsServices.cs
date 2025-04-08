using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICKIFY.Data.Entities;

namespace TICKIFY.Services.Abstracts
{
    public interface IReservationDetailsServices
    {


        Task<IEnumerable<ReservationDetails>> GetAllHotelsAsync();
        Task<ReservationDetails> GetHotelByIdAsync(int id);
        Task<ReservationDetails> CreateHotelAsync(ReservationDetails Details);
        Task<Hotels> UpdateHotelAsync(ReservationDetails Details);
        Task DeleteHotelAsync(int id);
        //int SaveChanges();

    }
}




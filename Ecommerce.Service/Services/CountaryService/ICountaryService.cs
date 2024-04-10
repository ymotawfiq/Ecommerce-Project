using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services.CountaryService
{
    public interface ICountaryService
    {
        Task<ApiResponse<IEnumerable<Countary>>> GetAllCountariesAsync();
        Task<ApiResponse<Countary>> AddCountaryAsync(CountaryDto countaryDto);
        Task<ApiResponse<Countary>> UpdateCountaryAsync(CountaryDto countaryDto);
        Task<ApiResponse<Countary>> GetCountryByIdAsync(Guid countaryId);
        Task<ApiResponse<Countary>> DeleteCountryByIdAsync(Guid countaryId);
    }
}

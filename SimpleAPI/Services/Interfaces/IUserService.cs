using SimpleAPI.DTOs;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services.Interfaces
{
    public interface IUserService
    {
       Task<ServiceResponse<IEnumerable<GetUserDTO>>> GetAllAsync();
    }
}

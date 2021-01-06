using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleAPI.DTOs;
using SimpleAPI.Models;
using SimpleAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services.Classes
{
    public class UsersService : IUserService
    {
        private readonly IUserRepository _usersRepo;
        private readonly ILogger<UsersService> _logger;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository usersRepo, ILogger<UsersService> logger, IMapper mapper)
        {
            _usersRepo = usersRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<GetUserDTO>>> GetAllAsync()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<GetUserDTO>>(await _usersRepo.GetAllAsync());               
                return new ServiceResponse<IEnumerable<GetUserDTO>> { Data = result };
            }catch(Exception e)
            {
                _logger.LogError($"Error while retrieving data. Message: {e.Message} {e.InnerException?.Message ?? ""}");
                return new ServiceResponse<IEnumerable<GetUserDTO>> { Success = false, ErrorMessage = "Unexpected error while retrieving data" };
            }
        }
    }
}

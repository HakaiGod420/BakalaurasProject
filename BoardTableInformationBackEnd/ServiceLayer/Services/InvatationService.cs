using DataLayer.Repositories.User;
using Microsoft.Extensions.Configuration;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class InvatationService : IInvatationService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public InvatationService(IUserRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public PostInvatationDto PostInvatation(PostInvatationDto data, int id)
        {
            throw new NotImplementedException();
        }
    }
}

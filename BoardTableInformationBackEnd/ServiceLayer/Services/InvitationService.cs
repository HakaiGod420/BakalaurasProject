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
    public class InvitationService : IInvitationService
    {


        public InvitationService()
        {

        }

        public Task<PostInvatationDto> PostInvatation(PostInvatationDto data, int id)
        {
            throw new NotImplementedException();
        }
    }
}

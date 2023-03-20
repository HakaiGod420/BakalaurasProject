using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.Invitation;
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
        private readonly IAddressRepository _addressRepository;
        private readonly IInvitationRepository _invitationRepository;

        public InvitationService(IAddressRepository addressRepository, IInvitationRepository invitationRepository)
        {
            _addressRepository = addressRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<PostInvatationDto> PostInvatation(PostInvatationDto data, int id)
        {
            var fullAddress = data.Address.City + " " + data.Address.Province + " " + data.Address.StreetName + " " + data.Address.HouseNumber.ToString();

            var addressEntity = await _addressRepository.CheckIfExistAddress(fullAddress);

            if(addressEntity == null)
            {
                addressEntity = new AddressEntity
                {
                    StreetName = data.Address.StreetName,
                    Province = data.Address.Province,
                    City = data.Address.City,
                    PostalCode = data.Address.PostalCode == null ? "" : data.Address.PostalCode,
                    FullAddress = fullAddress,

                };

                addressEntity = await _addressRepository.AddAddress(addressEntity);
            }
            var invititionEntity = new ActiveGameEntity
            {
                BoardGameId = data.ActiveGameId,
                PlayersNeed = data.PlayersNeeded,
                RegistredPlayerCount = 0,
                Map_X_Cords = data.Map_X_Cords,
                Map_Y_Cords = data.Map_Y_Cords,
                CreatorId = id,
                AddressId = addressEntity.AddressId,
                InvitationStateId = ModelLayer.Enum.ActiveGameState.Open,
            };

            await _invitationRepository.AddInvitation(invititionEntity);

            return data;
        }
    }
}

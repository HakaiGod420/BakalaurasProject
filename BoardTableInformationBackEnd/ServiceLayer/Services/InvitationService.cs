using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.Invitation;
using DataLayer.Repositories.User;
using Microsoft.Extensions.Configuration;
using ModelLayer.DTO;
using ModelLayer.Enum;
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
        private readonly IUserRepository _userRepository;

        public InvitationService(IAddressRepository addressRepository, IInvitationRepository invitationRepository, IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
        }

        public async Task ChangeInvitationState(InvitationStateChangeDto data)
        {
            if(data.State == "accept")
            {
                await _invitationRepository.UpdateStateInvitation(Convert.ToInt32(InvitationState.Accepted), data.InvitationId, data.UserId);
                await _invitationRepository.UpdatePlayerCount(data.InvitationId);
            }
            else if(data.State == "decline")
            {
                await _invitationRepository.UpdateStateInvitation(Convert.ToInt32(InvitationState.Declined), data.InvitationId, data.UserId);
            }
        }

        public async Task<List<UserInvitationDto>> GetActiveInvitations(int id)
        {
            return await _invitationRepository.GetAllActiveInvitations(id);
        }

        public async Task<List<UserInvitationDto>> GetInvitations(int id)
        {
            return await _invitationRepository.GetAllInvitations(id);
        }

        public async Task<List<UserInvitationDto>> GetCreatedInvitations(int id)
        {
            return await _invitationRepository.GetAllCreatedInvitations(id);
        }

        public async Task<PostInvatationDto> PostInvatation(PostInvatationDto data, int id)
        {
            var fullAddress = data.Address.City + " " + data.Address.Province + " " + data.Address.StreetName + " " + data.Address.HouseNumber.ToString();

            var addressEntity = await _addressRepository.CheckIfExistAddress(fullAddress);

            if (addressEntity == null)
            {
                addressEntity = new AddressEntity
                {
                    Country = data.Address.Country,
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
                RegisteredPlayerCount = 0,
                Map_X_Coords = data.Map_X_Cords,
                Map_Y_Coords = data.Map_Y_Cords,
                CreatorId = id,
                AddressId = addressEntity.AddressId,
                MeetDate = Convert.ToDateTime(data.InvitationDate),
                InvitationStateId = ModelLayer.Enum.ActiveGameState.Open,
            };

            invititionEntity = await _invitationRepository.AddInvitation(invititionEntity);

            await Task.Run(() =>
            SentInvitatiosToOthers(invititionEntity));
            
            return data;
        }

        private async Task SentInvitatiosToOthers(ActiveGameEntity activeGameEntity)
        {
            var userIdListWhichToSend = await _userRepository.GetCloseUserIds(activeGameEntity.CreatorId,activeGameEntity.Address);

            {
                foreach (var userId in userIdListWhichToSend)
                {
                    var invitationToSent = new SentInvitationEntity
                    {
                        SelectedActiveGameId = activeGameEntity.ActiveGameId,
                        UserId = userId,
                        InvitationStateId = Convert.ToInt32(InvitationState.Onhold)
                    };

                     await _invitationRepository.SentInvitation(invitationToSent);
                };
            }
        }

        public async Task<int> ActiveInvitationCount(int id)
        {
            return await _invitationRepository.ActiveInvitationCount(id);
        }

        public async Task SentInvitationToUser(SingeUserSentInvitationDTO invitation)
        {
            var userId = await _userRepository.GetUserIdByUsername(invitation.UserName);

            if(userId == null)
            {
                throw new Exception("User not found");
            }

            var invitationToSent = new SentInvitationEntity
            {
                SelectedActiveGameId = invitation.ActiveInvitationId,
                UserId = (int)userId,
                InvitationStateId = Convert.ToInt32(InvitationState.Onhold)
            };

            await _invitationRepository.SentInvitation(invitationToSent);
        }

        public Task<InvitationsListResponse> GetInvitationsByCountry(string country, int pageIndex, int pageSize,int userId)
        {
            if(country == null)
            {
                throw new ArgumentNullException("Country must be selected");
            }

            return _invitationRepository.GetInvitationsByCountry(country, pageIndex, pageSize,userId);
        }

        public async Task JointInvitation(JoinInvitationDTO invitation, int userId)
        {
            if(invitation == null)
            {
                throw new ArgumentNullException("JoinInvitationDTO is null");
            }

            var newInvitationAcception = new SentInvitationEntity
            {
                UserId = userId,
                InvitationStateId = (int)InvitationState.Accepted,
                SelectedActiveGameId = invitation.SelectedActiveInvitation,
            };

            var id = await _invitationRepository.JointInvitation(newInvitationAcception);

            await _invitationRepository.UpdatePlayerCount(id);
        }

        public async Task<List<Parcipant>> GetParticipants(int invitationId)
        {
            return await _invitationRepository.GetParticipants(invitationId);
        }

        public async Task<bool> ChangeParticipantsState(int userId, int activeGameId, bool IsBlocked)
        {
            return await _invitationRepository.ChangeParticipantState(userId, activeGameId,IsBlocked);
        }
    }
}

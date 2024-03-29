﻿using DataLayer.Models;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Invitation
{
    public interface IInvitationRepository
    {
        public Task<ActiveGameEntity> AddInvitation(ActiveGameEntity invitation);
        public Task<bool> SentInvitation(SentInvitationEntity invitation);
        public Task<List<UserInvitationDto>> GetAllInvitations(int userId);
        public Task<List<UserInvitationDto>> GetAllActiveInvitations(int userId);
        public Task<List<UserInvitationDto>> GetAllCreatedInvitations(int userId);
        public Task UpdateStateInvitation(int newStateId, int invitationId, int userId);
        public Task<bool> UpdatePlayerCount(int invitationId, bool reducePlayer = false);
        public Task<int> ActiveInvitationCount(int userId);
        public Task<InvitationsListResponse> GetInvitationsByCountry(string country,int pageIndex,int pageSize, int userId);
        public Task<int> JointInvitation(SentInvitationEntity invitation);
        public Task<List<Parcipant>> GetParticipants(int invitationId);
        public Task<bool> ChangeParticipantState(int userId, int activeGameId, bool IsBlocked);
    }
}

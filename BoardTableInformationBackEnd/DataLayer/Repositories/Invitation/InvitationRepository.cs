using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Invitation
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly DataBaseContext _dbContext;
        public InvitationRepository(DataBaseContext dataBaseContext) 
        {
            _dbContext = dataBaseContext;
        }

        public async Task<ActiveGameEntity> AddInvitation(ActiveGameEntity invitation)
        {
            await _dbContext.ActiveGames.AddAsync(invitation);
            await _dbContext.SaveChangesAsync();
            return invitation;
        }

        public async Task<List<UserInvitationDto>> GetAllInvitations(int userId)
        {
            var result = await _dbContext.SentInvitations
                .Where(x =>
                    x.UserId == userId 
                    && x.InvitationStateId == 1
                    && x.SelectedActiveGame.MeetDate > DateTime.Now)
                .Select(j => new UserInvitationDto
                {
                    InvitationId = j.SentInvitationId,
                    ActiveGameId = j.SelectedActiveGameId,
                    BoardGameTitle = j.SelectedActiveGame.BoardGame.Title,
                    BoardGameId = j.SelectedActiveGame.BoardGameId,
                    EventDate = j.SelectedActiveGame.MeetDate,
                    EventFullLocation = j.SelectedActiveGame.Address.FullAddress,
                    MaxPlayerCount = j.SelectedActiveGame.PlayersNeed,
                    AcceptedCount = j.SelectedActiveGame.RegistredPlayerCount,
                    Map_X_Cords = j.SelectedActiveGame.Map_X_Cords,
                    Map_Y_Cords = j.SelectedActiveGame.Map_Y_Cords
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<UserInvitationDto>> GetAllActiveInvitations(int userId)
        {
            var result = await _dbContext.SentInvitations
                .Where(x =>
                    x.UserId == userId
                    && x.InvitationStateId == 2
                    && x.SelectedActiveGame.MeetDate > DateTime.Now)
                .Select(j => new UserInvitationDto
                {
                    InvitationId = j.SentInvitationId,
                    ActiveGameId = j.SelectedActiveGameId,
                    BoardGameTitle = j.SelectedActiveGame.BoardGame.Title,
                    BoardGameId = j.SelectedActiveGame.BoardGameId,
                    EventDate = j.SelectedActiveGame.MeetDate,
                    EventFullLocation = j.SelectedActiveGame.Address.FullAddress,
                    MaxPlayerCount = j.SelectedActiveGame.PlayersNeed,
                    AcceptedCount = j.SelectedActiveGame.RegistredPlayerCount,
                    Map_X_Cords = j.SelectedActiveGame.Map_X_Cords,
                    Map_Y_Cords = j.SelectedActiveGame.Map_Y_Cords
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<UserInvitationDto>> GetAllCreatedInvitations(int userId)
        {
            var result = await _dbContext.SentInvitations
                .Where(x =>
                    x.SelectedActiveGame.CreatorId == userId
                    && x.SelectedActiveGame.MeetDate > DateTime.Now)
                .Select(j => new UserInvitationDto
                {
                    InvitationId = j.SentInvitationId,
                    ActiveGameId = j.SelectedActiveGameId,
                    BoardGameTitle = j.SelectedActiveGame.BoardGame.Title,
                    BoardGameId = j.SelectedActiveGame.BoardGameId,
                    EventDate = j.SelectedActiveGame.MeetDate,
                    EventFullLocation = j.SelectedActiveGame.Address.FullAddress,
                    MaxPlayerCount = j.SelectedActiveGame.PlayersNeed,
                    AcceptedCount = j.SelectedActiveGame.RegistredPlayerCount,
                    Map_X_Cords = j.SelectedActiveGame.Map_X_Cords,
                    Map_Y_Cords = j.SelectedActiveGame.Map_Y_Cords
                })
                .ToListAsync();

            return result;
        }

        public async Task<bool> SentInvitation(SentInvitationEntity invitation)
        {
            _dbContext.SentInvitations.Add(invitation);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateStateInvitation(int newStateId, int invitationId,int userId)
        {
            var invitation = await _dbContext.SentInvitations.Where(x => x.SentInvitationId == invitationId && x.UserId == userId).SingleAsync();
            
            if(invitation != null)
            {
                invitation.InvitationStateId = newStateId;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePlayerCount(int invitationId)
        {
            var invitation = await _dbContext.SentInvitations.Where(x => x.SentInvitationId == invitationId).SingleAsync();

            if(invitation != null)
            {
                invitation.SelectedActiveGame.RegistredPlayerCount++;

                if(invitation.SelectedActiveGame.RegistredPlayerCount == invitation.SelectedActiveGame.PlayersNeed)
                {
                    invitation.SelectedActiveGame.InvitationStateId = ActiveGameState.Closed;
                }
                else if(invitation.SelectedActiveGame.RegistredPlayerCount > invitation.SelectedActiveGame.PlayersNeed)
                {
                    return false;
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

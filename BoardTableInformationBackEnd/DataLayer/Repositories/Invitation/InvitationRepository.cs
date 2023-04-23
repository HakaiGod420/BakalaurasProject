using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var result = await _dbContext.ActiveGames
                .Where(x =>
                    x.CreatorId == userId
                    && x.MeetDate > DateTime.Now)
                .Select(j => new UserInvitationDto
                {
                    InvitationId = j.ActiveGameId,
                    ActiveGameId = j.ActiveGameId,
                    BoardGameTitle = j.BoardGame.Title,
                    BoardGameId = j.BoardGameId,
                    EventDate = j.MeetDate,
                    EventFullLocation = j.Address.FullAddress,
                    MaxPlayerCount = j.PlayersNeed,
                    AcceptedCount = j.RegistredPlayerCount,
                    Map_X_Cords = j.Map_X_Cords,
                    Map_Y_Cords = j.Map_Y_Cords
                })
                .ToListAsync();

            return result.OrderByDescending(m=>m.EventDate).ToList();
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
            var activeGameId = await _dbContext.SentInvitations.Where(x => x.SentInvitationId == invitationId).Select(x=>x.SelectedActiveGameId).FirstOrDefaultAsync();

            var invitation = await _dbContext.ActiveGames.Where(x => x.ActiveGameId == activeGameId).FirstOrDefaultAsync();

            if(invitation != null)
            {
                invitation.RegistredPlayerCount++;

                if(invitation.RegistredPlayerCount == invitation.PlayersNeed)
                {
                    invitation.InvitationStateId = ActiveGameState.Closed;
                }
                else if(invitation.RegistredPlayerCount > invitation.PlayersNeed)
                {
                    return false;
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> ActiveInvitationCount(int userId)
        {
            return await _dbContext.SentInvitations
                .Where(x =>
                    x.UserId == userId
                    && x.InvitationStateId == 1
                    && x.SelectedActiveGame.MeetDate > DateTime.Now)
                .CountAsync();
        }

        public async Task<InvitationsListResponse> GetInvitationsByCountry(string country,int pageIndex, int pageSize)
        {
            var query = _dbContext.ActiveGames
                .Include(x => x.BoardGame)
                .Include(x => x.Address)
                .Where(x =>
                    x.InvitationStateId == ActiveGameState.Open
                    && x.MeetDate > DateTime.Now
                    && x.Address.Country == country)
                .Select(x => new InvitationItem
                {
                    InvitationId = x.ActiveGameId,
                    BoardGameId = x.BoardGameId,
                    BoardGameTitle = x.BoardGame.Title,
                    Date = x.MeetDate,
                    Location = x.Address.FullAddress,
                    MaxPlayer = x.PlayersNeed,
                    AcceptedPlayer = x.RegistredPlayerCount,
                    ImageUrl = "Images/" + Regex.Replace(x.BoardGame.Title, @"[^\w\s]+", "").Replace(" ", "_") + "/" + x.BoardGame.Thubnail_Location,
                });

            var totalCount = await query.CountAsync();

            var result = await query
                .Skip(pageSize*pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new InvitationsListResponse
            { 
                Invitations = result,
                TotalCount = totalCount
            };
        }
    }
}

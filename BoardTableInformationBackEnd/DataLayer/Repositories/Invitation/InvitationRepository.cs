﻿using DataLayer.DBContext;
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
            if(invitation == null)
            {
                throw new ArgumentNullException("ActiveGameEntity");
            }

            _dbContext.ActiveGames.Add(invitation);
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
                    AcceptedCount = j.SelectedActiveGame.RegisteredPlayerCount,
                    Map_X_Cords = j.SelectedActiveGame.Map_X_Coords,
                    Map_Y_Cords = j.SelectedActiveGame.Map_Y_Coords
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
                    AcceptedCount = j.SelectedActiveGame.RegisteredPlayerCount,
                    Map_X_Cords = j.SelectedActiveGame.Map_X_Coords,
                    Map_Y_Cords = j.SelectedActiveGame.Map_Y_Coords
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
                    AcceptedCount = j.RegisteredPlayerCount,
                    Map_X_Cords = j.Map_X_Coords,
                    Map_Y_Cords = j.Map_Y_Coords
                })
                .ToListAsync();

            return result.OrderByDescending(m=>m.EventDate).ToList();
        }

        public async Task<bool> SentInvitation(SentInvitationEntity invitation)
        {
            if(invitation == null || invitation.SelectedActiveGameId <= 0)
            {
                return false;
            }    

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

        public async Task<bool> UpdatePlayerCount(int invitationId, bool reducePlayer = false)
        {
            var activeGameId = await _dbContext.SentInvitations.Where(x => x.SentInvitationId == invitationId).Select(x=>x.SelectedActiveGameId).FirstOrDefaultAsync();

            var invitation = await _dbContext.ActiveGames.Where(x => x.ActiveGameId == activeGameId).FirstOrDefaultAsync();

            if(invitation != null)
            {
                if (reducePlayer)
                {
                    invitation.RegisteredPlayerCount--;
                }
                else
                {
                    invitation.RegisteredPlayerCount++;
                }

                if(invitation.RegisteredPlayerCount == invitation.PlayersNeed)
                {
                    invitation.InvitationStateId = ActiveGameState.Closed;
                }

                else if(invitation.RegisteredPlayerCount > invitation.PlayersNeed)
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

        public async Task<InvitationsListResponse> GetInvitationsByCountry(string country,int pageIndex, int pageSize,int userId)
        {
            var activeIds = new List<int>();

            activeIds = await _dbContext.SentInvitations
                .Where(x => x.UserId == userId && x.InvitationStateId != 3)
                .Select(x => x.SelectedActiveGameId)
                .ToListAsync();

            var query = _dbContext.ActiveGames
                .Include(x => x.BoardGame)
                .Include(x => x.Address)
                .Where(x =>
                    x.InvitationStateId == ActiveGameState.Open
                    && x.MeetDate > DateTime.Now
                    && x.Address.Country == country
                    && x.CreatorId != userId
                    && !activeIds.Contains(x.ActiveGameId))
                .Select(x => new InvitationItem
                {
                    InvitationId = x.ActiveGameId,
                    BoardGameId = x.BoardGameId,
                    BoardGameTitle = x.BoardGame.Title,
                    Date = x.MeetDate,
                    Location = x.Address.FullAddress,
                    MaxPlayer = x.PlayersNeed,
                    AcceptedPlayer = x.RegisteredPlayerCount,
                    ImageUrl = "Images/" + Regex.Replace(x.BoardGame.Title, @"[^\w\s]+", "").Replace(" ", "_") + "/" + x.BoardGame.Thumbnail_Location,
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

        public async Task<int> JointInvitation(SentInvitationEntity invitation)
        {
            _dbContext.SentInvitations.Add(invitation);

            await _dbContext.SaveChangesAsync();

            return invitation.SentInvitationId;
        }

        public async Task<List<Parcipant>> GetParticipants(int invitationId)
        {
            var result = await _dbContext.SentInvitations
                .Where(x => x.SelectedActiveGameId == invitationId && x.InvitationStateId == 2)
                .Select(x => new Parcipant
                {
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    IsBlocked = x.IsBlocked,
                })
                .ToListAsync();

            return result;
        }

        public async Task<bool> ChangeParticipantState(int userId, int activeGameId, bool IsBlocked)
        {
            var entity = await _dbContext.SentInvitations.Where(x => x.UserId == userId && x.SelectedActiveGameId == activeGameId).FirstOrDefaultAsync();

            if(entity == null)
            {
                return false;
            }

            if(!IsBlocked)
            {
                entity.IsBlocked = false;
                var checkIfCanBeAdd = await UpdatePlayerCount(entity.SentInvitationId);

                if (!checkIfCanBeAdd)
                {
                    return false;
                }
            }
            else
            {
                await UpdatePlayerCount(entity.SentInvitationId,true);

                entity.IsBlocked = true;
            }

            _dbContext.SentInvitations.Update(entity);

            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}

using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IInvitationService
    {
        public Task<PostInvatationDto> PostInvatation(PostInvatationDto data, int id);
        public Task<List<UserInvitationDto>> GetInvitations(int id);
        public Task ChangeInvitationState(InvitationStateChangeDto data);
        public Task<List<UserInvitationDto>> GetActiveInvitations(int id);
        public Task<List<UserInvitationDto>> GetCreatedInvitations(int id);
        public Task<int> ActiveInvitationCount(int id);
        public Task SentInvitationToUser(SingeUserSentInvitationDTO invitation);
        public Task<InvitationsListResponse> GetInvitationsByCountry(string country, int pageIndex, int pageSize, int userId);
        public Task JointInvitation(JoinInvitationDTO invitation, int userId);
        public Task<List<Parcipant>> GetParticipants(int invitationId);
        public Task<bool> ChangeParticipantsState(int userId, int activeGameId,bool IsBlocked);
    }
}

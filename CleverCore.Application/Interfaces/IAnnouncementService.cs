using System;
using CleverCore.Application.ViewModels.System;
using CleverCore.Utilities.Dtos;

namespace CleverCore.Application.Interfaces
{
    public interface IAnnouncementService
    {
        PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize);

        bool MarkAsRead(Guid userId, Guid id);
    }
}

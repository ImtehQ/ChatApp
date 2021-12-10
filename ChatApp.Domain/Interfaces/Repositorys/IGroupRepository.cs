using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace ChatApp.Domain.Interfaces
{
    public interface IGroupRepository
    {
        void DeleteGroup(int GroupID);
        Group GetGroupByID(int id);
        IEnumerable<Group> GetGroups();
        void InsertGroup(Group Group);
        void Save();
        void UpdateGroup(Group Group);
    }
}

using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

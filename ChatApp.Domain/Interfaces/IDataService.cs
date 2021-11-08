using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapApp.Domain.Interfaces
{
    public interface IDataService
    {
       public void Register(string Name, string Username, string Emailaddress, string Password);
       public void Login(string username, string password);
    }
}

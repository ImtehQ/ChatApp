using ChapApp.Business.Core.Repositorys;
using ChapApp.Business.Domain.Interfaces;
using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Services
{
    public class DataService : IDataService
    {
        private IRepository _repository;
        private IJwtAuth _JwtAuth;

        public DataService(IRepository repository, IJwtAuth jwtAuth)
        {
            _repository = repository;
            _JwtAuth = jwtAuth;
        }

        public void Login(string username, string password)
        {
            _JwtAuth.Authentication(username, password);

            throw new NotImplementedException();
        }

        public void Register(string Name, string Username, string Emailaddress, string Password)
        {

        }
    }
}

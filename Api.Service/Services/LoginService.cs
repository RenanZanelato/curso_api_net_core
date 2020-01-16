using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services;
using Api.Domain.Repository;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _repository;
        private TokenService _tokenService;


        public LoginService(IUserRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;

        }
        public async Task<object> FindByLogin(LoginDto entity)
        {
            if(entity == null && string.IsNullOrWhiteSpace(entity.Email))
            {
                return null;
            }

            var userData = await _repository.FindByLogin(entity.Email);

            if(userData == null)
            {
                return new
                {
                    authenticate = false,
                    message = "Fail to get Data"
                };
            }

            return _tokenService.GetToken(entity.Email);


        }
    }
}

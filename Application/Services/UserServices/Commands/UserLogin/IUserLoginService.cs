using Common;

namespace Application.Services.UserService.Commands.UserLogin
{
    public interface IUserLoginService
    {
        ResultDto<LoginResultDto> Execute(LoginDto dto);
    }
}


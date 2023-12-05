using BLL.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteUserInfoCommand : IRequest<bool>
    {
        public int UserInfoId;
    }

    public class DeleteUserInfoCommandHandler : IRequestHandler<DeleteUserInfoCommand, bool>
    {
        public readonly IUserInfoService _userInfoService;

        public DeleteUserInfoCommandHandler(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        public async Task<bool> Handle(DeleteUserInfoCommand request, CancellationToken cancellationToken)
        {
            return await _userInfoService.DeleteUserInfoAsync(request.UserInfoId, cancellationToken);
        }
    }
}

using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateUserInfoCommand : IRequest<int>
    {
        public UserInfoDto userInfoDto;
    }

    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, int>
    {
        public readonly IUserInfoService _userInfoService;
        public readonly IMapper _mapper;

        public UpdateUserInfoCommandHandler(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            return await _userInfoService.UpdateUserInfoAsync(_mapper.Map<UserInfo>(request.userInfoDto), cancellationToken);

        }
    }
}

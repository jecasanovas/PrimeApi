using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertUserInfoCommand : IRequest<int>
    {
        public UserInfoDto userInfoDto;
    }

    public class InsertUserInfoCommandHandler : IRequestHandler<InsertUserInfoCommand, int>
    {
        public readonly IUserInfoService _userInfoService;
        public readonly IMapper _mapper;

        public InsertUserInfoCommandHandler(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }


        public async Task<int> Handle(InsertUserInfoCommand request, CancellationToken cancellationToken)
        {
            return await _userInfoService.InsertUserInfoAsync(_mapper.Map<UserInfo>(request.userInfoDto), cancellationToken);

        }
    }
}

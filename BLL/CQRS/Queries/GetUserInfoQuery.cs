using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetUserInfoQuery : IRequest<DataResults<UserInfoDto>>
    {
        public SearchParamUsers searchParams;
    }

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, DataResults<UserInfoDto>>
    {
        private IUserInfoService _userInfoService;
        private IMapper _mapper;
        public GetUserInfoQueryHandler(IMapper mapper, IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        public async Task<DataResults<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _userInfoService.GetUsersInfoAsync(request.searchParams, cancellationToken);

            var nrows = await _userInfoService.GetTotalRowsAsync(request.searchParams, cancellationToken);

            return new DataResults<UserInfoDto>()
            {
                Dto = _mapper.Map<IEnumerable<UserInfoDto>>(result),
                Results = nrows
            };
        }
    }

}
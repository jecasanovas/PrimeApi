using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetTeacherQuery : IRequest<DataResults<TeacherDto>>
    {
        public SearchParamTeachers searchParams;
    }

    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, DataResults<TeacherDto>>
    {
        private ITeacherService _teacherService;
        private IMapper _mapper;
        public GetTeacherQueryHandler(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        public async Task<DataResults<TeacherDto>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {

            var teachers = await _teacherService.GetTeachersAsync(request.searchParams, cancellationToken);
            //Count results without pagination active, for paging info
            var nrows = await _teacherService.GetTotalRowsAsync(request.searchParams, cancellationToken);

            return new DataResults<TeacherDto>()
            {
                Dto = _mapper.Map<IEnumerable<TeacherDto>>(teachers),
                Results = nrows
            };
        }
    }
}


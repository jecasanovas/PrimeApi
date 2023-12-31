using BLL.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteCourseDetailCommand : IRequest<bool>
    {
        public int CourseId;
    }
    public class DeleteCourseDetailCommandHandler : IRequestHandler<DeleteCourseDetailCommand, bool>
    {
        public readonly ICourseService _courseService;
        public DeleteCourseDetailCommandHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<bool> Handle(DeleteCourseDetailCommand request, CancellationToken cancellationToken)
        {
            return await _courseService.DeleteCourseAsync(request.CourseId, cancellationToken);
        }
    }
}
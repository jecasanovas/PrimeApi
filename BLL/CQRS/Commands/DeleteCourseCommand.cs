using BLL.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteCourseCommand : IRequest<bool>
    {
        public int CourseId;
    }
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        public readonly ICourseService _courseService;
        public DeleteCourseCommandHandler(ICourseService course)
        {
            _courseService = course;
        }
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.DeleteCourseAsync(request.CourseId, cancellationToken);
            return true;
        }
    }
}
using API.Helpers;
using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PrimeApi;
using PrimeApi.Api.Helpers;

namespace Courses.Api.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseService _courseService;
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly IConfiguration _configuration;

        public CourseController(IMapper mapper, ICourseService courseService, IMediator mediator, IConfiguration configuration)
        {
            _courseService = courseService;
            _mapper = mapper;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<CourseDto>>> Course([FromQuery] SearchParamCourses searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseQuery()
                {
                    searchParams = searchParameters
                });
                return Ok(new Pagination<CourseDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<CourseDto>)result.Dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Detail")]
        public async Task<ActionResult<Pagination<CourseDetailDto>>> CourseDetails([FromQuery] SearchParamCourses searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseDetailQuery()
                {
                    searchParams = searchParameters
                });
                return Ok(new Pagination<CourseDetailDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<CourseDetailDto>)result.Dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<CourseDto>> UpdateCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateCourseCommand()
                {
                    Course = courseDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CourseDto>> InsertCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertCourseCommand()
                {
                    Course = courseDto
                }));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("Detail")]
        public async Task<ActionResult> UpdateCourseDetail([FromBody] CourseDetailDto courseDetailDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateCourseDetailCommand()
                {
                    Course = courseDetailDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Detail")]
        public async Task<ActionResult> InsertCourseDetail([FromBody] CourseDetailDto courseDetailDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertCourseDetailCommand()
                {
                    Course = courseDetailDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCourse(int id)
        {
            try
            {
                return await _mediator.Send(new DeleteCourseCommand()
                {
                    CourseId = id
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
        [HttpGet]
        [Route("Download")]
        public async Task<ActionResult> DownloadIndex(int id)
        {
            var searchParams = new SearchParamCourses()
            {
                CourseId = id
            };
            var result = await _mediator.Send(new GetCourseDetailQuery()
            {
                searchParams = searchParams
            });
            Reports.CreateReport(result.Dto, "CourseIndex.xls", false);

            _ = new FileExtensionContentTypeProvider().TryGetContentType("CourseIndex.xls", out string? contentType);

            var memory = new MemoryStream();
            using (var stream = new FileStream(@"../files/CourseIndex.xls", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, contentType ?? String.Empty, "CourseIndex.xls");
        }

        [Authorize]
        [HttpPost]
        [Route("File")]
        public async Task<ActionResult<CourseDto>> PostFile(int id)
        {
            try
            {
                var file = Request.Form;
                if (file.Files == null || file.Files.Count == 0) return NoContent();
                return Ok(await _courseService.PostFileAsync(id, Request.Form.Files[0], CancellationToken.None));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult<CourseDetailDto>> Index(int id)
        {
            try
            {
                var filePath = String.Empty;
                var formFile = Request.Form.Files[0];

                if (Request.Form?.Files?.Count > 0)
                {
                    filePath = await FileUtilities.CreateFile(Request.Form.Files[0]);
                    var result = Reports.ReadCsv(filePath, id);

                    return Ok(await _mediator.Send(new InsertCourseDetailMasiveCommand()
                    {
                        Course = result

                    }));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}

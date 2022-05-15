using API.Helpers;
using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Models;
using Core.DBContext;
using Core.Entities;
using CsvHelper;
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
        private readonly ICourseRepository _courseRepository;
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly IConfiguration _configuration;

        public CourseController(GestionCursosContext context, IMapper mapper, ICourseRepository courseRepository,IMediator mediator, IConfiguration configuration)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]

        public async Task<ActionResult<Pagination<CourseDto>>> Course([FromQuery] SearchParamCourses searchParameters)
        {
            try
            {
                bool useCQRS = false;

                var result = useCQRS ?  await  _mediator.Send(new CourseQueries() 
                {
                    searchParams = searchParameters
                }) :   await _courseRepository.GetCourses(searchParameters);

                return Ok(new Pagination<CourseDto>(searchParameters.page, searchParameters.pageSize, result.Results,(IReadOnlyList<CourseDto>)result.Dto)); 
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
                var result = await _courseRepository.GetCourseDetails(searchParameters);
                IEnumerable<CourseDetailDto> DetailsDto = _mapper.Map<IEnumerable<CourseDetail>, IEnumerable<CourseDetailDto>>(result);
                var rows = await _courseRepository.GetTotalCoursesDetails(searchParameters);
                return new Pagination<CourseDetailDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<CourseDetailDto>)DetailsDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // POST api/<CourseController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<CourseDto>> UpdateCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                bool useCQRS = false;

                return Ok(useCQRS ? await _mediator.Send(new CourseCommand()
                {
                    Course = courseDto,
                    OperationDB = Operation.InsertUpdate
                }): await _courseRepository.UpdateCourse(courseDto));

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
                bool useCQRS = false;

                return Ok(useCQRS ? await _mediator.Send(new CourseCommand() {
                    OperationDB = Operation.InsertUpdate,
                    Course = courseDto }) : await _courseRepository.InsertCourse(courseDto));

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
                return Ok(await _courseRepository.UpdateCourseDetail(courseDetailDto));
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
                return Ok(await _courseRepository.InsertCourseDetail(courseDetailDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                bool useCQRS = false;

                SearchParamCourses searchParameters = new SearchParamCourses()
                {
                    id = id,
                    page = 1,
                    pageSize = 1
                };

                var course = useCQRS ? await _mediator.Send(new CourseQueries()
                {
                    searchParams = searchParameters
                }) : await _courseRepository.GetCourses(searchParameters);

                return Ok(useCQRS ? await _mediator.Send(new CourseCommand()
                {
                    Course = course.Dto.ElementAt(0),
                    OperationDB = Operation.Delete
                }) : await _courseRepository.DeleteCourse(id));
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
            var result = await _courseRepository.GetCourseDetails(new SearchParamCourses
            {
                CourseId = id,
                page = 1,
                pageSize = int.MaxValue
            });
            Reports.CreateReport(result, "CourseIndex.xls", false);

            _ = new FileExtensionContentTypeProvider().TryGetContentType("CourseIndex.xls", out string ? contentType);

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
                return Ok(await _courseRepository.PostFile(id, Request.Form.Files[0]));
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
                    return Ok(await _courseRepository.InsertCourseDetailsMasive(Reports.ReadCsv(filePath, id)));
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

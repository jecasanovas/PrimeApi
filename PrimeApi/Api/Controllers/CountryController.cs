using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using SelectPdf;
using BLL.Interfaces.Repositories;


namespace Courses.Api.Controllers
{
    public class CountryController : BaseApiController
    {
        private readonly IGenericRepository<Country> _countryRepository;

        public CountryController(IGenericRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }


        // GET: api/<CountryControlller>
        [HttpGet]
        [Authorize]
        [Route("Country")]
        public async Task<ActionResult> Country()
        {
            return Ok(await _countryRepository.ListAllAsync(CancellationToken.None));
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> TestAsync()
        {
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString("<html><head>test</head><body>funca!</body></html>");

            // save pdf document
            doc.Save(@"../files/test.pdf");

            // close pdf document
            doc.Close();

            var memory = new MemoryStream();
            using (var stream = new FileStream(@"../files/test.pdf", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", "Cours");
        }
    }
}

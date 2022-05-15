using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.DBContext;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using SelectPdf;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.Api.Controllers 
{
    public class CountryController : BaseApiController
    {
        private GestionCursosContext _context;

        public CountryController(GestionCursosContext context)
        {
            _context = context;
        }


        // GET: api/<CountryControlller>
        [HttpGet]
        [Authorize]
        public List<Country> Country()
        {
            return _context.Countries.ToList();


        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult>  TestAsync ()
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
            return File(memory,"application/pdf", "Cours");


        }

    }
    
}

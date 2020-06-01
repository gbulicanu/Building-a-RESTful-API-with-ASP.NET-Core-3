using CourseLibrary.API.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            this.courseLibraryRepository = courseLibraryRepository
                ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet]
        public IActionResult GetAuthors() 
        {
            var authors = this.courseLibraryRepository.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthors(Guid authorId)
        {
            var author = this.courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
    }
}

using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
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

            var authorsDtos = new List<AuthorDto>(); 
            foreach (var author in authors)
            {
                authorsDtos.Add(new AuthorDto
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    Age = author.DateOfBirth.GetCurrentAge(),
                    MainCategory = author.MainCategory
                });
            }
            return Ok(authorsDtos);
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

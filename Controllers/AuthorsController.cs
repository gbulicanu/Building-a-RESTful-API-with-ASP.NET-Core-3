using AutoMapper;

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
        private readonly IMapper mapper;

        public AuthorsController(
            ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository
                ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors() 
        {
            var authors = this.courseLibraryRepository.GetAuthors();

            return Ok(this.mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}")]
        public ActionResult<AuthorDto> GetAuthors(Guid authorId)
        {
            var author = this.courseLibraryRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<AuthorDto>(author));
        }
    }
}

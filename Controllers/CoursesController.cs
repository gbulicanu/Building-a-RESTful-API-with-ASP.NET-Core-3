using AutoMapper;

using CourseLibrary.API.Models;
using CourseLibrary.API.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper mapper;

        public CoursesController(
            ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository
                ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!this.courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var authorCourses = this.courseLibraryRepository.GetCourses(authorId);
            return Ok(this.mapper.Map<IEnumerable<CourseDto>>(authorCourses));
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!this.courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = this.courseLibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
            {
                return NotFound();
            }

            return this.mapper.Map<CourseDto>(course);
        }
    }
}

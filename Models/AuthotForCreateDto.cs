using System;

namespace CourseLibrary.API.Models
{
    public class AuthotForCreateDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }
    }
}
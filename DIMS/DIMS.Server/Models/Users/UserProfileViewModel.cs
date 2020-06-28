using AutoMapper;
using DIMS.BL.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DIMS.Server.Models
{
    [AutoMap(typeof(UserProfileDTO))]
    public class UserProfileViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Direction")]
        public int DirectionId { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The maximum Name's field length is 15 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^(([a-z0-9_-]+\.)*[a-z0-9_-]+){6,}@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Incorrect email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The maximum Last Name's field length is 15 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Only M (Male) or F (Female)")]
        [RegularExpression(@"[M,F]", ErrorMessage = "Only in format: M (Male) or F (Female)")]
        public string Sex { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "The maximum length of Education field is 250 characters.")]
        public string Education { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Range(0, 10, ErrorMessage = "University Average Score must be in range from 0 to 10")]
        public double UniversityAverageScore { get; set; }

        [Range(0, 10, ErrorMessage = "Math Score must be in range from 0 to 10")]
        public double MathScore { get; set; }

        [StringLength(100, ErrorMessage = "The maximum length of Address is 100 characters.")]
        public string Address { get; set; }

        [StringLength(13, ErrorMessage = "The maximum length of Mobile Phone is 13 characters (Format: +375xxxxxxxxx).")]
        public string MobilePhone { get; set; }

        [StringLength(50, ErrorMessage = "The maximum length of Skype is 50 characters.")]
        public string Skype { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
    }
}
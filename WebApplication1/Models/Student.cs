﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}

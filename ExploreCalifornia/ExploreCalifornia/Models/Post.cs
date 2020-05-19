﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        [Required]
        [StringLength(100,MinimumLength =5,
            ErrorMessage ="Title must be between 5 and 100 characters long")]
        [Display(Name ="Post Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        
        public string Author { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "Blog posts mush be at least 100 characters long")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime Posted { get; set; }

    }
}

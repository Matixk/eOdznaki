﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class Thread
    {
        public int Id { get; private set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime Created { get; private set; }

        [Required]
        public User Author { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public Thread() { }

        public Thread(int authorId, string title, User author)
        {
            AuthorId = authorId;
            Title = title;
            Created = DateTime.Now;
            Author = author;
            Posts = new List<Post>();
        }
    }
}

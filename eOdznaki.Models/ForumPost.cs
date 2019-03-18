using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Models
{
    public class ForumPost
    {
        public ForumPost()
        {
        }

        public ForumPost(int authorId, int forumThreadId, string content, User author, ForumThread forumThread)
        {
            AuthorId = authorId;
            ForumThreadId = forumThreadId;
            Content = content;
            Created = DateTime.Now;
            Author = author;
            ForumThread = forumThread;
        }

        public int Id { get; private set; }

        [Required] public int AuthorId { get; set; }

        [Required] public int ForumThreadId { get; set; }

        [Required] [MaxLength(2000)] public string Content { get; set; }

        public DateTime Created { get; private set; }

        [Required] public User Author { get; set; }

        [Required] public ForumThread ForumThread { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly DataContext context;

        public SearchRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Dictionary<ForumThread, IEnumerable<ForumPost>>> SearchForum(string regex)
        {
            regex = regex.ToLower();

            var posts = await context
                .ForumPosts
                .Include("ForumThread")
                .Where(f => f.Content.ToLower().Contains(regex))
                .ToListAsync();

            var threads = await context
                .ForumThreads
                .Where(f => f.Title.ToLower().Contains(regex))
                .ToDictionaryAsync(f => f, f => f.ForumPosts);

            posts.ForEach(post =>
            {
                var postThread = post.ForumThread;

                if (threads.ContainsKey(postThread))
                {
                    threads[postThread].ToList().Add(post);
                }
                else
                {
                    threads.Add(postThread, new List<ForumPost>() { post });
                }
            });

            return threads;
        }
    }
}

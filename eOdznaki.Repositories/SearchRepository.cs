using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
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

        public async Task<PagedList<ForumThread>> SearchForum(SearchParams searchParams)
        {
            var regex = searchParams.Regex.ToLower();

            var posts = await context
                .ForumPosts
                .Include("ForumThread")
                .Where(f => f.Content.ToLower().Contains(regex))
                .ToListAsync();

            var threads = await context
                .ForumThreads
                .Where(f => f.Title.ToLower().Contains(regex))
                .ToListAsync();

            posts.ForEach(post =>
            {
                var postThread = post.ForumThread;

                if (threads.Contains(postThread))
                    threads.Find(thread => postThread == thread).ForumPosts.Append(post);
                else
                {
                    postThread.ForumPosts.Append(post);
                    threads.Add(postThread);
                }
            });

            return PagedList<ForumThread>.CreateAsync(threads, searchParams.PageNumber,
                searchParams.PageSize);
        }
    }
}
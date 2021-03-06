﻿using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Models;

namespace eOdznaki.Interfaces
{
    public interface IForumThreadsRepository
    {
        Task<PagedList<ForumThread>> GetAllForumThreads(ForumThreadsParams forumThreadsParams);
        Task<ForumThread> GetForumThread(int forumThreadId);
        Task<ForumThread> Insert(ForumThreadForCreateDto forumThread);
        Task<ForumThread> Update(int userId, int forumThreadId, ForumThreadForUpdateDto forumThread, bool sudo);
        Task<ForumThread> Delete(int userId, int forumThreadId, bool sudo);
    }
}
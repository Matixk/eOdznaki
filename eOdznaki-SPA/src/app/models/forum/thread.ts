import {Post} from './post';

export interface Thread {
  id: number;
  authorId: number;
  authorName: string;
  title: string;
  created: Date;
  forumPosts: Post[];
}

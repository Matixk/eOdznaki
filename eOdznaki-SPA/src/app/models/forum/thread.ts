import {Post} from './post';
import {ForumEntity} from './forumEntity';

export interface Thread extends ForumEntity {
  title: string;
  forumPosts: Post[];
}

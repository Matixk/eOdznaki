import {User} from '../user/user';

export interface ForumEntity {
  id: number;
  authorId: number;
  authorName: string;
  authorAvatar: string;
  created: Date;
  author: User;
}

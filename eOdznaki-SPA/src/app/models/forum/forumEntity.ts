import {Author} from '../user/author';

export interface ForumEntity {
  id: number;
  author: Author;
  created: Date;
}

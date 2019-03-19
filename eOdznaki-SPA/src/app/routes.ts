import {Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';
import {PostComponent} from './components/post/post.component';
import {ThreadPostResolver} from './resolvers/threadPostResolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'forum', component: ForumComponent, resolve: { threads: ThreadsResolver }},
  { path: 'forum/:id', component: PostComponent, resolve: { posts: ThreadPostResolver }},
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

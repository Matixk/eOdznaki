import {Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'forum', component: ForumComponent, resolve: { threads: ThreadsResolver }},
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

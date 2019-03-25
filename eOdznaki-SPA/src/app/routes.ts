import {Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';
import {PostComponent} from './components/post/post.component';
import {ThreadPostResolver} from './resolvers/threadPostResolver';
import {SearchComponent} from './components/search/search.component';
import {SearchResolver} from './resolvers/searchResolver';
import {ProfileEditComponent} from './components/profile-edit/profile-edit.component';
import {PreventUnsavedChanged} from './_guards/prevent-unsaved-changes.guard';
import {ProfileEditResolver} from './resolvers/profile-edit-resolver';
import {AuthGuard} from './_guards/auth.guard';
import {UserRolesComponent} from './components/user-roles/user-roles.component';
import {UserRolesResolver} from './resolvers/user-roles-resolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'forum', component: ForumComponent, resolve: { threads: ThreadsResolver }},
  { path: 'forum/:id', component: PostComponent, resolve: { posts: ThreadPostResolver }},
  { path: 'search/:regex', component: SearchComponent, resolve: { threads: SearchResolver }},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'profile/edit', component: ProfileEditComponent,
        resolve: {user: ProfileEditResolver}, canDeactivate: [PreventUnsavedChanged],
      },
      {
        path: 'manage/roles', component: UserRolesComponent,
        resolve: { users: UserRolesResolver },
        data: { roles: ['Admin'] }
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

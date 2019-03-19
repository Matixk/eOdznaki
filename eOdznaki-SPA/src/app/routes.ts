import {Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';
import {ProfileEditComponent} from './components/profile-edit/profile-edit.component';
import {PreventUnsavedChanged} from './_guards/prevent-unsaved-changes.guard';
import {ProfileEditResolver} from './resolvers/profile-edit-resolver';
import {AuthGuard} from './_guards/auth.guard';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {path: 'home', component: HomeComponent},
  { path: 'forum', component: ForumComponent, resolve: { threads: ThreadsResolver }},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'profile/edit', component: ProfileEditComponent,
        resolve: {user: ProfileEditResolver}, canDeactivate: [PreventUnsavedChanged],
      },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

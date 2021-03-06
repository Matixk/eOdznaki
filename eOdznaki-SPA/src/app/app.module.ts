import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {MDBBootstrapModule} from 'angular-bootstrap-md';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TimeAgoPipe} from 'time-ago-pipe';
import {JwtModule} from '@auth0/angular-jwt';
import {CommonModule} from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ToastrModule} from 'ngx-toastr';
import {AgmCoreModule} from '@agm/core';

import {AppComponent} from './app.component';
import {NavbarComponent} from './components/navbar/navbar.component';
import {HomeComponent} from './components/home/home.component';
import {RegisterComponent} from './components/register/register.component';
import {appRoutes} from './routes';
import {RouterModule} from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {ErrorInterceptorProvider} from './_services/error.interceptor';
import { TrailComponent } from './components/trail/trail.component';
import {AgmDirectionModule} from 'agm-direction';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';
import {PostComponent} from './components/post/post.component';
import {ThreadPostResolver} from './resolvers/threadPostResolver';
import {SearchComponent} from './components/search/search.component';
import {SearchResolver} from './resolvers/searchResolver';
import {ProfileEditComponent} from './components/profile-edit/profile-edit.component';
import {ProfileEditResolver} from './resolvers/profile-edit-resolver';
import {PreventUnsavedChanged} from './_guards/prevent-unsaved-changes.guard';
import {AuthGuard} from './_guards/auth.guard';
import {FileUploadModule} from 'ng2-file-upload';
import {UserRolesComponent} from './components/user-roles/user-roles.component';
import {UserRolesResolver} from './resolvers/user-roles-resolver';
import {RolesModalComponent} from './components/roles-modal/roles-modal.component';
import {PaginationComponent} from './components/pagination/pagination.component';
import { LeaderboardComponent } from './components/leaderboard/leaderboard.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    TimeAgoPipe,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    TrailComponent,
    ForumComponent,
    PostComponent,
    SearchComponent,
    ProfileEditComponent,
    PaginationComponent,
    ProfileEditComponent,
    UserRolesComponent,
    RolesModalComponent,
    LeaderboardComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploadModule,
    MDBBootstrapModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCLfc_k5Cy5vdAxQjpdVSWV0XvmY0ImPUA',
      libraries: ['places']
    }),
    AgmDirectionModule,
  ],
  providers: [
    ErrorInterceptorProvider,
    ThreadsResolver,
    ThreadPostResolver,
    SearchResolver,
    ProfileEditResolver,
    ThreadsResolver,
    UserRolesResolver,
    AuthGuard,
    PreventUnsavedChanged
  ],
  entryComponents: [
    RolesModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

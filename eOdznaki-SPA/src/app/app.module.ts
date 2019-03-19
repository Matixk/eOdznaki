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


import {AppComponent} from './app.component';
import {NavbarComponent} from './components/navbar/navbar.component';
import {HomeComponent} from './components/home/home.component';
import {RegisterComponent} from './components/register/register.component';
import {appRoutes} from './routes';
import {RouterModule} from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {ErrorInterceptorProvider} from './_services/error.interceptor';
import {ForumComponent} from './components/forum/forum.component';
import {ThreadsResolver} from './resolvers/forumResolver';
import {ProfileEditComponent} from './components/profile-edit/profile-edit.component';
import {ProfileEditResolver} from './resolvers/profile-edit-resolver';
import {PreventUnsavedChanged} from './_guards/prevent-unsaved-changes.guard';
import {AuthGuard} from './_guards/auth.guard';

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
    ForumComponent,
    ProfileEditComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
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
  ],
  providers: [
    ErrorInterceptorProvider,
    ProfileEditResolver,
    ThreadsResolver,
    AuthGuard,
    PreventUnsavedChanged
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

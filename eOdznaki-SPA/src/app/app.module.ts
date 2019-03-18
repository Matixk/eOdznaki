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
import {ShContextMenuModule} from 'ng2-right-click-menu'

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
    ShContextMenuModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCLfc_k5Cy5vdAxQjpdVSWV0XvmY0ImPUA',
      libraries: ['places']
    }),
    AgmDirectionModule,
  ],
  providers: [
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

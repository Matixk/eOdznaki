import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TimeAgoPipe } from 'time-ago-pipe';
import { JwtModule } from '@auth0/angular-jwt';


import { AppComponent } from './app.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    TimeAgoPipe
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    MDBBootstrapModule.forRoot(),
    JwtModule.forRoot({
      config: {
         tokenGetter: tokenGetter,
         whitelistedDomains: ['localhost:5000', 'da-app-106.azurewebsites.net'],
         blacklistedRoutes: ['localhost:5000/api/auth', 'da-app-106.azurewebsites.net/api/auth']
      }
   })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

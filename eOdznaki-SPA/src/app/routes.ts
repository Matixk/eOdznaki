import {Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {TrailComponent} from './components/trail/trail.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'trail', component: TrailComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

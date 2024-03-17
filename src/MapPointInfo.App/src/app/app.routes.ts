import { Routes } from '@angular/router';

import {HomeComponent} from './home/home.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, children: [] },
    { path: 'Home', component: HomeComponent }
  ];


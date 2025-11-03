import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Home } from './components/home/home';
import { Liveorders } from './components/liveorders/liveorders';
import { Orderhistory } from './components/orderhistory/orderhistory';
import { Offers } from './components/offers/offers';
import { Products } from './components/products/products';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: Login },

  { path: 'register', component: Register },

  {
    path: 'home/:id',
    component: Home,
    children: [
      { path: 'liveorders', component: Liveorders },
      { path: 'orderhistory', component: Orderhistory },
      { path: 'offers', component: Offers },
      { path: 'products', component: Products },
    ]
  }
];

import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Home } from './components/home/home';
import { Liveorders } from './components/liveorders/liveorders';
import { Orderhistory } from './components/orderhistory/orderhistory';
import { Offers } from './components/offers/offers';
import { Products } from './components/products/products';
import { Admin} from './components/admin/admin';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full'},

  { path: 'login', component: Login },

  { path: 'register', component: Register },

  {path: "admin",component:Admin},

  {
    path: 'home/:id',
    component: Home,
    children: [
      { path: '', redirectTo: 'products', pathMatch: 'full'},
      { path: 'products', component: Products },
      { path: 'liveorders', component: Liveorders },
      { path: 'orderhistory', component: Orderhistory },
      { path: 'offers', component: Offers },
    ]
  }
];

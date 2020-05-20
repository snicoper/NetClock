import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error403Component } from './pages/errors/error403/error403.component';

import { PagesComponent } from './pages/pages.component';

const routes: Routes = [
  {
    /** Paginas con navbar. */
    path: '',
    component: PagesComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'accounts',
        loadChildren: () => import('./pages/accounts/accounts.module').then(m => m.AccountsModule)
      },
      {
        path: 'admin',
        loadChildren: () => import('./pages/admin/admin.module').then(m => m.AdminModule)
      },
      {
        path: 'errors/403',
        component: Error403Component,
        data: { title: 'Sin autorización' }
      }
    ]
  },

  /** Paginas sin navbar. */
  {
    path: 'auth',
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'errors',
    loadChildren: () => import('./pages/errors/errors.module').then(m => m.ErrorsModule)
  },
  {
    path: '**',
    redirectTo: 'errors',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

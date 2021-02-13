import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error404Component } from './pages/errors/error404/error404.component';
import { PagesComponent } from './pages/pages.component';

// http://example.com
const routes: Routes = [
  {
    /** Paginas con navbar. */
    path: '',
    component: PagesComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./pages/home/home.module').then((m) => m.HomeModule)
      },
      {
        path: 'accounts',
        loadChildren: () => import('./pages/accounts/accounts.module').then((m) => m.AccountsModule)
      },
      {
        path: 'admin',
        loadChildren: () => import('./pages/admin/admin.module').then((m) => m.AdminModule)
      },
      {
        path: 'errors',
        loadChildren: () => import('./pages/errors/errors.module').then((m) => m.ErrorsModule)
      }
    ]
  },

  /** Paginas sin navbar. */
  {
    path: 'auth',
    loadChildren: () => import('./pages/auth/auth.module').then((m) => m.AuthModule)
  },
  {
    path: 'error/404',
    component: Error404Component,
    data: { title: 'Pagina no encontrada' }
  },
  {
    path: '**',
    redirectTo: 'error/404',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled', relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}

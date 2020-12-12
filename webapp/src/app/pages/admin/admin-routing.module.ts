import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../guards';
import { AdminComponent } from './admin.component';

// http://example.com/admin
const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    data: { title: 'Administración' },
    children: [
      {
        path: '',
        component: AdminComponent
      },
      {
        path: 'accounts',
        loadChildren: () => import('./admin-accounts/admin-accounts.module').then(m => m.AdminAccountsModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}

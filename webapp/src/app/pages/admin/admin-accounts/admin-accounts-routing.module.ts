import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../../guards';
import { AdminAccountChangePasswordComponent } from './admin-account-change-password/admin-account-change-password.component';
import { AdminAccountCreateComponent } from './admin-account-create/admin-account-create.component';
import { AdminAccountDetailsComponent } from './admin-account-details/admin-account-details.component';
import { AdminAccountListComponent } from './admin-account-list/admin-account-list.component';
import { AdminAccountUpdateComponent } from './admin-account-update/admin-account-update.component';

// http://example.com/admin/accounts
const routes: Routes = [
  {
    path: '',
    component: AdminAccountListComponent,
    canActivate: [AuthGuard],
    data: { title: 'Lista de usuarios' }
  },
  {
    path: ':slug/details',
    component: AdminAccountDetailsComponent,
    canActivate: [AuthGuard],
    data: { title: 'Detalle de usuario' }
  },
  {
    path: ':slug/update',
    component: AdminAccountUpdateComponent,
    canActivate: [AuthGuard],
    data: { title: 'Editar usuario' }
  },
  {
    path: ':slug/change-password',
    component: AdminAccountChangePasswordComponent,
    canActivate: [AuthGuard],
    data: { title: 'Cambiar contraseña de usuario' }
  },
  {
    path: 'create',
    component: AdminAccountCreateComponent,
    canActivate: [AuthGuard],
    data: { title: 'Nuevo usuario' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminAccountsRoutingModule {
}

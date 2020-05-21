import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../guards';
import { AdminAccountCreateComponent } from './accounts/admin-account-create/admin-account-create.component';
import { AdminAccountDetailsComponent } from './accounts/admin-account-details/admin-account-details.component';
import { AdminAccountListComponent } from './accounts/admin-account-list/admin-account-list.component';
import { AdminAccountUpdateComponent } from './accounts/admin-account-update/admin-account-update.component';
import { AdminAccountChangePasswordComponent } from './accounts/admin-change-account-password/admin-account-change-password.component';
import { AdminComponent } from './admin.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    canActivate: [AuthGuard],
    data: { title: 'Administración' }
  },
  {
    path: 'accounts',
    component: AdminAccountListComponent,
    canActivate: [AuthGuard],
    data: { title: 'Lista de usuarios' }
  },
  {
    path: 'accounts/:slug/details',
    component: AdminAccountDetailsComponent,
    canActivate: [AuthGuard],
    data: { title: 'Detalle de usuario' }
  },
  {
    path: 'accounts/:slug/update',
    component: AdminAccountUpdateComponent,
    canActivate: [AuthGuard],
    data: { title: 'Editar usuario' }
  },
  {
    path: 'accounts/:slug/change-password',
    component: AdminAccountChangePasswordComponent,
    canActivate: [AuthGuard],
    data: { title: 'Cambiar contraseña de usuario' }
  },
  {
    path: 'accounts/create',
    component: AdminAccountCreateComponent,
    canActivate: [AuthGuard],
    data: { title: 'Nuevo usuario' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}

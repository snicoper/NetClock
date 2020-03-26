import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../guards';
import { AdminUserChangePasswordComponent } from './accounts/admin-change-user-password/admin-user-change-password.component';
import { AdminUserCreateComponent } from './accounts/admin-user-create/admin-user-create.component';
import { AdminUserDetailsComponent } from './accounts/admin-user-details/admin-user-details.component';
import { AdminUserListComponent } from './accounts/admin-user-list/admin-user-list.component';
import { AdminUserUpdateComponent } from './accounts/admin-user-update/admin-user-update.component';
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
    component: AdminUserListComponent,
    canActivate: [AuthGuard],
    data: { title: 'Lista de usuarios' }
  },
  {
    path: 'accounts/:slug/details',
    component: AdminUserDetailsComponent,
    canActivate: [AuthGuard],
    data: { title: 'Detalle de usuario' }
  },
  {
    path: 'accounts/:slug/update',
    component: AdminUserUpdateComponent,
    canActivate: [AuthGuard],
    data: { title: 'Editar usuario' }
  },
  {
    path: 'accounts/:slug/change-password',
    component: AdminUserChangePasswordComponent,
    canActivate: [AuthGuard],
    data: { title: 'Cambiar contraseña de usuario' }
  },
  {
    path: 'accounts/create',
    component: AdminUserCreateComponent,
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

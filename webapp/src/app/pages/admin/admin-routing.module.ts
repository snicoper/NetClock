import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../guards';
import { UserDetailsComponent } from './accounts/user-details/user-details.component';
import { UserListComponent } from './accounts/user-list/user-list.component';
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
    component: UserListComponent,
    canActivate: [AuthGuard],
    data: { title: 'Lista de usuarios' }
  },
  {
    path: 'accounts/:slug',
    component: UserDetailsComponent,
    canActivate: [AuthGuard],
    data: { title: 'Detalle de ussuario' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}

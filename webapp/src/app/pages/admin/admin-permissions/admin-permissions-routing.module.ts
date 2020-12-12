import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminAccountListComponent } from '../admin-accounts/admin-account-list/admin-account-list.component';

const routes: Routes = [
  {
    path: '',
    component: AdminAccountListComponent,
    data: { title: 'Lista de usuarios' }
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPermissionsRoutingModule {
}

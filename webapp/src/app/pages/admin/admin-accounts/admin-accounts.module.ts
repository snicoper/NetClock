import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { ComponentsModule } from '../../../components/components.module';
import { DirectivesModule } from '../../../core/directives/directives.module';
import { PipesModule } from '../../../core/pipes/pipes.module';
import { AdminAccountChangePasswordComponent } from './admin-account-change-password/admin-account-change-password.component';
import { AdminAccountCreateComponent } from './admin-account-create/admin-account-create.component';
import { AdminAccountDetailsComponent } from './admin-account-details/admin-account-details.component';
import { AdminAccountListComponent } from './admin-account-list/admin-account-list.component';
import { AdminAccountUpdateComponent } from './admin-account-update/admin-account-update.component';
import { AdminAccountsRoutingModule } from './admin-accounts-routing.module';

@NgModule({
  declarations: [
    AdminAccountChangePasswordComponent,
    AdminAccountCreateComponent,
    AdminAccountDetailsComponent,
    AdminAccountListComponent,
    AdminAccountUpdateComponent
  ],
  imports: [
    CommonModule,
    AdminAccountsRoutingModule,
    ComponentsModule,
    PipesModule,
    DirectivesModule,
    ReactiveFormsModule
  ]
})
export class AdminAccountsModule {
}

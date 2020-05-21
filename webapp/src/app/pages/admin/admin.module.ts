import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { ComponentsModule } from '../../components/components.module';
import { DirectivesModule } from '../../directives/directives.module';
import { PipesModule } from '../../pipes/pipes.module';
import { AdminAccountCreateComponent } from './accounts/admin-account-create/admin-account-create.component';
import { AdminAccountDetailsComponent } from './accounts/admin-account-details/admin-account-details.component';
import { AdminAccountListComponent } from './accounts/admin-account-list/admin-account-list.component';
import { AdminAccountUpdateComponent } from './accounts/admin-account-update/admin-account-update.component';
import { AdminAccountChangePasswordComponent } from './accounts/admin-change-account-password/admin-account-change-password.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';

@NgModule({
  declarations: [
    AdminComponent,
    AdminAccountListComponent,
    AdminAccountDetailsComponent,
    AdminAccountCreateComponent,
    AdminAccountUpdateComponent,
    AdminAccountChangePasswordComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ComponentsModule,
    PipesModule,
    DirectivesModule,
    ReactiveFormsModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AdminModule {
}

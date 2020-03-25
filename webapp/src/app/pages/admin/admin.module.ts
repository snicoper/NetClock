import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { ComponentsModule } from '../../components/components.module';
import { DirectivesModule } from '../../directives/directives.module';
import { PipesModule } from '../../pipes/pipes.module';
import { AdminUserCreateComponent } from './accounts/admin-user-create/admin-user-create.component';
import { AdminUserDetailsComponent } from './accounts/admin-user-details/admin-user-details.component';
import { AdminUserUpdateComponent } from './accounts/admin-user-update/admin-user-update.component';
import { AdminUserListComponent } from './accounts/admin-user-list/admin-user-list.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';

@NgModule({
  declarations: [
    AdminComponent,
    AdminUserListComponent,
    AdminUserDetailsComponent,
    AdminUserCreateComponent,
    AdminUserUpdateComponent
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

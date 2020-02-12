import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { ComponentsModule } from '../../components/components.module';
import { AccountsRoutingModule } from './accounts-routing.module';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    ProfileComponent,
    ChangePasswordComponent,
  ],
  imports: [
    AccountsRoutingModule,
    CommonModule,
    ComponentsModule,
    ReactiveFormsModule,
  ],
})
export class AccountsModule { }

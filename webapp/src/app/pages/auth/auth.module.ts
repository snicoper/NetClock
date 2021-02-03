import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from '../../components/components.module';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { RecoveryPasswordSuccessComponent } from './recovery-password-success/recovery-password-success.component';
import { RecoveryPasswordValidateComponent } from './recovery-password-validate/recovery-password-validate.component';
import { RecoveryPasswordComponent } from './recovery-password/recovery-password.component';

@NgModule({
  declarations: [
    LoginComponent,
    RecoveryPasswordComponent,
    RecoveryPasswordSuccessComponent,
    RecoveryPasswordValidateComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule
  ]
})
export class AuthModule {
}

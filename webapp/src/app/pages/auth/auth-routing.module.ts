import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RecoveryPasswordSuccessComponent } from './recovery-password-success/recovery-password-success.component';
import { RecoveryPasswordValidateComponent } from './recovery-password-validate/recovery-password-validate.component';
import { RecoveryPasswordComponent } from './recovery-password/recovery-password.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Iniciar sesión' }
  },
  {
    path: 'recovery-password',
    component: RecoveryPasswordComponent,
    data: { title: 'Recuperar contraseña' }
  },
  {
    path: 'recovery-password/success',
    component: RecoveryPasswordSuccessComponent,
    data: { title: 'Recuperación de contraseña enviada con éxito' }
  },
  {
    path: 'recovery-password/validate',
    component: RecoveryPasswordValidateComponent,
    data: { title: 'Validación de recuperación de contraseña' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../../config';
import { PasswordMustMatch } from '../../../validators';
import { AuthService } from '../../auth/services/auth.service';
import { ChangePasswordModel } from '../models';
import { AccountsService } from '../services/accounts.service';

@Component({
  selector: 'nc-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  errors = {};
  submitted = false;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private accountsService: AccountsService,
    private authService: AuthService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.setBreadcrumb();
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;
    this.errors = {};

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    const changePasswordModel = Object.assign(new ChangePasswordModel(), this.form.value) as ChangePasswordModel;
    changePasswordModel.id = this.authService.currentUserValue.id;

    this.accountsService.changePassword(changePasswordModel)
      .pipe(finalize(() => this.loading = false))
      .subscribe(() => {
          this.toastrService.success('Contraseña cambiada con éxito.');
          this.authService.logout();
        },
        (errors: any) => {
          this.errors = errors.error;
        }
      )
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Perfil', UrlsApp.accounts, 'fas fa-user-cog')
      .add('Cambiar contraseña', UrlsApp.changePassword, 'fas fa-user-lock', false)
  }

  private buildForm(): void {
    this.form = this.fb.group({
        oldPassword: new FormControl('', [Validators.required]),
        newPassword: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      },
      {
        validators: PasswordMustMatch('newPassword', 'confirmPassword')
      });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../components/breadcrumb/models/BreadcrumbCollection';
import { FormInputTypes } from '../../../components/forms/form-input/form-input-types.enum';
import { SiteUrls } from '../../../core';
import { PasswordMustMatch } from '../../../validators';
import { AuthRestService } from '../../auth/services/auth-rest.service';
import { AccountsRestService } from '../services/accounts-rest.service';
import { ChangePasswordModel } from './change-password.model';

@Component({
  selector: 'nc-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  errors = [];
  submitted = false;
  loading = false;
  formTypes = FormInputTypes;

  constructor(
    private fb: FormBuilder,
    private accountsService: AccountsRestService,
    private authService: AuthRestService,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.setBreadcrumb();
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;
    this.errors = [];

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
      );
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Perfil', SiteUrls.accounts, 'fas fa-user-cog')
      .add('Cambiar contraseña', SiteUrls.changePassword, 'fas fa-user-lock', false);
  }

  private buildForm(): void {
    this.form = this.fb.group({
        oldPassword: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      },
      {
        validators: PasswordMustMatch('newPassword', 'confirmPassword')
      });
  }
}

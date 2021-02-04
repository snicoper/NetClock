import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { BreadcrumbCollection } from '../../../components/breadcrumb/breadcrumb-collection';
import { FormInputTypes } from '../../../components/forms/form-input/form-input-types.enum';
import { siteUrls } from '../../../core/common';
import { passwordMustMatch } from '../../../core/validators';
import { AuthService } from '../../auth/login/auth.service';
import { ChangePasswordModel } from './change-password.model';
import { ChangePasswordService } from './change-password.service';

@Component({
  selector: 'nc-change-password',
  templateUrl: './change-password.component.html',
  providers: [ChangePasswordService]
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
    private changePasswordService: ChangePasswordService,
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
    this.errors = [];

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    const changePasswordModel = Object.assign(new ChangePasswordModel(), this.form.value) as ChangePasswordModel;
    changePasswordModel.id = this.authService.currentUserValue.id;

    this.changePasswordService.change(changePasswordModel)
      .pipe(
        finalize(() => this.loading = false)
      )
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
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Perfil', siteUrls.accountsProfile, 'fas fa-user-cog')
      .add('Cambiar contraseña', siteUrls.accountsChangePassword, 'fas fa-user-lock', false);
  }

  private buildForm(): void {
    const formOptions: AbstractControlOptions = {
      validators: () => passwordMustMatch('password', 'newPassword')
    };

    this.form = this.fb.group({
        oldPassword: new FormControl('', [Validators.required]),
        newPassword: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      }, formOptions);
  }
}

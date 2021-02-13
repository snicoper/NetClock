import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { BreadcrumbCollection } from '../../../../components/breadcrumb/breadcrumb-collection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { siteUrls } from '../../../../core/common';
import { BadRequest } from '../../../../core/types';
import { passwordMustMatch } from '../../../../core/validators';
import { AdminAccountDetailsModel } from '../admin-account-details/admin-account-details.model';
import { AdminAccountChangePasswordModel } from './admin-account-change-password.model';
import { AdminAccountChangePasswordService } from './admin-account-change-password.service';

@Component({
  selector: 'nc-admin-change-user-password',
  templateUrl: './admin-account-change-password.component.html',
  providers: [AdminAccountChangePasswordService]
})
export class AdminAccountChangePasswordComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  badRequest: BadRequest;
  submitted = false;
  loading = false;
  updating = false;
  formTypes = FormInputTypes;

  private readonly slug: string;
  private user: AdminAccountDetailsModel;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private adminUserChangePasswordService: AdminAccountChangePasswordService
  ) {
    this.slug = this.route.snapshot.paramMap.get('slug');
  }

  ngOnInit(): void {
    this.loadUser();
    this.buildForm();
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.updating = true;
    this.badRequest = null;
    const model = Object.assign(new AdminAccountChangePasswordModel(), this.form.value);
    model.id = this.user.id;

    this.adminUserChangePasswordService
      .change(model)
      .pipe(finalize(() => (this.updating = false)))
      .subscribe(
        () => {
          this.toastrService.success('Contraseña actualizada con éxito');
          const url = siteUrls.replace(siteUrls.adminAccountsDetails, { slug: this.user.slug });

          this.router.navigate([url]);
        },
        (error: BadRequest) => {
          this.badRequest = error;
        }
      );
  }

  private loadUser(): void {
    this.loading = true;
    this.adminUserChangePasswordService
      .getBy(this.slug)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe((result: AdminAccountDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }

  private setBreadcrumb(): void {
    const urlUserDetails = siteUrls.replace(siteUrls.adminAccountsDetails, { slug: this.slug });
    const fullName = `${this.user.firstName} ${this.user.lastName}`;

    this.breadcrumb
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Administración', siteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', siteUrls.adminAccounts, 'fas fa-users')
      .add(fullName, urlUserDetails, 'fas fa-user')
      .add('Cambiar contraseña', siteUrls.adminAccountsCreate, 'fas fa-user-edit', false);
  }

  private buildForm(): void {
    const formOptions: AbstractControlOptions = {
      validators: () => passwordMustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group(
      {
        newPassword: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      },
      formOptions
    );
  }
}

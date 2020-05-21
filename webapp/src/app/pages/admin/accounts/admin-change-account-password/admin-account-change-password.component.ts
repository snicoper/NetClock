import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { SiteUrls } from '../../../../core';
import { PasswordMustMatch } from '../../../../validators';
import { AdminAccountDetailsModel } from '../admin-account-details/admin-account-details.model';
import { AdminAccountChangePasswordService } from './admin-account-change-password.service';
import { AdminAccountChangePasswordModel } from './admin-account-changepassword.model';

@Component({
  selector: 'nc-admin-change-user-password',
  templateUrl: './admin-account-change-password.component.html',
  providers: [AdminAccountChangePasswordService]
})
export class AdminAccountChangePasswordComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  errors = [];
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
    this.errors = [];
    const model = Object.assign(new AdminAccountChangePasswordModel(), this.form.value);
    model.id = this.user.id;

    this.adminUserChangePasswordService.change(model)
      .pipe(finalize(() => this.updating = false))
      .subscribe(() => {
        this.toastrService.success('Contraseña actualizada con éxito');
        const url = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: this.user.slug });

        this.router.navigate([url]);
      });
  }

  private loadUser(): void {
    this.loading = true;
    this.adminUserChangePasswordService.getBy(this.slug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminAccountDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }

  private setBreadcrumb(): void {
    const urlUserDetails = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: this.slug });
    const fullName = `${this.user.firstName} ${this.user.lastName}`;

    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminUserList, 'fas fa-users')
      .add(fullName, urlUserDetails, 'fas fa-user')
      .add('Cambiar contraseña', SiteUrls.adminUserCreate, 'fas fa-user-edit', false);
  }

  private buildForm(): void {
    this.form = this.fb.group({
        newPassword: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      },
      {
        validators: PasswordMustMatch('newPassword', 'confirmPassword')
      });
  }
}

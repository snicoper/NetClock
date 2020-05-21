import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as HttpStatus from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { SiteUrls } from '../../../../core';
import { DebugService } from '../../../../services';
import { PasswordMustMatch } from '../../../../validators';
import { AdminAccountCreateResult } from './admin-account-create-result.model';
import { AdminAccountCreateService } from './admin-account-create.service';

@Component({
  selector: 'nc-user-create',
  templateUrl: './admin-account-create.component.html',
  providers: [AdminAccountCreateService]
})
export class AdminAccountCreateComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  errors = [];
  submitted = false;
  loading = false;
  formTypes = FormInputTypes;

  constructor(
    private fb: FormBuilder,
    private adminUserCreateService: AdminAccountCreateService,
    private router: Router,
    private toastrService: ToastrService,
    private debugService: DebugService
  ) {
  }

  ngOnInit(): void {
    this.setBreadcrumb();
    this.buildForm();
  }

  submit(): void {
    this.submitted = true;
    if (!this.form.valid) {
      return;
    }

    this.loading = true;
    this.adminUserCreateService.create(this.form.value)
      .pipe(
        finalize(() => this.loading = false)
      )
      .subscribe((result: AdminAccountCreateResult) => {
        const url = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: result.slug });
        this.toastrService.success('Usuario creado con éxito');
        this.router.navigate([url]);
      }, ((error) => {
        this.debugService.errors(error.error);
        if (error.status === HttpStatus.BAD_REQUEST) {
          this.errors = error.error;
        }
      }));
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminUserList, 'fas fa-users')
      .add('Nuevo', SiteUrls.adminUserCreate, 'fas fa-user-plus', false);
  }

  private buildForm(): void {
    this.form = this.fb.group({
        userName: new FormControl('', [Validators.required]),
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required]),
        active: new FormControl(true)
      },
      {
        validators: PasswordMustMatch('password', 'confirmPassword')
      });
  }
}

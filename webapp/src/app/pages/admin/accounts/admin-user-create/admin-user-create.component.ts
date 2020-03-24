import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as HttpStatus from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { UrlsApp } from '../../../../config';
import { DebugService } from '../../../../services';
import { PasswordMustMatch } from '../../../../validators';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';

@Component({
  selector: 'nc-user-create',
  templateUrl: './admin-user-create.component.html'
})
export class AdminUserCreateComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  form: FormGroup;
  errors = [];
  submitted = false;
  loading = false;
  formTypes = FormInputTypes;

  constructor(
    private fb: FormBuilder,
    private adminAccountsService: AdminAccountsRestService,
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
    this.adminAccountsService.create(this.form.value)
      .pipe(
        finalize(() => this.loading = false)
      )
      .subscribe((result) => {
        const url = UrlsApp.replace(UrlsApp.adminUserDetails, { slug: result.slug });
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
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Usuarios', UrlsApp.adminUserList, 'fas fa-users')
      .add('Nuevo', UrlsApp.adminUserCreate, 'fas fa-user-plus', false);
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

import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StatusCodes } from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { BreadcrumbCollection } from '../../../../components/breadcrumb/breadcrumb-collection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { siteUrls } from '../../../../core/common';
import { BadRequest } from '../../../../core/types';
import { passwordMustMatch } from '../../../../core/validators';
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
  badRequest: BadRequest;
  submitted = false;
  loading = false;
  formInputTypes = FormInputTypes;

  constructor(
    private fb: FormBuilder,
    private adminUserCreateService: AdminAccountCreateService,
    private router: Router,
    private toastrService: ToastrService
  ) {}

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
    this.adminUserCreateService
      .create(this.form.value)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(
        (result: AdminAccountCreateResult) => {
          const url = siteUrls.replace(siteUrls.adminAccountsDetails, { slug: result.slug });
          this.toastrService.success('Usuario creado con éxito');
          this.router.navigate([url]);
        },
        (error) => {
          if (error.status === StatusCodes.BAD_REQUEST) {
            this.badRequest = error.error;
          }
        }
      );
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Administración', siteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', siteUrls.adminAccounts, 'fas fa-users')
      .add('Nuevo', siteUrls.adminAccountsCreate, 'fas fa-user-plus', false);
  }

  private buildForm(): void {
    const formOptions: AbstractControlOptions = {
      validators: () => passwordMustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group(
      {
        userName: new FormControl('', [Validators.required]),
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required]),
        active: new FormControl(true)
      },
      formOptions
    );
  }
}

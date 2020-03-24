import { DEBUG } from '@angular/compiler-cli/ngcc/src/logging/console_logger';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as HttpStatus from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { UrlsApp } from '../../../../config';
import { DebugService } from '../../../../services';
import { AdminUserEditCreateModel } from '../models';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';

@Component({
  selector: 'nc-admin-user-edit',
  templateUrl: './admin-user-edit.component.html'
})
export class AdminUserEditComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  data: AdminUserEditCreateModel;
  form: FormGroup;
  submitted = false;
  loading = false;
  formTypes = FormInputTypes;
  errors = [];
  userNotFound = false;
  slug: string;

  constructor(
    private adminAccountsService: AdminAccountsRestService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private debugService: DebugService
  ) {
    this.slug = this.route.snapshot.paramMap.get('slug');
  }

  ngOnInit(): void {
    this.setBreadcrumb();
    this.loadData();
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.data = Object.assign(this.data, this.form.value);

    this.adminAccountsService.update(this.data.id, this.data)
      .subscribe((result: AdminUserEditCreateModel) => {
        const url = UrlsApp.replace(UrlsApp.adminUserDetails, { slug: result.slug });
        this.toastrService.success('Usuario editado con éxito');
        this.router.navigate([url]);
      }, ((error) => {
        this.debugService.errors(error.error);
        if (error.status === HttpStatus.BAD_REQUEST) {
          this.errors = error.error;
        }
      }));
  }

  private setBreadcrumb(): void {
    const urlUserDetails = UrlsApp.replace(UrlsApp.adminUserDetails, { slug: this.slug });

    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Usuarios', UrlsApp.adminUserList, 'fas fa-users')
      .add(this.slug, urlUserDetails, 'fas fa-user')
      .add('Editar', UrlsApp.adminUserCreate, 'fas fa-user-edit', false);
  }

  private loadData(): void {
    this.loading = true;
    this.adminAccountsService.getBy(this.slug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminUserEditCreateModel) => {
        this.data = result;
        this.buildForm();
      }, (errors) => {
        this.debugService.errors(errors);
        if (errors.status === HttpStatus.NOT_FOUND) {
          this.userNotFound = true;
        }
      });
  }

  private buildForm(): void {
    this.form = this.fb.group({
      userName: new FormControl(this.data.userName || '', [Validators.required]),
      firstName: new FormControl(this.data.firstName || '', [Validators.required]),
      lastName: new FormControl(this.data.lastName || '', [Validators.required]),
      email: new FormControl(this.data.email || '', [Validators.required]),
      active: new FormControl(this.data.active)
    });
  }
}

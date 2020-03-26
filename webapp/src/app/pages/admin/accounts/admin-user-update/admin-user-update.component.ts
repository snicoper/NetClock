import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as HttpStatus from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { SiteUrls } from '../../../../core';
import { DebugService } from '../../../../services';
import { AdminUserUpdateResultModel } from './admin-user-update-result.model';
import { AdminUserUpdateModel } from './admin-user-update.model';
import { AdminUserUpdateService } from './admin-user-update.service';

@Component({
  selector: 'nc-admin-user-edit',
  templateUrl: './admin-user-update.component.html',
  providers: [AdminUserUpdateService]
})
export class AdminUserUpdateComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  data: AdminUserUpdateModel;
  form: FormGroup;
  submitted = false;
  updating = false;
  loading = false;
  formTypes = FormInputTypes;
  errors = [];
  userNotFound = false;
  slug: string;

  constructor(
    private adminUserUpdateService: AdminUserUpdateService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private debugService: DebugService
  ) {
    this.slug = this.route.snapshot.paramMap.get('slug');
  }

  ngOnInit(): void {
    this.loadData();
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.updating = true;
    this.data = Object.assign(this.data, this.form.value);

    this.adminUserUpdateService.update(this.data.id, this.data)
      .pipe(finalize(() => this.updating = false))
      .subscribe((result: AdminUserUpdateResultModel) => {
        this.toastrService.success('Usuario editado con éxito');
        const url = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: result.slug });
        this.router.navigate([url]);
      }, ((error) => {
        this.debugService.errors(error.error);
        if (error.status === HttpStatus.BAD_REQUEST) {
          this.errors = error.error;
        }
      }));
  }

  private loadData(): void {
    this.loading = true;
    this.adminUserUpdateService.getBy(this.slug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminUserUpdateModel) => {
        this.data = result;
        this.setBreadcrumb();
        this.buildForm();
      }, (errors) => {
        this.debugService.errors(errors);
        if (errors.status === HttpStatus.NOT_FOUND) {
          this.userNotFound = true;
        }
      });
  }

  private setBreadcrumb(): void {
    const urlUserDetails = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: this.slug });
    const fullName = `${this.data.firstName} ${this.data.lastName}`;

    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminUserList, 'fas fa-users')
      .add(fullName, urlUserDetails, 'fas fa-user')
      .add('Editar', SiteUrls.adminUserCreate, 'fas fa-user-edit', false);
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
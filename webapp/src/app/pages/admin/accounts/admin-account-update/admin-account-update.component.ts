import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusCodes } from 'http-status-codes';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { FormInputTypes } from '../../../../components/forms/form-input/form-input-types.enum';
import { SiteUrls } from '../../../../core';
import { BadRequest } from '../../../../types';
import { AdminAccountUpdateResultModel } from './admin-account-update-result.model';
import { AdminAccountUpdateModel } from './admin-account-update.model';
import { AdminAccountUpdateService } from './admin-account-update.service';

@Component({
  selector: 'nc-admin-user-edit',
  templateUrl: './admin-account-update.component.html',
  providers: [AdminAccountUpdateService]
})
export class AdminAccountUpdateComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  data: AdminAccountUpdateModel;
  form: FormGroup;
  submitted = false;
  updating = false;
  loading = false;
  formTypes = FormInputTypes;
  badRequest: BadRequest;
  accountNotFound = false;
  slug: string;

  constructor(
    private adminUserUpdateService: AdminAccountUpdateService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private toastrService: ToastrService
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

    this.adminUserUpdateService.update(this.data)
      .pipe(
        finalize(() => this.updating = false)
      )
      .subscribe((result: AdminAccountUpdateResultModel) => {
        this.toastrService.success('Usuario editado con éxito');
        const url = SiteUrls.replace(SiteUrls.adminAccountsDetails, { slug: result.slug });
        this.router.navigate([url]);
      }, ((error) => {
        if (error.status === StatusCodes.BAD_REQUEST) {
          this.badRequest = error.error;
        }
      }));
  }

  private loadData(): void {
    this.loading = true;
    this.adminUserUpdateService.getBy(this.slug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminAccountUpdateModel) => {
        this.data = result;
        this.setBreadcrumb();
        this.buildForm();
      }, (errors) => {
        if (errors.status === StatusCodes.NOT_FOUND) {
          this.accountNotFound = true;
        }
      });
  }

  private setBreadcrumb(): void {
    const urlUserDetails = SiteUrls.replace(SiteUrls.adminAccountsDetails, { slug: this.slug });
    const fullName = `${this.data.firstName} ${this.data.lastName}`;

    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminAccounts, 'fas fa-users')
      .add(fullName, urlUserDetails, 'fas fa-user')
      .add('Editar', SiteUrls.adminAccountsCreate, 'fas fa-user-edit', false);
  }

  private buildForm(): void {
    this.form = this.fb.group({
      userName: new FormControl(this.data.userName || '', [Validators.required]),
      firstName: new FormControl(this.data.firstName || '', [Validators.required]),
      lastName: new FormControl(this.data.lastName || '', [Validators.required]),
      email: new FormControl(this.data.email || '', [Validators.required, Validators.email]),
      active: new FormControl(this.data.active)
    });
  }
}

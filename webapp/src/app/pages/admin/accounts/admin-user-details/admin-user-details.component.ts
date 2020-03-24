import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../../../config';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';
import { AdminUserDetailsModel } from './admin-user-details.model';

@Component({
  selector: 'nc-user-details',
  templateUrl: './admin-user-details.component.html'
})
export class AdminUserDetailsComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  user: AdminUserDetailsModel;
  loading = false;

  private readonly userSlug: string;

  constructor(
    private adminAccountsService: AdminAccountsRestService,
    private route: ActivatedRoute
  ) {
    this.userSlug = this.route.snapshot.paramMap.get('slug');
  }

  ngOnInit(): void {
    this.loadUser();
  }

  private setBreadcrumb(): void {
    const fullName = `${this.user.firstName} ${this.user.lastName}`;

    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Usuarios', UrlsApp.adminUserList, 'fas fa-users')
      .add(fullName, UrlsApp.adminUserList, 'fas fa-user', false);
  }

  private loadUser(): void {
    this.loading = true;
    this.adminAccountsService.getBy(this.userSlug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminUserDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }
}

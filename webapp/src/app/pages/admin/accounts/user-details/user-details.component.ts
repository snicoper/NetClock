import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../../../config';
import { AdminUserDetailsModel } from '../models';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';

@Component({
  selector: 'nc-user-details',
  templateUrl: './user-details.component.html'
})
export class UserDetailsComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  user: AdminUserDetailsModel;
  loading = false;

  private readonly userSlug: string;

  constructor(
    private adminAccountsService: AdminAccountsRestService,
    private route: ActivatedRoute
  ) {
    this.userSlug = this.route.snapshot.paramMap.get('slug');
    this.loadUser();
  }

  ngOnInit(): void {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Lista de usuarios', UrlsApp.adminUserList, 'fas fa-users')
      .add(this.userSlug, UrlsApp.adminUserList, 'fas fa-user', false);
  }

  private loadUser(): void {
    this.loading = true;
    this.adminAccountsService.getBy(this.userSlug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminUserDetailsModel) => {
        this.user = result;
      });
  }
}

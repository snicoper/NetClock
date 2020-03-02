import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/core';
import { UrlsApp } from '../../../../config';
import { ApiResult } from '../../../../models';
import { AdminUserListModel } from '../models';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';
import { UserListHeaderConfig } from './user-list-headers.config';

@Component({
  selector: 'nc-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, OnDestroy {
  breadcrumb = new BreadcrumbCollection();
  transferData: ApiResult<AdminUserListModel>;
  tableHeaderConfig = new TableHeaderConfig();
  loading = false;

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminAccountsService: AdminAccountsRestService
  ) {
  }

  ngOnInit(): void {
    this.configureTableHeaders();
    this.setBreadcrumb();
    this.loadUsers();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSelectItem(user: AdminUserListModel): void {
    const url = UrlsApp.replace(UrlsApp.adminUserDetails, { slug: user.slug });
    this.router.navigate([url]);
  }

  onReloadData(): void {
    this.loadUsers();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig.addRange(UserListHeaderConfig);
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Lista de usuarios', UrlsApp.adminUserList, 'fas fa-users', false);
  }

  private loadUsers(): void {
    this.loading = true;
    this.adminAccountsService.getAllPaginated(this.transferData)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: ApiResult<AdminUserListModel>) => {
        this.transferData = result;
      });
  }
}

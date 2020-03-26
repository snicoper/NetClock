import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/table-header.config';
import { SiteUrls } from '../../../../core';
import { ApiResult } from '../../../../models';
import { AdminUserListHeaderConfig } from './admin-user-list-headers.config';
import { AdminUserListModel } from './admin-user-list.model';
import { AdminUserListService } from './admin-user-list.service';

@Component({
  selector: 'nc-user-list',
  templateUrl: './admin-user-list.component.html',
  providers: [AdminUserListService]
})
export class AdminUserListComponent implements OnInit, OnDestroy {
  breadcrumb = new BreadcrumbCollection();
  apiResult: ApiResult<AdminUserListModel>;
  tableHeaderConfig = new TableHeaderConfig();
  loading = false;

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminUserListService: AdminUserListService
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
    const url = SiteUrls.replace(SiteUrls.adminUserDetails, { slug: user.slug });
    this.router.navigate([url]);
  }

  onReloadData(): void {
    this.loadUsers();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig.addRange(AdminUserListHeaderConfig);
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminUserList, 'fas fa-users', false);
  }

  private loadUsers(): void {
    this.loading = true;
    this.adminUserListService.getAllPaginated(this.apiResult)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: ApiResult<AdminUserListModel>) => {
        this.apiResult = result;
      });
  }
}

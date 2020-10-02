import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/table-header.config';
import { DebugErrors, SiteUrls } from '../../../../core';
import { ApiResult } from '../../../../models';
import { AdminAccountListHeaderConfig } from './admin-account-list-headers.config';
import { AdminAccountListModel } from './admin-account-list.model';
import { AdminAccountListService } from './admin-account-list.service';

@Component({
  selector: 'nc-user-list',
  templateUrl: './admin-account-list.component.html',
  providers: [AdminAccountListService]
})
export class AdminAccountListComponent implements OnInit, OnDestroy {
  breadcrumb = new BreadcrumbCollection();
  apiResult: ApiResult<AdminAccountListModel>;
  tableHeaderConfig = new TableHeaderConfig();
  siteUrls = SiteUrls;
  loading = false;

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminUserListService: AdminAccountListService
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

  onSelectItem(user: AdminAccountListModel): void {
    const url = SiteUrls.replace(SiteUrls.adminAccountsDetails, { slug: user.slug });
    this.router.navigate([url]);
  }

  onReloadData(): void {
    this.loadUsers();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig.addRange(AdminAccountListHeaderConfig);
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminAccounts, 'fas fa-users', false);
  }

  private loadUsers(): void {
    this.loading = true;
    this.adminUserListService.getAllPaginated(this.apiResult)
      .pipe(
        finalize(() => this.loading = false)
      )
      .subscribe((result: ApiResult<AdminAccountListModel>) => {
        this.apiResult = result;
      }, (error) => {
        DebugErrors(error);
      });
  }
}

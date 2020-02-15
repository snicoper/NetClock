import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/table-header.config';
import { UrlsApp } from '../../../../config';
import { HttpTransferData, LogicalOperator, RelationalOperator } from '../../../../models';
import { AdminUserListModel } from '../models';
import { AdminAccountsService } from '../services/admin-accounts.service';

@Component({
  selector: 'nc-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, OnDestroy {
  breadcrumb = new BreadcrumbCollection();
  transferData = new HttpTransferData<AdminUserListModel>();
  loading = false;
  tableHeaderConfig = new TableHeaderConfig<AdminUserListModel>();

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminAccountsService: AdminAccountsService
  ) {
  }

  ngOnInit(): void {
    this.configureTableHeaders();
    this.setBreadcrumb();
    this.loadUserList();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSelectItem(user: AdminUserListModel): void {
    this.router.navigate([UrlsApp.adminUserDetails.replace('{slug}', user.slug)]);
  }

  onChangePage(transferData: HttpTransferData<AdminUserListModel>): void {
    this.transferData = transferData;
    this.loadUserList();
  }

  onChangePageListNumber(transferData: HttpTransferData<AdminUserListModel>): void {
    this.transferData = transferData;
    this.loadUserList();
  }

  onOrdering(transferData: HttpTransferData<AdminUserListModel>): void {
    this.transferData = transferData;
    this.loadUserList();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig.transferData = this.transferData;
    this.tableHeaderConfig
      .addField('userName', 'Usuario', true)
      .addField('fullName', 'Nombre')
      .addField('email', 'Email', true)
      .addField('createAt', 'Fecha de registro', true)
      .addField('active', 'Activo', true);
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Lista de usuarios', UrlsApp.adminUserList, 'fas fa-users', false);
  }

  private loadUserList(): void {
    this.loading = true;
    this.adminAccountsService.getUsers(this.transferData)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (result: HttpTransferData<AdminUserListModel>) => {
          this.transferData = result;
        });
  }
}

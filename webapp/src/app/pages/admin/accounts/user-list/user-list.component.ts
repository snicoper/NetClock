import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/table-header.config';
import { UrlsApp } from '../../../../config';
import { HttpTransferData } from '../../../../models';
import { AdminUserListModel } from '../models';
import { AdminAccountsService } from '../services/admin-accounts.service';

@Component({
  selector: 'nc-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, OnDestroy {
  breadcrumb = new BreadcrumbCollection();
  transferData: HttpTransferData<AdminUserListModel>;
  tableHeaderConfig = new TableHeaderConfig();
  loading = false;

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminAccountsService: AdminAccountsService
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
    this.router.navigate([UrlsApp.adminUserDetails.replace('{slug}', user.slug)]);
  }

  onReload(): void {
    this.loadUsers();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig
      .add('userName', 'Usuario', true)
      .add('fullName', 'Nombre')
      .add('email', 'Email', true)
      .add('createAt', 'Fecha de registro', true)
      .add('active', 'Activo', true);
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
      .subscribe((result: HttpTransferData<AdminUserListModel>) => {
        this.transferData = result;
      });
  }
}

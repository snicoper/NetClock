import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { TableHeaderConfig } from '../../../../components/tables/table-header/table-header.config';
import { urlsApp } from '../../../../config';
import { RequestData } from '../../../../models';
import { AdminUserListModel } from '../models';
import { AdminAccountsService } from '../services/admin-accounts.service';

@Component({
  selector: 'nc-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, OnDestroy {
  public breadcrumb = new BreadcrumbCollection();
  public requestData = new RequestData<AdminUserListModel>();
  public loading = false;
  public tableHeaderConfig = new TableHeaderConfig<AdminUserListModel>();

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private adminAccountsService: AdminAccountsService
  ) {
  }

  public ngOnInit(): void {
    this.configureTableHeaders();
    this.setBreadcrumb();
    this.loadUserList();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public onSelectItem(user: AdminUserListModel): void {
    this.router.navigate([urlsApp.adminUserDetails.replace('{slug}', user.slug)]);
  }

  public onChangePage(requestData: RequestData<AdminUserListModel>): void {
    this.requestData = requestData;
    this.loadUserList();
  }

  public onChangePageListNumber(requestData: RequestData<AdminUserListModel>): void {
    this.requestData = requestData;
    this.loadUserList();
  }

  public onOrdering(requestData: RequestData<AdminUserListModel>): void {
    this.requestData = requestData;
    this.loadUserList();
  }

  private configureTableHeaders(): void {
    this.tableHeaderConfig.requestData = this.requestData;
    this.tableHeaderConfig
      .addField('userName', 'Usuario', true)
      .addField('fullName', 'Nombre')
      .addField('email', 'Email', true)
      .addField('createAt', 'Fecha de registro', true)
      .addField('active', 'Activo', true);
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', urlsApp.home, 'fas fa-home')
      .add('Administración', urlsApp.admin, 'fas fa-user-shield')
      .add('Lista de usuarios', urlsApp.adminUserList, 'fas fa-users', false)
  }

  private loadUserList(): void {
    this.loading = true;
    this.adminAccountsService.getUsers(this.requestData)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (result: RequestData<AdminUserListModel>) => {
          this.requestData = result;
        });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbCollection } from '../../../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../../../config';
import { AdminAccountsRestService } from '../services/admin-accounts-rest.service';

@Component({
  selector: 'nc-user-create',
  templateUrl: './admin-user-create.component.html',
  styleUrls: ['./admin-user-create.component.scss']
})
export class AdminUserCreateComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();

  constructor(
    private adminAccountsService: AdminAccountsRestService,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield')
      .add('Usuarios', UrlsApp.adminUserList, 'fas fa-users')
      .add('Nuevo', UrlsApp.adminUserCreate, 'fas fa-user-plus', false);
  }
}

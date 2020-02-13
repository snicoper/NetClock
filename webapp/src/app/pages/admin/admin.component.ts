import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../config';

@Component({
  selector: 'nc-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  ngOnInit(): void {
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Administración', UrlsApp.admin, 'fas fa-user-shield', false)
  }
}

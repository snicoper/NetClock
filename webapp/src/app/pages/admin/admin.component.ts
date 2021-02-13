import { Component, OnInit } from '@angular/core';
import { BreadcrumbCollection } from '../../components/breadcrumb/breadcrumb-collection';
import { siteUrls } from '../../core/common';

@Component({
  selector: 'nc-admin',
  templateUrl: './admin.component.html'
})
export class AdminComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  ngOnInit(): void {}

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Administración', siteUrls.admin, 'fas fa-user-shield', false);
  }
}

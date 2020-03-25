import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/models/BreadcrumbCollection';
import { SiteUrls } from '../../core';

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
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield', false);
  }
}

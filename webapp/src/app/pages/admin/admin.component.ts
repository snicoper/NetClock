import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/models/BreadcrumbCollection';
import { urlsApp } from '../../config';

@Component({
  selector: 'nc-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  public breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  public ngOnInit(): void {
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', urlsApp.home, 'fas fa-home')
      .add('Administración', urlsApp.admin, 'fas fa-user-shield', false)
  }
}

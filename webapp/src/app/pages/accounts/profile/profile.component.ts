import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../../config';

@Component({
  selector: 'nc-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  ngOnInit(): void {
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', UrlsApp.home, 'fas fa-home')
      .add('Perfil', UrlsApp.accounts, 'fas fa-user-cog', false)
  }
}

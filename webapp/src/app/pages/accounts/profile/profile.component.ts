import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../../components/breadcrumb/models/BreadcrumbCollection';
import { SiteUrls } from '../../../core';

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
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Perfil', SiteUrls.accounts, 'fas fa-user-cog', false);
  }
}

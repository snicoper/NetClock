import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../../components/breadcrumb/breadcrumb-collection';
import { siteUrls } from '../../../core/common';

@Component({
  selector: 'nc-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent {
  breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Perfil', siteUrls.accountsProfile, 'fas fa-user-cog', false);
  }
}

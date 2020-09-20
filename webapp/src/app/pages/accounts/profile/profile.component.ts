import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../../components/breadcrumb/BreadcrumbCollection';
import { SiteUrls } from '../../../core';

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
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Perfil', SiteUrls.accountsProfile, 'fas fa-user-cog', false);
  }
}

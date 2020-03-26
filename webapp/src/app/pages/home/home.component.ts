import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/BreadcrumbCollection';
import { SiteUrls } from '../../core';

@Component({
  selector: 'nc-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb.add('Inicio', SiteUrls.home, 'fas fa-home', false);
  }
}

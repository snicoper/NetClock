import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/breadcrumb-collection';
import { siteUrls } from '../../core/common';

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
    this.breadcrumb.add('Inicio', siteUrls.home, 'fas fa-home', false);
  }
}

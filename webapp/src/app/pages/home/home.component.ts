import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/models/BreadcrumbCollection';
import { UrlsApp } from '../../config';

@Component({
  selector: 'nc-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  breadcrumb = new BreadcrumbCollection();
  urlsApp = UrlsApp;

  constructor() {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb.add('Inicio', UrlsApp.home, 'fas fa-home', false)
  }
}

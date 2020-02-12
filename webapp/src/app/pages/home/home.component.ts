import { Component } from '@angular/core';

import { BreadcrumbCollection } from '../../components/breadcrumb/models/BreadcrumbCollection';
import { urlsApp } from '../../config';

@Component({
  selector: 'nc-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public breadcrumb = new BreadcrumbCollection();
  public urlsApp = urlsApp;

  constructor() {
    this.setBreadcrumb();
  }

  private setBreadcrumb(): void {
    this.breadcrumb.add('Inicio', urlsApp.home, 'fas fa-home', false)
  }
}

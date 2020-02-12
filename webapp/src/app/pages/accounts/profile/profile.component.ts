import { Component, OnInit } from '@angular/core';

import { BreadcrumbCollection } from '../../../components/breadcrumb/models/BreadcrumbCollection';
import { urlsApp } from '../../../config';

@Component({
  selector: 'nc-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  public breadcrumb = new BreadcrumbCollection();

  constructor() {
    this.setBreadcrumb();
  }

  public ngOnInit(): void {
  }

  private setBreadcrumb(): void {
    this.breadcrumb
      .add('Inicio', urlsApp.home, 'fas fa-home')
      .add('Perfil', urlsApp.accounts, 'fas fa-user-cog', false)
  }
}

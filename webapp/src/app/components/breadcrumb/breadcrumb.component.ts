import { Component, Input } from '@angular/core';

import { SidebarService } from '../sidebar/sidebar.service';
import { BreadcrumbCollection } from './BreadcrumbCollection';

@Component({
  selector: 'nc-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent {
  @Input() breadcrumb: BreadcrumbCollection;

  constructor(private sidebarService: SidebarService) {
  }

  toggleSidebar(): void {
    this.sidebarService.toggle();
  }
}

import { Component, Input } from '@angular/core';

import { BreadcrumbCollection } from '../breadcrumb/models/BreadcrumbCollection';

@Component({
  selector: 'nc-page-base',
  templateUrl: './page-base.component.html'
})
export class PageBaseComponent {
  @Input() cssContent = 'container-fluid';
  @Input() breadcrumb: BreadcrumbCollection;
  @Input() showPageTitle = true;
  @Input() pageTitle: string;
}

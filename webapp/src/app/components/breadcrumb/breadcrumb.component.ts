import { Component, Input } from '@angular/core';

import { BreadcrumbCollection } from './models/BreadcrumbCollection';

@Component({
  selector: 'nc-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent {
  @Input() breadcrumb: BreadcrumbCollection;
}

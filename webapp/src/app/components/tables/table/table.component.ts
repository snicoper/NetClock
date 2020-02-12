import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-table',
  templateUrl: './table.component.html'
})
export class TableComponent {
  @Input() tableResponsive = true;
}

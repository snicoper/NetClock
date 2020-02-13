import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-non-field-errors',
  templateUrl: './non-field-errors.component.html'
})
export class NonFieldErrorsComponent {
  @Input() errors = {};
}

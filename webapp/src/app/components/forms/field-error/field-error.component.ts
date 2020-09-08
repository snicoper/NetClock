import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

import { BadRequest } from './bad-request';

@Component({
  selector: 'nc-field-error',
  templateUrl: './field-error.component.html'
})
export class FieldErrorComponent implements OnInit {
  @Input() submitted = false;
  @Input() fieldText: string;
  @Input() fieldName: string;
  @Input() errors: BadRequest;
  @Input() form: FormGroup;

  control: AbstractControl;

  ngOnInit(): void {
    this.control = this.form.get(this.fieldName);
  }

  formHasErrors(): boolean {
    return this.submitted
      && this.form
      && this.form.dirty
      || this.form.touched
      && this.form[this.fieldName]
      && this.form[this.fieldName].errors;
  }

  controlHasErrors(): boolean {
    return !!(this.control && this.control.dirty && this.control.errors || this.submitted && this.control.errors);
  }

  getBadRequestErrors(): string[] {
    if (this.errors && 'errors' in this.errors) {
      return this.errors.errors[this.fieldName];
    }
  }
}

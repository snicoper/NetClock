import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import * as HttpStatus from 'http-status-codes';

import { BadRequest } from '../../../types';


@Component({
  selector: 'nc-field-error',
  templateUrl: './field-error.component.html'
})
export class FieldErrorComponent implements OnInit {
  @Input() submitted = false;
  @Input() fieldText: string;
  @Input() fieldName: string;
  @Input() badRequest: BadRequest;
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
    if (this.badRequest && this.badRequest.status === HttpStatus.BAD_REQUEST) {
      return this.badRequest.errors[this.fieldName];
    }
  }
}

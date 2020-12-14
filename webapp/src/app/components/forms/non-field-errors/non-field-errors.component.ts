import { Component, Input, OnInit } from '@angular/core';

import { FormGlobals } from '../../../core/common';
import { BadRequest } from '../../../core/types';

@Component({
  selector: 'nc-non-field-errors',
  templateUrl: './non-field-errors.component.html'
})
export class NonFieldErrorsComponent implements OnInit {
  @Input() badRequest: BadRequest;

  formGlobals = FormGlobals;

  ngOnInit(): void {
  }

  get hasErrors(): boolean {
    return this.badRequest && this.badRequest.errors && FormGlobals.NonFieldErrors in this.badRequest.errors;
  }
}

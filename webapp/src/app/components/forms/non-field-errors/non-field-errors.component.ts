import { Component, Input, OnInit } from '@angular/core';
import { BadRequest } from '../../../types';

@Component({
  selector: 'nc-non-field-errors',
  templateUrl: './non-field-errors.component.html'
})
export class NonFieldErrorsComponent implements OnInit {
  @Input() badRequest: BadRequest;

  ngOnInit(): void {
  }

  get hasErrors(): boolean {
    return this.badRequest && this.badRequest.errors && 'nonFieldErrors' in this.badRequest.errors;
  }
}

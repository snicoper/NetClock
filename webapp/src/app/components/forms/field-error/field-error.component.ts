import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'nc-field-error',
  templateUrl: './field-error.component.html'
})
export class FieldErrorComponent implements OnInit {
  @Input() submitted = false;
  @Input() fieldText: string;
  @Input() fieldName: string;
  @Input() errors = {};
  @Input() form: FormGroup;

  public control: AbstractControl;

  public ngOnInit(): void {
    this.control = this.form.get(this.fieldName);
  }

  public formHasErrors(): boolean {
    return this.submitted
      && this.form
      && this.form.dirty
      || this.form.touched
      && this.form[this.fieldName]
      && this.form[this.fieldName].errors;
  }

  public controlHasErrors(): boolean {
    return !!(this.control && this.control.dirty || this.submitted && this.control.errors);
  }

  public getBadRequestErrors(): string[] {
    if (this.errors) {
      return this.errors[this.fieldName];
    }
  }
}

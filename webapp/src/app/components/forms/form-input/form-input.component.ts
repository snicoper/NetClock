import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

import { BadRequest } from '../../../types';
import { FormInputTypes } from './form-input-types.enum';

@Component({
  selector: 'nc-form-input',
  templateUrl: './form-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FormInputComponent),
      multi: true
    }
  ]
})
export class FormInputComponent implements ControlValueAccessor {
  @Input() id: string;
  @Input() form: FormGroup;
  @Input() fieldName: string;
  @Input() inputType = FormInputTypes.text;
  @Input() label: string;
  @Input() extraCss: string;
  @Input() badRequest: BadRequest;
  @Input() submitted = false;
  @Input() placeholder = '';

  value: string;
  isDisabled: boolean;

  constructor() {
    this.id = Math.random().toString();
  }

  onChange = (_: any) => {
  }

  onTouch = () => {
  }

  writeValue(value: any): void {
    if (value !== undefined) {
      this.value = value || '';
      this.onChange(this.value);
    } else {
      this.value = '';
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  onChangeValue(value: any): void {
    this.onChange(value);
  }

  isInvalid(): boolean {
    const control = this.form.get(this.fieldName);

    return this.submitted && control.invalid || this.badRequest && this.badRequest.errors && this.fieldName in this.badRequest.errors;
  }
}

import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

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
export class FormInputComponent implements ControlValueAccessor, OnInit {
  @Input() id: string;
  @Input() form: FormGroup;
  @Input() fieldName: string;
  @Input() inputType = FormInputTypes.text;
  @Input() label: string;
  @Input() extraCss: string;
  @Input() errors = [];
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

  ngOnInit(): void {
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

  isValid(): boolean {
    const control = this.form.get(this.fieldName);

    return !(this.submitted && control.invalid);
  }
}

import { FormGroup } from '@angular/forms';

/** Custom validator to check that two fields match. */
export function passwordMustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    if (matchingControl.errors && !matchingControl.errors.mustMatch) {
      return;
    }

    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ noPasswordMatch: true });
    } else {
      matchingControl.setErrors(null);
    }
  };
}

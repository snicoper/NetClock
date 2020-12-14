import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { siteUrls } from '../../../core/common';
import { BadRequest } from '../../../core/types';
import { passwordMustMatch } from '../../../core/validators';
import { RecoveryPasswordValidateService } from './recovery-password-validate.service';

@Component({
  selector: 'nc-recovery-password-validate',
  templateUrl: './recovery-password-validate.component.html',
  providers: [RecoveryPasswordValidateService]

})
export class RecoveryPasswordValidateComponent {
  form: FormGroup;
  badRequest: BadRequest;
  loading = false;
  submitted = false;
  errorChangePassword = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private recoveryPasswordValidateService: RecoveryPasswordValidateService
  ) {
    this.buildForm();
    this.route.queryParamMap.subscribe(
      (params: ParamMap) => {
        this.form.patchValue({
          userId: params.get('userId'),
          code: params.get('code')
        });
      }
    );
  }

  control(fieldName: string): AbstractControl {
    return this.form.get(fieldName);
  }

  onSubmit(): void {
    this.submitted = true;
    this.badRequest = null;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.recoveryPasswordValidateService.recoveryPasswordValidate(this.form.value).subscribe(
      () => {
        this.toastr.success('Contraseña restablecida con éxito');
        this.router.navigate([siteUrls.authLogin]);
      },
      () => {
        this.errorChangePassword = true;
      }
    );
  }

  private buildForm(): void {
    this.form = this.fb.group({
        userId: new FormControl(null, [Validators.required]),
        code: new FormControl(null, [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required])
      },
      {
        validators: passwordMustMatch('password', 'confirmPassword')
      });
  }
}

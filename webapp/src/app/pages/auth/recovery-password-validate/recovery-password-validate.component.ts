import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { urlsApp } from '../../../config';
import { PasswordMustMatch } from '../../../validators';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'nc-recovery-password-validate',
  templateUrl: './recovery-password-validate.component.html'
})
export class RecoveryPasswordValidateComponent {
  public form: FormGroup;
  public errors: object;
  public loading = false;
  public submitted = false;
  public errorChangePassword = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService
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

  public control(fieldName: string): AbstractControl {
    return this.form.get(fieldName);
  }

  public onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.authService.recoveryPasswordValidate(this.form.value).subscribe(
      () => {
        this.toastr.success('Contraseña restablecida con éxito');
        this.router.navigate([urlsApp.login]);
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
        validators: PasswordMustMatch('password', 'confirmPassword')
      });
  }
}

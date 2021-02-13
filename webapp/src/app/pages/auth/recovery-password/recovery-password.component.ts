import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { siteUrls } from '../../../core/common';
import { BadRequest } from '../../../core/types';
import { AuthService } from '../login/auth.service';
import { RecoveryPasswordService } from './recovery-password.service';

@Component({
  selector: 'nc-recovery-password',
  templateUrl: './recovery-password.component.html',
  providers: [RecoveryPasswordService]
})
export class RecoveryPasswordComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  errors: BadRequest;
  siteUrls = siteUrls;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private recoveryPasswordService: RecoveryPasswordService,
    private authService: AuthService
  ) {
    if (this.authService.currentUserValue) {
      this.router.navigate([siteUrls.home]);
    }
  }

  ngOnInit(): void {
    this.buildForm();
  }

  control(control: string): AbstractControl {
    return this.form.get(control);
  }

  onSubmit(): void {
    this.submitted = true;
    this.errors = null;

    if (this.form.invalid) {
      return;
    }

    this.recoveryPasswordService
      .recoveryPassword(this.form.value)
      .pipe(finalize(() => this.router.navigate([siteUrls.authRecoveryPasswordSuccess])));
  }

  private buildForm(): void {
    this.form = this.formBuilder.group({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { UrlsApp } from '../../../config';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'nc-recovery-password',
  templateUrl: './recovery-password.component.html'
})
export class RecoveryPasswordComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  errors = {};
  urlsApp = UrlsApp;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate([UrlsApp.home]);
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
    this.errors = {};

    if (this.form.invalid) {
      return;
    }

    this.authenticationService.recoveryPassword(this.form.value)
      .pipe(finalize(() => this.router.navigate([UrlsApp.recoveryPasswordSuccess])));
  }

  private buildForm(): void {
    this.form = this.formBuilder.group({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }
}

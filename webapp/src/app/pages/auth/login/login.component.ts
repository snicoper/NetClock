import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize, timeout } from 'rxjs/operators';

import { siteUrls } from '../../../core/common';
import { BadRequest } from '../../../core/types';
import { AuthService } from './auth.service';
import { CurrentUserModel } from './current-user.model';

@Component({
  selector: 'nc-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl = '/';
  badRequest: BadRequest;
  siteUrls = siteUrls;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate([siteUrls.home]);
    }
  }

  ngOnInit(): void {
    this.buildForm();

    if ('returnUrl' in this.route.snapshot.queryParams) {
      this.returnUrl = this.route.snapshot.queryParams.returnUrl;
    }
  }

  control(control: string): AbstractControl {
    return this.form.get(control);
  }

  onSubmit(): void {
    this.submitted = true;
    this.badRequest = null;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.form.value)
      .pipe(
        timeout(5000),
        finalize(() => this.loading = false)
      )
      .subscribe(
        (user: CurrentUserModel) => {
          this.authenticationService.setCurrentUser(user);
          this.router.navigate([this.returnUrl]);
        },
        (error: any) => {
          this.badRequest = error.error;
        }
      );
  }

  private buildForm(): void {
    this.form = this.fb.group({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      rememberMe: new FormControl(false)
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { siteUrls } from '../../../../core';
import { AdminAccountDetailsModel } from './admin-account-details.model';
import { AdminAccountDetailsService } from './admin-account-details.service';

@Component({
  selector: 'nc-user-details',
  templateUrl: './admin-account-details.component.html',
  providers: [AdminAccountDetailsService]
})
export class AdminAccountDetailsComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  user: AdminAccountDetailsModel;
  loading = false;
  siteUrls = siteUrls;

  private readonly userSlug: string;

  constructor(
    private adminUserDetailsService: AdminAccountDetailsService,
    private route: ActivatedRoute
  ) {
    this.userSlug = this.route.snapshot.paramMap.get('slug');
  }

  ngOnInit(): void {
    this.loadUser();
  }

  private setBreadcrumb(): void {
    const fullName = `${this.user.firstName} ${this.user.lastName}`;

    this.breadcrumb
      .add('Inicio', siteUrls.home, 'fas fa-home')
      .add('Administración', siteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', siteUrls.adminAccounts, 'fas fa-users')
      .add(fullName, siteUrls.adminAccounts, 'fas fa-user', false);
  }

  private loadUser(): void {
    this.loading = true;
    this.adminUserDetailsService.getBy(this.userSlug)
      .pipe(
        finalize(() => this.loading = false)
      )
      .subscribe((result: AdminAccountDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }
}

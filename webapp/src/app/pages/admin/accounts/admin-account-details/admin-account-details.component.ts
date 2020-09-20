import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { SiteUrls } from '../../../../core';
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
  siteUrls = SiteUrls;

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
      .add('Inicio', SiteUrls.home, 'fas fa-home')
      .add('Administración', SiteUrls.admin, 'fas fa-user-shield')
      .add('Usuarios', SiteUrls.adminAccounts, 'fas fa-users')
      .add(fullName, SiteUrls.adminAccounts, 'fas fa-user', false);
  }

  private loadUser(): void {
    this.loading = true;
    this.adminUserDetailsService.getBy(this.userSlug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminAccountDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }
}

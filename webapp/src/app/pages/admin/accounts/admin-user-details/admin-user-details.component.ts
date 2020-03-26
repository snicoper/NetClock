import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { BreadcrumbCollection } from '../../../../components/breadcrumb/BreadcrumbCollection';
import { SiteUrls } from '../../../../core';
import { AdminUserDetailsModel } from './admin-user-details.model';
import { AdminUserDetailsService } from './admin-user-details.service';

@Component({
  selector: 'nc-user-details',
  templateUrl: './admin-user-details.component.html',
  providers: [AdminUserDetailsService]
})
export class AdminUserDetailsComponent implements OnInit {
  breadcrumb = new BreadcrumbCollection();
  user: AdminUserDetailsModel;
  loading = false;

  private readonly userSlug: string;

  constructor(
    private adminUserDetailsService: AdminUserDetailsService,
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
      .add('Usuarios', SiteUrls.adminUserList, 'fas fa-users')
      .add(fullName, SiteUrls.adminUserList, 'fas fa-user', false);
  }

  private loadUser(): void {
    this.loading = true;
    this.adminUserDetailsService.getBy(this.userSlug)
      .pipe(finalize(() => this.loading = false))
      .subscribe((result: AdminUserDetailsModel) => {
        this.user = result;
        this.setBreadcrumb();
      });
  }
}

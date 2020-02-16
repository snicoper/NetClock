import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule, CollapseModule } from 'ngx-bootstrap';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

import { DirectivesModule } from '../directives/directives.module';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { CardAlignCenterComponent } from './cards/card-align-center/card-align-center.component';
import { CardComponent } from './cards/card/card.component';
import { FooterComponent } from './footer/footer.component';
import { FieldErrorComponent } from './forms/field-error/field-error.component';
import { NonFieldErrorsComponent } from './forms/non-field-errors/non-field-errors.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PageBaseComponent } from './page-base/page-base.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { TableHeaderComponent } from './tables/table-header/table-header.component';
import { TableComponent } from './tables/table/table.component';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    FieldErrorComponent,
    NonFieldErrorsComponent,
    BreadcrumbComponent,
    PageTitleComponent,
    PageBaseComponent,
    SpinnerComponent,
    CardAlignCenterComponent,
    SidebarComponent,
    PaginationComponent,
    TableHeaderComponent,
    CardComponent,
    TableComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    PerfectScrollbarModule,
    DirectivesModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    FieldErrorComponent,
    NonFieldErrorsComponent,
    BreadcrumbComponent,
    PageTitleComponent,
    PageBaseComponent,
    SpinnerComponent,
    CardAlignCenterComponent,
    SidebarComponent,
    PaginationComponent,
    TableHeaderComponent,
    CardComponent,
    TableComponent
  ]
})
export class ComponentsModule {
}

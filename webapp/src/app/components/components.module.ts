import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDropdownModule, CollapseModule, TooltipModule } from 'ngx-bootstrap';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

import { DirectivesModule } from '../directives/directives.module';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { CardComponent } from './cards/card/card.component';
import { FooterComponent } from './footer/footer.component';
import { FieldErrorComponent } from './forms/field-error/field-error.component';
import { NonFieldErrorsComponent } from './forms/non-field-errors/non-field-errors.component';
import { PageBaseComponent } from './page-base/page-base.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { TableHeaderComponent } from './tables/table-header/table-header.component';
import { TableComponent } from './tables/table/table.component';
import { FormInputComponent } from './forms/form-input/form-input.component';

@NgModule({
  declarations: [
    FooterComponent,
    FieldErrorComponent,
    NonFieldErrorsComponent,
    BreadcrumbComponent,
    PageTitleComponent,
    PageBaseComponent,
    SpinnerComponent,
    SidebarComponent,
    PaginationComponent,
    TableHeaderComponent,
    CardComponent,
    TableComponent,
    FormInputComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    PerfectScrollbarModule,
    DirectivesModule
  ],
  exports: [
    FooterComponent,
    FieldErrorComponent,
    NonFieldErrorsComponent,
    BreadcrumbComponent,
    PageTitleComponent,
    PageBaseComponent,
    SpinnerComponent,
    SidebarComponent,
    PaginationComponent,
    TableHeaderComponent,
    CardComponent,
    TableComponent,
    FormInputComponent
  ]
})
export class ComponentsModule {
}

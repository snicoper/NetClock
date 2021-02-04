import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { DirectivesModule } from '../core/directives/directives.module';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { CardComponent } from './cards/card/card.component';
import { FooterComponent } from './footer/footer.component';
import { BtnSubmitFormComponent } from './forms/btn-submit-form/btn-submit-form.component';
import { FieldErrorComponent } from './forms/field-error/field-error.component';
import { FormInputComponent } from './forms/form-input/form-input.component';
import { NonFieldErrorsComponent } from './forms/non-field-errors/non-field-errors.component';
import { PageBaseComponent } from './page-base/page-base.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { TableHeaderComponent } from './tables/table-header/table-header.component';
import { TableComponent } from './tables/table/table.component';

@NgModule({
  declarations: [
    BreadcrumbComponent,
    CardComponent,
    BtnSubmitFormComponent,
    FieldErrorComponent,
    FooterComponent,
    FormInputComponent,
    NonFieldErrorsComponent,
    PageBaseComponent,
    PageTitleComponent,
    PaginationComponent,
    SidebarComponent,
    SpinnerComponent,
    TableComponent,
    TableHeaderComponent
  ],
  imports: [
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    CommonModule,
    DirectivesModule,
    FormsModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    RouterModule,
    TooltipModule.forRoot()
  ],
  exports: [
    BreadcrumbComponent,
    CardComponent,
    FieldErrorComponent,
    FooterComponent,
    FormInputComponent,
    NonFieldErrorsComponent,
    BtnSubmitFormComponent,
    PageBaseComponent,
    PageTitleComponent,
    PaginationComponent,
    SidebarComponent,
    SpinnerComponent,
    TableComponent,
    TableHeaderComponent
  ]
})
export class ComponentsModule {
}

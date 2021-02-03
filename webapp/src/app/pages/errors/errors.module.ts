import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ComponentsModule } from '../../components/components.module';
import { Error403Component } from './error403/error403.component';
import { ErrorsRoutingModule } from './errors-routing.module';

@NgModule({
  declarations: [
    Error403Component
  ],
  imports: [
    CommonModule,
    ErrorsRoutingModule,
    ComponentsModule
  ]
})
export class ErrorsModule {
}

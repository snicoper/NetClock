import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ComponentsModule } from '../../components/components.module';
import { ErrorsRoutingModule } from './errors-routing.module';
import { Error403Component } from './error403/error403.component';

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

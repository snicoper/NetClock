import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ComponentsModule } from '../../components/components.module';
import { Error404Component } from './error404/error404.component';
import { ErrorsRoutingModule } from './errors-routing.module';

@NgModule({
  declarations: [
    Error404Component
  ],
  imports: [
    CommonModule,
    ErrorsRoutingModule,
    ComponentsModule
  ],
})
export class ErrorsModule {
}

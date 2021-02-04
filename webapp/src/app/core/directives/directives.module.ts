import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ClickStopPropagationDirective } from './click-stop-propagation.directive';

@NgModule({
  declarations: [
    ClickStopPropagationDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ClickStopPropagationDirective
  ]
})
export class DirectivesModule {
}

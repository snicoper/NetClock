import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ClickStopPropagationDirective } from './click-stop-propagation.directive';
import { TippyDirective } from './tippy.directive';

@NgModule({
  declarations: [
    ClickStopPropagationDirective,
    TippyDirective
  ],
  imports: [
    CommonModule
  ],
    exports: [
        ClickStopPropagationDirective,
        TippyDirective
    ]
})
export class DirectivesModule {
}

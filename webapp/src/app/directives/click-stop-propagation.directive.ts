import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[ifmClickStopPropagation]'
})
export class ClickStopPropagationDirective {
  @HostListener('click', ['$event'])
  public onClick(event: any): void
  {
    event.stopPropagation();
  }
}

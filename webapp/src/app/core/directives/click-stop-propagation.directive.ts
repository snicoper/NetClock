import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[ncClickStopPropagation]'
})
export class ClickStopPropagationDirective {
  @HostListener('click', ['$event'])
  onClick(event: any): void {
    event.stopPropagation();
  }
}

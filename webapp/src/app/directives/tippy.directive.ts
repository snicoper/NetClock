import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import tippy from 'tippy.js';

/**
 * @example
 * <button ncTippy [tippyOptions]="tippyOptions">Hello</button>
 */
@Directive({
  selector: '[ncTippy]'
})
export class TippyDirective implements OnInit {
  @Input() tippyOptions = {};

  constructor(private el: ElementRef) {
  }

  ngOnInit() {
    tippy(this.el.nativeElement, this.tippyOptions);
  }
}

import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-card',
  templateUrl: './card.component.html'
})
export class CardComponent {
  @Input() loading = false;
  @Input() spinnerSize = 100;
}

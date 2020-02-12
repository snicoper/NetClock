import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-card-align-center',
  templateUrl: './card-align-center.component.html'
})
export class CardAlignCenterComponent {
  @Input() cssCard = 'col-lg-6';
  @Input() cssContainer = 'container-fluid';
  @Input() justifyContent = 'justify-content-center';
}

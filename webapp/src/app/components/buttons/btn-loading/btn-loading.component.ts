import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-btn-loading',
  templateUrl: './btn-loading.component.html'
})
export class BtnLoadingComponent {
  @Input() loading = false;
  @Input() btnClass = 'btn btn-primary';
  @Input() btnType = 'submit';
  @Input() btnText = 'Procesando...';
  @Input() spinnerClass = 'spinner-border-sm';
  @Input() spinnerSize = 22;
  @Input() spinnerText = 'spinner-border-sm';
  @Input() spinnerColor = 'text-white';
  @Input() spinnerLoadingText: string;
}

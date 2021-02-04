import { Component, Input } from '@angular/core';

@Component({
  selector: 'nc-btn-submit-form',
  templateUrl: './btn-submit-form.component.html'
})
export class BtnSubmitFormComponent {
  @Input() loading = false;
  @Input() btnClass = 'btn btn-primary';
  @Input() btnType = 'submit';
  @Input() btnText: string;
  @Input() spinnerClass = 'spinner-border-sm';
  @Input() spinnerText = 'spinner-border-sm';
  @Input() spinnerColor = 'text-white';
  @Input() spinnerLoadingText: string;
}

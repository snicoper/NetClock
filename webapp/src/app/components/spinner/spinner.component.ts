import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'nc-spinner',
  templateUrl: './spinner.component.html'
})
export class SpinnerComponent implements OnInit {
  /** Solo mostrara el spinner cuando loading sea true. */
  @Input() public loading = false;
  /** Alineación del spinner, por defecto: 'justify-content-center'. */
  @Input() public justify = 'justify-content-center';
  /** Color del spinner, por defecto: 'text-primary'. */
  @Input() public color = 'text-primary';
  /** Clases css extra para el spinner. */
  @Input() public css = '';
  /** Texto mostrado a la derecha del spinner. */
  @Input() text = '';
  /** Tamaño en px del spinner, por defecto 40px. */
  @Input() size = 40;

  public style: string;

  public ngOnInit(): void {
    this.style = `width: ${this.size}px; height: ${this.size}px;`;
  }
}

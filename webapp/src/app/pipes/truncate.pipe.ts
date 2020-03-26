import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'truncate' })
export class TruncatePipe implements PipeTransform {
  transform(value: string, limite: string, dots: boolean = true): string {
    const limit = parseInt(limite, 10);
    const printDots = dots === true ? '...' : '';

    return value.length > limit ? `${value.substring(0, limit)}${printDots}` : value;
  }
}

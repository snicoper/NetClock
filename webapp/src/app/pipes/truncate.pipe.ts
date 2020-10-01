import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'truncate' })
export class TruncatePipe implements PipeTransform {
  transform(value: string, limit: number, dots: boolean = true): string {
    const printDots = dots === true ? '...' : '';

    return value.length > limit ? `${value.substring(0, limit)}${printDots}` : value;
  }
}

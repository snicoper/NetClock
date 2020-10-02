import { Pipe, PipeTransform } from '@angular/core';

import { Utils } from '../core';

/** Pasar bytes a una medida legible según el size. */
@Pipe({ name: 'formatSizeUnit' })
export class FormatSizeUnitPipe implements PipeTransform {
  transform(size: number): string {
    return Utils.formatSizeUnit(size);
  }
}

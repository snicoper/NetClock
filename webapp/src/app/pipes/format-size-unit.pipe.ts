import { Pipe, PipeTransform } from '@angular/core';

import { Utils } from '../core';

@Pipe({ name: 'formatSizeUnit' })
export class FormatSizeUnitPipe implements PipeTransform {
  transform(value: number): string {
    return Utils.formatSizeUnit(value);
  }
}

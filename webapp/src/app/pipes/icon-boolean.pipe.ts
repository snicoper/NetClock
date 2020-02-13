import { Pipe, PipeTransform } from '@angular/core';

/**
 * Devuelve un icon en función del valor booleano.
 * Para tipos booleans, retorna el HTML con un icono según el valor.
 *
 * @example:
 *  <td [innerHTML]="employee.is_staff | toIcon"></td>
 */
@Pipe({ name: 'iconBoolean' })
export class IconBooleanPipe implements PipeTransform {
  transform(value: boolean): string {
    if (value) {
      return '<i class="fas fa-check text-success"></i>';
    }

    return '<i class="fas fa-times text-danger"></i>';
  }
}

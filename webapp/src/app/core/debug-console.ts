import { settings } from '../config';

/** Muestra los errores solo si se esta en desarrollo. */
export class DebugConsole {
  public static errors(...errors): void {
    if (settings.isDebug === true) {
      errors.forEach((error) => console.log(error));
    }
  }
}

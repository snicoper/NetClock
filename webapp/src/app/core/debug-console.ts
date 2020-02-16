// tslint:disable
import { Settings } from '../config';

/** Muestra los errores solo si se esta en desarrollo. */
export class DebugConsole {
  static errors(...errors: string[]): void {
    if (Settings.isDebug === true) {
      errors.forEach((error) => console.log(error));
    }
  }

  static raiseError(message: string): void {
    if (Settings.isDebug === true) {
      message = '';
    }

    throw new Error(message);
  }
}

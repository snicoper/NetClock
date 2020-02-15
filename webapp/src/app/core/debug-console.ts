// tslint:disable
import { Settings } from '../config';

/** Muestra los errores solo si se esta en desarrollo. */
export class DebugConsole {
  static errors(...errors: string[]): void {
    if (Settings.isDebug === true) {
      errors.forEach((error) => console.log(error));
    }
  }

  static consoleLog(key, message): void {
    if (Settings.isDebug === true) {
      console.log(key.toUpperCase(), message);
    }
  }

  static raiseTypeError(message: string): void {
    if (Settings.isDebug === true) {
      message = '';
    }

    throw new TypeError(message);
  }

  static raiseError(message: string): void {
    if (Settings.isDebug === true) {
      message = '';
    }

    throw new Error(message);
  }
}

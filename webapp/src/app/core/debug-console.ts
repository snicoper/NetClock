// tslint:disable
import { Settings } from '../config';

/** Muestra los errores solo si se esta en desarrollo. */
export class DebugConsole {
  static errors(...errors): void {
    if (Settings.isDebug === true) {
      errors.forEach((error) => console.log(error));
    }
  }

  static error(key, error): void {
    if (Settings.isDebug === true) {
      console.log(key.toLowerCase(), error);
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

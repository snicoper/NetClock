import { Settings } from '../config';

/** Muestra los errores solo si se esta en desarrollo. */
export class DebugConsole {
  static errors(...errors): void {
    if (Settings.isDebug === true) {
      errors.forEach((error) => console.log(error));
    }
  }

  static raiseTypeError(message: string): void {
    if (Settings.isDebug === true) {
      message = '';
    }

    throw new TypeError(message);
  }
}

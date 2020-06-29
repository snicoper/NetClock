import { AppConfig } from '../app.config';

export function DebugErrors(...errors: string[]): void {
  if (AppConfig.isDebug === true) {
    // tslint:disable-next-line: no-console
    errors.forEach((error) => console.log(error));
  }
}

export function RaiseError(message: string): void {
  if (AppConfig.isDebug === true) {
    throw new Error(message);
  }
}

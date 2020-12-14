import { appConfig } from '../../app.config';

export function debugErrors(...errors: string[]): void {
  if (appConfig.isDebug === true) {
    // tslint:disable-next-line: no-console
    errors.forEach((error) => console.log(error));
  }
}

export function raiseError(message: string): void {
  if (appConfig.isDebug === true) {
    throw new Error(message);
  }
}

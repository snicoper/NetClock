import { appConfig } from '../../app.config';

export const debugErrors = (...errors: string[]): void => {
  if (appConfig.isDebug === true) {
    // eslint-disable-next-line no-console
    errors.forEach((error) => console.log(error));
  }
};

export const raiseError =(message: string): void => {
  if (appConfig.isDebug === true) {
    throw new Error(message);
  }
};

import { environment } from '../environments/environment';

export const appConfig = {
  siteName: 'NetClock',
  apiUrl: environment.apiUrl,
  siteUrl: environment.siteUrl,
  isDebug: environment.isDebug,
  baseApiUrl: `${environment.apiUrl}/${environment.apiSegment}`
};

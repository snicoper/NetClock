import { environment } from 'src/environments/environment';

/** Configuración predeterminada del sitio. */
export const Settings = {
  // Nombre del sitio.
  siteName: 'Net clock',

  // URL API.
  apiUrl: environment.production ? 'https://localhost:5101' : 'https://localhost:5001',

  // URL sitio frontend.
  siteUrl: environment.production ? 'http://localhost:4210' : 'http://localhost:4200',

  // Segmento a concatenar el la URL API.
  apiSegment: 'api/v1',

  // Esta en desarrollo o producción?.
  isDebug: !environment.production
};

/** Base URL a la API. */
export const BaseApiUrl = `${Settings.apiUrl}/${Settings.apiSegment}`;

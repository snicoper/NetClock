import { environment } from 'src/environments/environment';

/** Configuración predeterminada del sitio. */
export const settings = {
  // Nombre del sitio.
  siteName: 'Net clock',

  // URL API.
  apiUrl: environment.production ? 'https://api-clock.es' : 'https://localhost:5001',

  // URL sitio frontend.
  siteUrl: environment.production ? 'https://clock.es' : 'http://localhost:4200',

  // Segmento a concatenar el la URL API.
  apiSegment: 'api/v1',

  // Esta en desarrollo o producción?.
  isDebug: !environment.production
};

/** Base URL a la API. */
export const baseUrl = `${settings.apiUrl}/${settings.apiSegment}`;

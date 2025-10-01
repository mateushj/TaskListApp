import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { CustomPaginatorIntl } from './utils/custom-paginator';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes), provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch()),
    provideNativeDateAdapter(),
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { 
      provide: MatPaginatorIntl, 
      useClass: CustomPaginatorIntl 
    }
  ]
};

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AccountModule } from './account.module';
import { enableProdMode } from '@angular/core';

enableProdMode();

platformBrowserDynamic().bootstrapModule(AccountModule);
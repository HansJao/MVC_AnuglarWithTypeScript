import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

//import {AccountComponent} from '../Account/account.component';


@NgModule({
    imports: [
        //angular builtin module
        BrowserModule,
        HttpModule,
        FormsModule
    ],
    declarations: [
        AppComponent, /*AccountComponent*/
    ],
    providers: [
        //register services here
    ],
    bootstrap: [
        AppComponent, /*AccountComponent*/
    ]
})

export class AppModule {
}


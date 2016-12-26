import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';

import { AccountComponent } from './account.component';


@NgModule({
    imports: [
        //angular builtin module
        BrowserModule,
        HttpModule,
        FormsModule
    ],
    declarations: [
        AccountComponent
    ],
    providers: [
        //register services here
    ],
    bootstrap: [
        AccountComponent
    ]
})

export class AccountModule {
}


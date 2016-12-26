import { Component, OnInit } from '@angular/core';
import {Http,Response} from '@angular/http';

@Component({
    selector: 'accountLogin',
    templateUrl: '../Scripts/Account/account.component.html'
})

export class AccountComponent implements OnInit {
    temp: string;

    constructor(private _http: Http) {
        this.temp = "Welcome";
    }

    ngOnInit() {
        console.log("hello")
    }

    btnClickedEvent(): void {
        console.log("button has been clicked");
    }

}


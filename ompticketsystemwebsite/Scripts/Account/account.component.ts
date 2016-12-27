import { Component, OnInit } from '@angular/core';
import {Http,Response,Headers} from '@angular/http';

@Component({
    selector: 'accountLogin',
    templateUrl: '../Scripts/Account/account.component.html'
})

export class AccountComponent implements OnInit {
    temp: string;

    constructor(private _http: Http) {
        this.temp = "Welcome";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        var test;
        _http.post('/Account/GetPeople', '{"test": "123" }', headers).subscribe(data=> test=data);
    }

    ngOnInit() {
        console.log("hello");
        
    }

    btnClickedEvent(): void {
        console.log("button has been clicked");
    }

}




import { Component, OnInit } from '@angular/core';
@Component({
    selector: 'Hans-app',
    templateUrl: '../Scripts/App/hans.component.html'
})

export class HansComponent implements OnInit {
    temp: string;

    constructor() {
        this.temp = "Welcome";
    }

    ngOnInit() {
        console.log("hello")
    }

    btnClickedEvent(): void {
        console.log("button has been clicked");
    }

}
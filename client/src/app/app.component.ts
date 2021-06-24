import { Component, OnInit } from '@angular/core';
import { SignalRService } from './_services/signal-r.service';
import { HttpClient } from '@angular/common/http';
import { CryptoPair } from './_models/cryptopair';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  cryptoPairs: CryptoPair[] = [];

  constructor(public signalRService: SignalRService, private http: HttpClient) { }
  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();   
    this.startHttpRequest();
  }
  private startHttpRequest = () => {
    this.http.get('http://localhost:8000/api/crypto')
      .subscribe(res => {
        console.log(res);
      })
  }
}

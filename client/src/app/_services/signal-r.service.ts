import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";  // or from "@microsoft/signalr" if you are using a new library
import { SheetModel } from '../_interfaces/sheet-model';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: SheetModel[] | undefined;
  public bradcastedData: SheetModel[] | undefined;

  private hubConnection: signalR.HubConnection | undefined;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:8000/crypto')
                            .withAutomaticReconnect()
                            .configureLogging(signalR.LogLevel.Information)
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection?.on('transfercryptodata', (data) => {
      this.data = data;
      data = data.sort(function (a:any,b:any){
        var x = a['percentage']; var y = b['percentage'];
        return ((x < y) ? 1 : ((x > y) ? -1 : 0));
      });
      console.log(data);
    });
  }

}
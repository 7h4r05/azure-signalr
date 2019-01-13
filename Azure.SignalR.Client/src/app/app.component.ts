import { Component } from '@angular/core';
import { HubConnectionBuilder,  HubConnection, JsonHubProtocol } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private connection: HubConnection;
  output: string[] = [];  
  message: string = '';
  groupToJoin: string = '';
  targetGroup: string = '';
  groups: string[] = [];

  ngOnInit(){
    const builder = new HubConnectionBuilder();
    const port = Math.floor((Math.random()*100))%2 == 0? 5000 : 5001;
    this.connection = builder.withUrl(`http://localhost:${port}/chat?token=S4cr4t`).build();

    this.connection.on('Send', (msg)=> {
      const groupPrefix = msg.groupName !== '' && msg.groupName !== null ?  msg.groupName + ':' : '';
      this.output.push(`${groupPrefix} ${msg.body}`);
    });
    
    this.connection.start();
  }

  ngOnDestroy(){
    this.connection.stop();
  }

  send(){
    this.connection.invoke('Send', { Body: this.message, GroupName: this.targetGroup}).then(() => {
      this.message = null; 
      this.targetGroup = null;
    });
  }

  async join(){
    await this.connection.invoke('Join', this.groupToJoin);
    this.groups.push(this.groupToJoin);
    this.groupToJoin = null;
  }
}

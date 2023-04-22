import * as SignalR from '@microsoft/signalr';
import { defaultURL } from 'endpoints';

export const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${defaultURL}/chathub`).build();
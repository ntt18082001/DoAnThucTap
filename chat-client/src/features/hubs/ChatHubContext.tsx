import React, { createContext, ReactElement, useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@microsoft/signalr';
import { defaultURL } from 'endpoints';
import { useAppSelector } from 'app/hooks';
import { selectIsLoggedIn, selectToken } from 'features/auth/authSlice';

interface PropsHub {
  children: ReactElement;
}

const ChatHubContext = createContext<any>(null);

function ChatHubProvider(props: PropsHub) {
	const token = useAppSelector(selectToken);
  const [connection, setConnection] = useState<HubConnection>();
	const isLoggedIn = useAppSelector(selectIsLoggedIn);
  
  useEffect(() => {
    if(isLoggedIn && token) {
      const connectionOptions: IHttpConnectionOptions = {
        accessTokenFactory: () => {
          if(token) {
            return token;
          }
          return Promise.reject();
        }
      };
      const createHubConnection = async () => {
        const hubConnection = new HubConnectionBuilder()
          .withUrl(`${defaultURL}/realtime`, connectionOptions)
          .withAutomaticReconnect()
          .build();
        try {
          await hubConnection.start();
          setConnection(hubConnection);
        } catch (e) {
          console.log(e);
        }
      };
			createHubConnection();
		}
  }, [isLoggedIn, token]);

  


  return <ChatHubContext.Provider value={connection}>{props.children}</ChatHubContext.Provider>;
}

export { ChatHubContext, ChatHubProvider };

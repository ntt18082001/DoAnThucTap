import { LinearProgress } from '@mui/material';
import { ChatHubProvider } from 'features/hubs/ChatHubContext';
import { Suspense } from 'react';
import Routing from 'routes';
import { BrowserRouter } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <>
      <Suspense fallback={<LinearProgress color="secondary" />}>
        <ToastContainer position='bottom-right' autoClose={2000} />
        <ChatHubProvider>
          <BrowserRouter>
            <Routing />
          </BrowserRouter>
        </ChatHubProvider>
      </Suspense>
    </>
  );
}

export default App;

import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import counterReducer from '../features/counter/counterSlice';
import darkmodeReducer from 'features/darkmode/darkmodeSlice';
import messageReducer from 'features/message/messageSlice';
import authReducer from 'features/auth/authSlice';
import hubReducer from 'features/hubs/hubSlice';
import friendReducer from 'features/friends/friendSlice';
import notifyReducer from 'features/notify/notifySlice';

import { rtkQueryErrorLogger } from 'middleware/middleware';
import { setupListeners } from '@reduxjs/toolkit/dist/query';
import { authApi } from 'features/auth/auth.service';
import { LOAD_LOCAL_STORAGE } from '../constants/index';
import { localStorageMiddleware } from 'middleware/localStorageMiddleware';
import { validateTokenMiddleware } from 'middleware/validateTokenMiddleware';
import { profileApi } from 'features/profile/profile.service';
import { friendsApi } from 'features/friends/friends.service';
import { notifyApi } from 'features/notify/notify.service';
import { messageApi } from 'features/message/message.service';

const store = configureStore({
  reducer: {
    counter: counterReducer,
    darkmode: darkmodeReducer,
		message: messageReducer,
		hub: hubReducer,
		auth: authReducer,
    [authApi.reducerPath]: authApi.reducer,
    [profileApi.reducerPath]: profileApi.reducer,
    [friendsApi.reducerPath]: friendsApi.reducer,
    friend: friendReducer,
    [notifyApi.reducerPath]: notifyApi.reducer,
    notify: notifyReducer,
    [messageApi.reducerPath]: messageApi.reducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(authApi.middleware, rtkQueryErrorLogger, validateTokenMiddleware, localStorageMiddleware, profileApi.middleware, friendsApi.middleware, notifyApi.middleware, messageApi.middleware)
});

setupListeners(store.dispatch);

store.dispatch({ type: LOAD_LOCAL_STORAGE });

export { store }; 

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;

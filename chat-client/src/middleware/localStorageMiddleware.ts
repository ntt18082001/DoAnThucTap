import { Middleware } from '@reduxjs/toolkit';
import { logout, setAvatar, setToken, setUser } from 'features/auth/authSlice';
import { UserModel } from 'models/useridentity.model';
import { LOAD_LOCAL_STORAGE } from '../constants/index';
import { setDarkmode } from 'features/darkmode/darkmodeSlice';

export const localStorageMiddleware: Middleware = (store) => (next) => (action) => {
  if (action.type === LOAD_LOCAL_STORAGE) {
    const token = localStorage.getItem('token');
    const user = JSON.parse(localStorage.getItem('user') || '{}') as UserModel;
    const darkmode = localStorage.getItem('darkmode');

    if (token && user) {
      store.dispatch(setToken(token));
      store.dispatch(setUser(user));
    }
    if(darkmode === 'true') {
      store.dispatch(setDarkmode(true));
    }
  }

  const result = next(action);
  if (action.type === setToken.type) {
    localStorage.setItem('token', action.payload);
  }

  if (action.type === setUser.type) {
    localStorage.setItem('user', JSON.stringify(action.payload));
  }

  if (action.type === logout.type) {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  if(action.type === setAvatar.type) {
    const user = JSON.parse(localStorage.getItem('user') || '{}') as UserModel;
    user.avatar = action.payload;
    localStorage.setItem('user', JSON.stringify(user));
  }
  return result;
};
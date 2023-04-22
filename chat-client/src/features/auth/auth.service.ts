import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { LoginModel, LoginResponse, RegisterModel, UserModel } from 'models/useridentity.model';
import { getToken, selectToken } from './authSlice';
import { RootState } from 'app/store';

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery({ 
    baseUrl: baseURL,
    prepareHeaders: (headers, { getState }) => {
      const token = getToken(getState() as RootState);
      if(token) {
        headers.set('authorization', `Bearer ${token}`);
      }
      return headers;
    }
 }),
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginModel>({
      query: (data) => ({
        url: '/login/login',
        method: 'POST',
        body: data
      })
    }),
    register: builder.mutation<boolean, RegisterModel>({
      query: (data) => ({
        url: '/account/register',
        method: 'POST',
        body: data
      })
    }),
    updateUser: builder.mutation<UserModel, UserModel>({
      query: (data) => ({
        url: '/account/updateuser',
        method: 'PUT',
        body: data
      })
    })
  })
});

export const { useLoginMutation, useRegisterMutation, useUpdateUserMutation } = authApi;
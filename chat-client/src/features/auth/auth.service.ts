import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { ForgotPasswordModel, LoginModel, LoginResponse, RegisterModel, UserModel, VerifyCodeModel } from 'models/useridentity.model';
import { getToken, selectToken } from './authSlice';
import { RootState } from 'app/store';
import { NewPassword } from '../../models/useridentity.model';

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
    }),
    forgotPassword: builder.mutation<ForgotPasswordModel, ForgotPasswordModel>({
      query: (data) => ({
        url: '/account/forgotpassword',
        method: 'POST',
        body: data
      })
    }),
    checkCode: builder.mutation<VerifyCodeModel, VerifyCodeModel>({
      query: (data) => ({
        url: '/account/checkcode',
        method: 'POST',
        body: data
      })
    }),
    createNewPwd: builder.mutation<boolean, NewPassword>({
      query: (data) => ({
        url: '/account/createnewpwd',
        method: 'POST',
        body: data
      })
    })
  })
});

export const { useLoginMutation, useRegisterMutation, useUpdateUserMutation, useForgotPasswordMutation, useCheckCodeMutation, useCreateNewPwdMutation } = authApi;
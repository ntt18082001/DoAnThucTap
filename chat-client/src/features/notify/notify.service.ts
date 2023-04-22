import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from 'app/store';
import { baseURL } from 'endpoints';
import { getToken } from 'features/auth/authSlice';
import { NotifyModel } from 'models/notify.model';

export const notifyApi = createApi({
  reducerPath: 'notifyApi',
  baseQuery: fetchBaseQuery({
    baseUrl: baseURL,
    prepareHeaders: (headers, { getState }) => {
      const token = getToken(getState() as RootState);
      if (token) {
        headers.set('authorization', `Bearer ${token}`);
      }
      return headers;
    }
  }),
  endpoints: (builder) => ({
    getListNotify: builder.query<NotifyModel[], void>({
      query: () => 'friend/getlistnotify'
    }),
    cancelRequest: builder.mutation<string, string>({
      query: (id) => ({
        url: `/friend/cancelrequest/${id}`,
        method: 'PUT'
      })
    }),
    acceptRequestNotify: builder.mutation<string, string>({
      query: (id) => ({
        url: `/friend/acceptrequestfromnotify/${id}`,
        method: 'PUT'
      })
    })
  })
});

export const { useGetListNotifyQuery, useCancelRequestMutation, useAcceptRequestNotifyMutation } = notifyApi;
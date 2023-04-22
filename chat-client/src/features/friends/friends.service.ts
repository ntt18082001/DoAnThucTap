import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from 'app/store';
import { baseURL } from 'endpoints';
import { getToken } from 'features/auth/authSlice';
import { AddFriend, FriendModel, SearchFriend } from 'models/friend.model';
import { NotifyCancel } from 'models/notify.model';
import { Unfriend } from '../../models/friend.model';

export const friendsApi = createApi({
  reducerPath: 'friendsApi',
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
    getListUserNotFriend: builder.query<FriendModel[], SearchFriend>({
      query: (data) => ({
        url: `/friend/GetListUserNotFriend?id=${data?.id}&search=${data.search}`,
      })
    }),
    addFriend: builder.mutation<string, AddFriend>({
      query: (data) => ({
        url: '/friend/addfriend',
        method: 'POST',
        body: data
      })
    }),
    cancelRequest: builder.mutation<string, AddFriend>({
      query: (data) => ({
        url: '/friend/cancelrequestfromsender',
        method: 'PUT',
        body: data
      })
    }),
    cancelRequestFromReceiver: builder.mutation<NotifyCancel, AddFriend>({
      query: (data) => ({
        url: '/friend/cancelrequestfromreceiver',
        method: 'PUT',
        body: data
      })
    }),
    acceptRequest: builder.mutation<NotifyCancel, AddFriend>({
      query: (data) => ({
        url: '/friend/acceptrequest',
        method: 'PUT',
        body: data
      })
    }),
    unfriend: builder.mutation<boolean, Unfriend>({
      query: (data) => ({
        url: '/friend/unfriend',
        method: 'PUT',
        body: data
      })
    })
  })
});

export const { 
  useGetListUserNotFriendQuery, 
  useAddFriendMutation, 
  useCancelRequestMutation, 
  useCancelRequestFromReceiverMutation, 
  useAcceptRequestMutation, 
  useUnfriendMutation } = friendsApi;
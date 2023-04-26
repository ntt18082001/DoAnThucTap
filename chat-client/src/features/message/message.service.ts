import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from 'app/store';
import { baseURL } from 'endpoints';
import { getToken } from 'features/auth/authSlice';
import { SearchFriend } from 'models/friend.model';
import { ConversationModel, SendMessage, UserMessage } from 'models/messages.model';
import { SeenMessage } from '../../models/messages.model';

export const messageApi = createApi({
  reducerPath: 'messageApi',
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
    getListFriendMessage: builder.query<UserMessage[], SearchFriend>({
      query: (data) => ({
        url: `/message/GetListFriend?id=${data?.id}&search=${data.search}`,
      })
    }),
    getUserSelected: builder.query<UserMessage, string>({
      query: (id) => `/message/GetUserSelected/${id}`
    }),
    sendMessage: builder.mutation<ConversationModel, SendMessage>({
      query: (data) => ({
        url: '/message/SendMessage',
        method: 'POST',
        body: data
      })
    }),
    getListConversation: builder.query<ConversationModel[], void>({
      query: () => '/message/getlistconversation'
    }),
    seenMessage: builder.mutation<string, SeenMessage>({
      query: (data) => ({
        url: '/message/seenmessage',
        method: 'PUT',
        body: data
      })
    }),
    toggleLikeMessage: builder.mutation<boolean, SeenMessage>({
      query: (data) => ({
        url: '/message/togglelikemessage',
        method: 'PUT',
        body: data
      })
    }),
    deleteMessage: builder.mutation<boolean, SeenMessage>({
      query: (data) => ({
        url: '/message/deletemessage',
        method: 'PUT',
        body: data
      })
    })
  })
});

export const { useGetListFriendMessageQuery, useGetUserSelectedQuery, useSendMessageMutation, useGetListConversationQuery, useSeenMessageMutation, useToggleLikeMessageMutation, useDeleteMessageMutation } = messageApi;
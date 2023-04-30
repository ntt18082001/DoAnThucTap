import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from 'app/store';
import { baseURL } from 'endpoints';
import { getToken } from 'features/auth/authSlice';
import { SearchFriend } from 'models/friend.model';
import { ConversationModel, GetListImg, GetListImgResponse, GetMoreMessageResponse, UserMessage } from 'models/messages.model';
import { SeenMessage, GetMoreMessage } from '../../models/messages.model';

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
    sendMessage: builder.mutation<ConversationModel, FormData>({
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
    }),
    getMoreMessage: builder.mutation<GetMoreMessageResponse, GetMoreMessage>({
      query: (data) => ({
        url: '/message/getmoremessage',
        method: 'PUT',
        body: data
      })
    }),
    getListMessageImage: builder.mutation<GetListImgResponse, GetListImg>({
      query: (data) => ({
        url: '/message/getlistmessageimage',
        method: 'POST',
        body: data
      })
    })
  })
});

export const { useGetListFriendMessageQuery, useGetUserSelectedQuery, useSendMessageMutation, useGetListConversationQuery, useSeenMessageMutation, useToggleLikeMessageMutation, useDeleteMessageMutation, useGetMoreMessageMutation, useGetListMessageImageMutation } = messageApi;
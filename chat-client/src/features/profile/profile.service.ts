import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { baseURL } from 'endpoints';
import { RootState } from 'app/store';
import { getToken } from '../auth/authSlice';
import { AvatarResponse, ChangePasswordModel } from 'models/useridentity.model';
import { FriendModel, Unfriend } from 'models/friend.model';

export const profileApi = createApi({
  reducerPath: 'profileApi',
  tagTypes: ['Profile'],
  keepUnusedDataFor: 10,
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
    updateAvatar: builder.mutation<AvatarResponse, FormData>({
      query: (data) => ({
        url: '/account/updateavatar',
        method: 'POST',
        body: data
      })
    }),
    changePassword: builder.mutation<Boolean, ChangePasswordModel>({
      query: (data) => ({
        url: '/account/changepassword',
        method: 'POST',
        body: data
      })
    }),
    getListFriend: builder.query<FriendModel[], void>({
      query: () => '/account/getlistfriend',
      /**
       * providesTags có thể là array hoặc callback return array
       * Nếu có bất kỳ một invalidateTag nào match với providesTags này thì sẽ làm cho getListFreind method chạy lại
       * và cập nhật lại danh sách các bài post cũng như các tags phía dưới
       */
      providesTags: (result) => {
        /**
         * Cái callback này sẽ chạy mỗi khi getListFreind chạy
         * Mong muốn là sẽ return 1 mảng kiểu
         * ```ts
         * interface Tags: {
         *  type: 'Profile';
         *  id: string;
         * }[]
         */
        if (result) {
          const final = [
            ...result.map(({ id }) => ({ type: 'Profile' as const, id })),
            { type: 'Profile' as const, id: 'LIST_FRIEND' }
          ]
          return final
        }
        return [{ type: 'Profile', id: 'LIST_FRIEND' }]
      }
    }),
    unfriendProfile: builder.mutation<boolean, Unfriend>({
      query: (data) => ({
        url: '/friend/unfriend',
        method: 'PUT',
        body: data
      }),
      /**
       * invalidatesTags cung cấp các tag để báo hiệu cho những method nào có providesTags match với nó sẽ bị gọi lại
       * Trong trường hợp này getListFreind sẽ được gọi lại
       */
      invalidatesTags: (result, error, body) => (error ? [] : [{ type: 'Profile', id: 'LIST_FRIEND' }])
    })
  })
});

export const { useUpdateAvatarMutation, useChangePasswordMutation, useGetListFriendQuery, useUnfriendProfileMutation } = profileApi;
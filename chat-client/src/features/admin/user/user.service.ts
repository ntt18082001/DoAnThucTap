import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { RootState } from 'app/store';
import { baseURL } from 'endpoints';
import { getToken } from 'features/auth/authSlice';
import { Paged, PagedResponse, UserPaged } from '../../../models/paged.model';

export const userApi = createApi({
  reducerPath: 'userApi',
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
    getAllUser: builder.query<PagedResponse<UserPaged>, Paged>({
      query: (params) => ({
        url: '/admin/user/getalluser',
        params: params
      })
    })
  })
});

export const { useGetAllUserQuery } = userApi;
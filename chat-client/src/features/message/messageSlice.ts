import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { DataMessage } from "models/messages.model";
import { RootState } from '../../app/store';
import { UserModel } from '../../models/useridentity.model';
import _ from 'lodash';


const initialState: DataMessage = {
   currentUserId: '',
   selectedUser: undefined,
   conversations: {}
}

export const messageSlice = createSlice({
   name: 'message',
   initialState,
   reducers: {
      setSelectedUser: (state, action: PayloadAction<UserModel | undefined>) => {
         if (action.payload) {
            state.selectedUser = action.payload;
         }
      },
      setMessageConversation: (state, action: PayloadAction<any>) => {
         if (state.selectedUser) {
            if (state.conversations[state.selectedUser.id]) {
               if (action.payload) {
                  state.conversations[state.selectedUser.id].push(action.payload);
               }
            } else {
               state.conversations[state.selectedUser.id] = [];
               if (action.payload) {
                  state.conversations[state.selectedUser.id].push(action.payload);
               }
            }
         }
      },
      setCurrentUserId: (state, action: PayloadAction<string>) => {
         state.currentUserId = action.payload;
      },
      setListMessage: (state, action: PayloadAction<any>) => {
         if (state.selectedUser) {
            if (_.isEqual(state.conversations[state.selectedUser.id], [])) {
               if (action.payload) {
                  console.log("test", action.payload);
                  state.conversations[state.selectedUser.id] = action.payload;
               }
            }
         }
      }
   }
});

export const { setSelectedUser, setMessageConversation, setCurrentUserId, setListMessage } = messageSlice.actions;

export const selectCurrentUserId = (state: RootState) => state.message.currentUserId;
export const selectSelectedUser = (state: RootState) => state.message.selectedUser;
export const selectConversations = (state: RootState) => state.message.conversations;

export default messageSlice.reducer;
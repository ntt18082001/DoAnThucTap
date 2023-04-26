import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { DataMessage, UserMessage } from "models/messages.model";
import { RootState } from '../../app/store';
import { ConversationModel } from '../../models/messages.model';


const initialState: DataMessage = {
   selectedUser: undefined,
   conversations: []
}

export const messageSlice = createSlice({
   name: 'message',
   initialState,
   reducers: {
      setConversations: (state, action: PayloadAction<ConversationModel[]>) => {
         if (action.payload) {
            state.conversations = action.payload;
         }
      },
      setSelectedUser: (state, action: PayloadAction<UserMessage | undefined>) => {
         if (action.payload) {
            state.selectedUser = action.payload;
         }
      },
      setMessage: (state, action: PayloadAction<ConversationModel>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.id === action.payload.id);
            if (indexConv > -1) {
               const tempConv = state.conversations[indexConv];
               tempConv.conversation.push(action.payload.lastMessage);
               tempConv.lastMessage = action.payload.lastMessage;
               state.conversations.splice(indexConv, 1);
               state.conversations.unshift(tempConv);
            } else {
               state.conversations.unshift(action.payload);
            }
         }
      },
      setSeenLastMessage: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.lastMessage.id === action.payload);
            if (indexConv > -1) {
               state.conversations[indexConv].lastMessage.isSeen = true;
            }
         }
      },
      toggleLikeMessage: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.conversation.some(msg => msg.id === action.payload));
            if (indexConv > -1) {
               const indexMsg = state.conversations[indexConv].conversation.findIndex(x => x.id === action.payload);
               if(indexMsg > -1) {
                  state.conversations[indexConv].conversation[indexMsg].isLiked = !state.conversations[indexConv].conversation[indexMsg].isLiked;
               }
            }
         }
      },
      setDeleteMessage: (state, action: PayloadAction<string>) => {
         if(action.payload) {
            const indexConv = state.conversations.findIndex(x => x.conversation.some(msg => msg.id === action.payload));
            if (indexConv > -1) {
               const indexMsg = state.conversations[indexConv].conversation.findIndex(x => x.id === action.payload);
               if(indexMsg > -1) {
                  state.conversations[indexConv].conversation[indexMsg].isDelete = true;
               }
               if(state.conversations[indexConv].lastMessage.id === action.payload) {
                  state.conversations[indexConv].lastMessage.isDelete = true;
               }
            }
         }
      }
   }
});

export const { setConversations, setSelectedUser, setMessage, setSeenLastMessage, toggleLikeMessage, setDeleteMessage } = messageSlice.actions;

export const selectSelectedUser = (state: RootState) => state.message.selectedUser;
export const selectConversations = (state: RootState) => state.message.conversations;
export const selectListUserConversation = (state: RootState) => state.message.conversations.map(item => item.friend);
export const selectSelectedUserId = (state: RootState) => state.message.selectedUser?.id;
export const selectCountLastMessageNotSeen = (state: RootState) => 
   state.message.conversations.filter(x => x.lastMessage.receiverId === state.auth.user?.id && !x.lastMessage.isSeen).length;

export default messageSlice.reducer;
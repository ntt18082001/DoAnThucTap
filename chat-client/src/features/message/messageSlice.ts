import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { DataMessage, UserMessage } from "models/messages.model";
import { RootState } from '../../app/store';
import { ConversationModel, GetMoreMessageResponse } from '../../models/messages.model';


const initialState: DataMessage = {
   selectedUser: undefined,
   conversations: [],
   listFindUser: [],
   isScrollMsg: false
}

export const messageSlice = createSlice({
   name: 'message',
   initialState,
   reducers: {
      setIsScrollTrue: (state, action: PayloadAction<void>) => {
         state.isScrollMsg = true;
      },
      setIsScrollFalse: (state, action: PayloadAction<void>) => {
         state.isScrollMsg = false;
      },
      setConversations: (state, action: PayloadAction<ConversationModel[]>) => {
         if (action.payload) {
            state.conversations = action.payload;
         }
      },
      setListFindUser: (state, action: PayloadAction<UserMessage[]>) => {
         if (action.payload) {
            state.listFindUser = action.payload;
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
               const indexMsg = state.conversations[indexConv].conversation.findIndex(x => x.id === action.payload);
               if (indexMsg > -1) {
                  state.conversations[indexConv].conversation[indexMsg].isSeen = true;
               }
            }
         }
      },
      toggleLikeMessage: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.conversation.some(msg => msg.id === action.payload));
            if (indexConv > -1) {
               const indexMsg = state.conversations[indexConv].conversation.findIndex(x => x.id === action.payload);
               if (indexMsg > -1) {
                  state.conversations[indexConv].conversation[indexMsg].isLiked = !state.conversations[indexConv].conversation[indexMsg].isLiked;
               }
            }
         }
      },
      setDeleteMessage: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.conversation.some(msg => msg.id === action.payload));
            if (indexConv > -1) {
               const indexMsg = state.conversations[indexConv].conversation.findIndex(x => x.id === action.payload);
               if (indexMsg > -1) {
                  state.conversations[indexConv].conversation[indexMsg].isDelete = true;
               }
               if (state.conversations[indexConv].lastMessage.id === action.payload) {
                  state.conversations[indexConv].lastMessage.isDelete = true;
               }
            }
         }
      },
      setFriendConnected: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.friend.id === action.payload);
            if (indexConv > -1) {
               state.conversations[indexConv].friend.isOnline = true;
               if (state.selectedUser && state.selectedUser?.id === state.conversations[indexConv].friend.id) {
                  state.selectedUser.isOnline = true;
               }
            }
            const indexFindUser = state.listFindUser.findIndex(x => x.id === action.payload);
            if (indexFindUser > -1) {
               state.listFindUser[indexFindUser].isOnline = true;
            }
         }
      },
      setFriendDisconnected: (state, action: PayloadAction<string>) => {
         if (action.payload) {
            const indexConv = state.conversations.findIndex(x => x.friend.id === action.payload);
            if (indexConv > -1) {
               state.conversations[indexConv].friend.isOnline = false;
               if (state.selectedUser && state.selectedUser?.id === state.conversations[indexConv].friend.id) {
                  state.selectedUser.isOnline = false;
               }
            }
            const indexFindUser = state.listFindUser.findIndex(x => x.id === action.payload);
            if (indexFindUser > -1) {
               state.listFindUser[indexFindUser].isOnline = false;
            }
         }
      },
      setListMessageToConversation: (state, action: PayloadAction<GetMoreMessageResponse>) => {
         if(action.payload) {
            const indexConv = state.conversations.findIndex(x => x.id === action.payload.conversationId);
            if(indexConv > -1) {
               state.conversations[indexConv].canGetMore = action.payload.canGetMore;
               state.conversations[indexConv].conversation = action.payload.messages.concat(state.conversations[indexConv].conversation);
            }
         }
      }
   }
});

export const { setConversations, setSelectedUser, setMessage, setSeenLastMessage, toggleLikeMessage, setDeleteMessage, setFriendConnected, setFriendDisconnected, setListFindUser, setIsScrollFalse, setIsScrollTrue, setListMessageToConversation } = messageSlice.actions;

export const selectSelectedUser = (state: RootState) => state.message.selectedUser;
export const selectConversations = (state: RootState) => state.message.conversations;
export const selectListUserConversation = (state: RootState) => state.message.conversations.map(item => item.friend);
export const selectSelectedUserId = (state: RootState) => state.message.selectedUser?.id;
export const selectCountLastMessageNotSeen = (state: RootState) =>
   state.message.conversations.filter(x => x.lastMessage.receiverId === state.auth.user?.id && !x.lastMessage.isSeen).length;
export const selectListFindUser = (state: RootState) => state.message.listFindUser;
export const selectIsScroll = (state: RootState) => state.message.isScrollMsg;

export default messageSlice.reducer;
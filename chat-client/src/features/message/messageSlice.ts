import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { DataMessage, UserMessage } from "models/messages.model";
import { RootState } from '../../app/store';
import { ConversationModel, GetMoreMessageResponse, UpdateInfoConvResponse, UpdateNicknameResponse } from '../../models/messages.model';


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
      },
      setInfoConv: (state, action: PayloadAction<UpdateInfoConvResponse>) => {
         if(action.payload) {
            const indexConv = state.conversations.findIndex(x => x.id === action.payload.conversationId);
            if(indexConv > -1) {
               state.conversations[indexConv].infoConversation = action.payload.infoConversationDTO;
               state.conversations[indexConv].conversation.push(action.payload.notifyMessage);
               state.conversations[indexConv].lastMessage = action.payload.notifyMessage;
               const tempConv = state.conversations[indexConv];
               state.conversations.splice(indexConv, 1);
               state.conversations.unshift(tempConv);
            }
         }
      },
      setNicknameConv: (state, action: PayloadAction<UpdateNicknameResponse>) => {
         if(action.payload) {
            const indexConv = state.conversations.findIndex(x => x.id === action.payload.conversationId);
            if(indexConv > -1) {
               if(state.conversations[indexConv].userNickname && state.conversations[indexConv].userNickname.userId === action.payload.nickname.userId) {
                  state.conversations[indexConv].userNickname = action.payload.nickname;
               }
               if(state.conversations[indexConv].friendNickname && state.conversations[indexConv].friendNickname.userId === action.payload.nickname.userId) {
                  state.conversations[indexConv].friendNickname = action.payload.nickname;
               }
               if(!state.conversations[indexConv].userNickname && state.conversations[indexConv].userId === action.payload.nickname.userId) {
                  state.conversations[indexConv].userNickname = action.payload.nickname;
               }
               if(!state.conversations[indexConv].friendNickname && state.conversations[indexConv].friendId === action.payload.nickname.userId) {
                  state.conversations[indexConv].userNickname = action.payload.nickname;
               }
               state.conversations[indexConv].conversation.push(action.payload.notifyMessage);
               state.conversations[indexConv].lastMessage = action.payload.notifyMessage;
               const tempConv = state.conversations[indexConv];
               state.conversations.splice(indexConv, 1);
               state.conversations.unshift(tempConv);
            }
         }
      }
   }
});

export const { setConversations, setSelectedUser, setMessage, setSeenLastMessage, toggleLikeMessage, setDeleteMessage, setFriendConnected, setFriendDisconnected, setListFindUser, setIsScrollFalse, setIsScrollTrue, setListMessageToConversation, setInfoConv, setNicknameConv } = messageSlice.actions;

export const selectSelectedUser = (state: RootState) => state.message.selectedUser;
export const selectConversations = (state: RootState) => state.message.conversations;
export const selectListUserConversation = (state: RootState) => state.message.conversations.map(item => item.friend);
export const selectSelectedUserId = (state: RootState) => state.message.selectedUser?.id;
export const selectCountLastMessageNotSeen = (state: RootState) =>
   state.message.conversations.filter(x => x.lastMessage.receiverId === state.auth.user?.id && !x.lastMessage.isSeen).length;
export const selectListFindUser = (state: RootState) => state.message.listFindUser;
export const selectIsScroll = (state: RootState) => state.message.isScrollMsg;

export default messageSlice.reducer;
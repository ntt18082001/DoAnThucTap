import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { FriendModel } from 'models/friend.model';
import { Unfriend } from '../../models/friend.model';

export interface FriendState {
  friends: FriendModel[];
}

const initialState: FriendState = {
  friends: []
}

export const friendSlice = createSlice({
  name: 'friend',
  initialState,
  reducers: {
    setFriends: (state, action: PayloadAction<FriendModel[]>) => {
      state.friends = action.payload;
    },
    setFriendSendRequest: (state, action: PayloadAction<string>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isSendRequest = true;
      }
    },
    setCancelRequest: (state, action: PayloadAction<string>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isSendRequest = false;
      }
    },
    setReceiverRequest: (state, action: PayloadAction<string>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isReceiverRequest = true;
      }
    },
    setCancelReceiverRequest: (state, action: PayloadAction<string>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isReceiverRequest = false;
      }
    },
    setAcceptRequest: (state, action: PayloadAction<string>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isReceiverRequest = false;
        state.friends[indexFriend].isSendRequest = false;
        state.friends[indexFriend].isFriendShip = true;
      }
    },
    setUnfriend: (state, action: PayloadAction<Unfriend>) => {
      const indexFriend = state.friends.findIndex(x => x.id === action.payload.senderId || x.id === action.payload.receiverId);
      if(indexFriend !== -1) {
        state.friends[indexFriend].isFriendShip = false;
      }
    }
  }
});

export const { setFriends, setFriendSendRequest, setCancelRequest, setReceiverRequest, setCancelReceiverRequest, setAcceptRequest, setUnfriend } = friendSlice.actions;

export const selectFriends = (state: RootState) => state.friend.friends;

export default friendSlice.reducer;
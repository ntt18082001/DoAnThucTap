export interface FriendModel {
  id: string;
  fullName: string;
  avatar: string;
  mutualFriends: string;
  isSendRequest: boolean;
  isReceiverRequest: boolean;
  isFriendShip: boolean;
}

export interface SearchFriend {
  id?: string;
  search: string;
}

export interface AddFriend {
  senderId?: string;
  receiverId?: string;
}

export interface FriendRequest {
  senderId: string;
  receiverId: string;
  sender: FriendModel;
}

export interface Unfriend {
  senderId?: string;
  receiverId: string;
}
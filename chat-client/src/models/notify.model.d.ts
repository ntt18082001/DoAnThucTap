export interface NotifyModel {
  id: string;
  senderId: string;
  receiverId: string;
  fullName: string;
  avatar: string;
  mutualFriends: string;
  isCancel: boolean;
  isAccept: boolean;
}

export interface NotifyCancel {
  notifyId: string;
  senderId: string;
  receiverId: string;
}
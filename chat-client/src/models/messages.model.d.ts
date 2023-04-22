import { UserModel } from './useridentity.model';

export interface DataMessage {
	currentUserId: string;
	selectedUser?: UserModel;
	conversations: any;
}

export interface ConversationModel {
	conversation: Message[];
}

export interface Message {
	id: string;
	message: string;
	senderId: string;
	receiverId?: string;
	isSeen?: boolean;
   sendingTime: Date;
}
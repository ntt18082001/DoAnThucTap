export interface UserMessage {
	id: string;
	name: string;
	avatar: string;
}

export interface DataMessage {
	selectedUser?: UserMessage;
	conversations: ConversationModel[];
}

export interface ConversationModel {
	id: string;
	userId: string;
	friendId: string;
	canGetMore: boolean;
	friend: UserMessage;
	user: UserMessage;
	conversation: Message[];
	lastMessage: Message;
}

export interface Message {
	id: string;
	conversationId: string;
	content: string;
	senderId: string;
	receiverId?: string;
	isSeen?: boolean;
  sentTime: Date;
	urlMessage?: string;
	isLiked?: boolean;
	isDelete?: boolean;
}

export interface SendMessage {
	userId?: string;
	friendId?: string;
	content: string;
}

export interface SeenMessage {
	senderId?: string;
	receiverId?: string;
	id?: string;
}
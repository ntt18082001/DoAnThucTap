export interface UserMessage {
	id: string;
	name: string;
	avatar: string;
	isOnline?: boolean;
}

export interface DataMessage {
	selectedUser?: UserMessage;
	conversations: ConversationModel[];
	listFindUser: UserMessage[];
	isScrollMsg: boolean;
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
	colorConversation: ColorConversation;
	infoConversation: InfoConversation;
}

export interface Message {
	id: string;
	conversationId: string;
	content: string;
	senderId: string;
	receiverId?: string;
	isSeen?: boolean;
  sentTime: Date;
	urlImage?: string;
	isLiked?: boolean;
	isDelete?: boolean;
	isNotify: boolean;
}

export interface SendMessage {
	userId?: string;
	friendId?: string;
	content: string;
	file?: File;
}

export interface SeenMessage {
	senderId?: string;
	receiverId?: string;
	id?: string;
}

export interface GetMoreMessage {
	conversationId: string;
	lastMessageId: string;
	lengthMessages: string;
}

export interface GetMoreMessageResponse {
	conversationId: string;
	lastMessageId: string;
	canGetMore: boolean;
	messages: Message[];
}

export interface GetListImg {
	id?: string; // id conversation
	idLastMsg?: string;
	lengthMessagesImg?: string;
}

export interface GetListImgResponse {
	id: string; // id conversation
	idLastMessage?: string;
	messages: Message[];
	canGetMore: boolean;
}

export interface ColorConversation {
	backgroundColorCode: string;
	textColorCode: string;
}

export interface InfoConversation {
	UserNickname: string;
	FriendNickname: string;
	MainEmoji: string;
}
import { HubConnection } from '@microsoft/signalr';
import { Grid } from '@mui/material';
import { ChatHubContext } from 'features/hubs/ChatHubContext';
import React, { useContext, useEffect } from 'react';

import { useAppDispatch, useAppSelector } from '../../../../app/hooks';
import { Message } from '../../../../models/messages.model';
import { selectConversations, selectCurrentUserId, selectSelectedUser, setListMessage, setMessageConversation } from '../../messageSlice';
import ConversationContent from './ConversationContent';
import ConversationFormMessage from './ConversationFormMessage';
import ConversationTitle from './ConversationTitle';
import axiosClient from 'api/axiosClient';


type Props = {};

const Conversation = (props: Props) => {
	console.log("Conversation render");
	const hubConnection = useContext<HubConnection>(ChatHubContext);

	const dispatch = useAppDispatch();

	const selectedUser = useAppSelector(selectSelectedUser);
	const currentUserId = useAppSelector(selectCurrentUserId);
	const conversations = useAppSelector(selectConversations);
	let conversationsOfSelectedUser: Message[] = [];

	if (selectedUser) {
		conversationsOfSelectedUser = conversations[selectedUser.id];
		if (!conversationsOfSelectedUser) {
			dispatch(setMessageConversation(null));
		}
	}
	
	const handleSubmitMessage = (message: string) => {
		if (message.trim().length === 0) {
			return;
		}
		hubConnection.send("SendMessage", currentUserId, selectedUser?.id.toString(), message);
	}

	useEffect(() => {
		hubConnection.on("ReceiveMessage", message => {
			dispatch(setMessageConversation(message));
		});
	}, [currentUserId, dispatch, hubConnection, selectedUser?.id]);

	return (
		<>
			<Grid
				container
				direction="column"
				justifyContent="space-between"
				alignItems="stretch"
				sx={{ height: '100%' }}
			>
				<ConversationTitle
					user={selectedUser}
				/>
				<ConversationContent
					conversations={conversationsOfSelectedUser}
				/>
				<ConversationFormMessage onSubmit={handleSubmitMessage} />
			</Grid>
		</>
	);
};

export default Conversation;
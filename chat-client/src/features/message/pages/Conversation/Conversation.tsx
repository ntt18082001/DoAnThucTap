import { HubConnection } from '@microsoft/signalr';
import { Grid } from '@mui/material';
import { ChatHubContext } from 'features/hubs/ChatHubContext';
import React, { useContext, useEffect } from 'react';

import { useAppDispatch, useAppSelector } from '../../../../app/hooks';
import { Message, SendMessage } from '../../../../models/messages.model';
import { selectSelectedUser, setMessage } from '../../messageSlice';
import ConversationContent from './ConversationContent';
import ConversationFormMessage from './ConversationFormMessage';
import ConversationTitle from './ConversationTitle';
import axiosClient from 'api/axiosClient';
import { selectUserId } from 'features/auth/authSlice';
import { useSeenMessageMutation, useSendMessageMutation } from 'features/message/message.service';


type Props = {};

const Conversation = (props: Props) => {
	const dispatch = useAppDispatch();
	const currendUserId = useAppSelector(selectUserId);
	const selectedUser = useAppSelector(selectSelectedUser);
	const selectConversation = useAppSelector(state => state.message.conversations
		.find(item => (item.userId === currendUserId && item.friendId === selectedUser?.id) || (item.userId === selectedUser?.id && item.friendId === currendUserId)));

	const [sendMessage, { data, isSuccess }] = useSendMessageMutation();
	
	const handleSubmitMessage = async (message: string) => {
		try {
			if(message === '') {
				return;
			}
			const data: SendMessage = {
				userId: currendUserId,
				friendId: selectedUser?.id,
				content: message
			}
			await sendMessage(data).unwrap();
		} catch(error) {
			console.log(error);
		}
	}

	useEffect(() => {
		if(isSuccess && data) {
			dispatch(setMessage(data));
		}
	}, [data, dispatch, isSuccess]);

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
					conversations={selectConversation}
				/>
				<ConversationFormMessage onSubmit={handleSubmitMessage} />
			</Grid>
		</>
	);
};

export default Conversation;
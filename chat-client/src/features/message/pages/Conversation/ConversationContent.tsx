import { Box, Grid, Typography } from '@mui/material';
import React, { useEffect, useRef } from 'react';

import { useAppSelector } from '../../../../app/hooks';
import { ConversationModel } from '../../../../models/messages.model';
import ConversationMessage from './ConversationMessage';
import { selectUserId } from 'features/auth/authSlice';

interface Props {
	conversations?: ConversationModel;
};

const ConversationContent = (props: Props) => {
	const messageEndRef = useRef<HTMLDivElement>(null);
	const currentUserId = useAppSelector(selectUserId);

	useEffect(() => {
		if (messageEndRef.current) {
			messageEndRef.current.scrollIntoView({ block: 'start', behavior: 'smooth' });
		}
	});

	return (
		<Grid
			item
			display="flex"
			alignItems="flex-end"
			sx={{
				height: '743px',
				color: "#E4E6EB",
				paddingTop: '12px'
			}}
		>
			<Box sx={{ pl: 1, pb: 2, maxHeight: '100%', width: '100%', overflowY: 'auto' }}>
				{props.conversations?.conversation.map((conv, index) => {
					let isAvatar = false;

					if (props.conversations) {
						if (props.conversations.conversation[index]?.receiverId !== props.conversations.conversation[index + 1]?.receiverId) {
							isAvatar = true;
						}
					}

					return (
						<ConversationMessage
							key={conv.id}
							me={conv.senderId === currentUserId}
							message={conv}
							isAvatar={isAvatar}
						/>
					)
				})}
				<Typography component="div" ref={messageEndRef} />
			</Box>
		</Grid>
	);
};

export default ConversationContent;

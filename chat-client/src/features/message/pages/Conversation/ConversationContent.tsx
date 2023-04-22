import { Box, Grid, Typography } from '@mui/material';
import React, { useEffect, useRef } from 'react';

import { useAppSelector } from '../../../../app/hooks';
import { Message } from '../../../../models/messages.model';
import { selectCurrentUserId } from '../../messageSlice';
import ConversationMessage from './ConversationMessage';

interface Props {
	conversations?: Message[];
};

const ConversationContent = (props: Props) => {
	const messageEndRef = useRef<HTMLDivElement>(null);
	const currentUserId = useAppSelector(selectCurrentUserId);

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
			}}
		>
			<Box sx={{ pl: 1, pb: 2, maxHeight: '100%', width: '100%', overflowY: 'auto' }}>
				{props.conversations?.map((conv, index) => {
					let isAvatar = false;

					if (props.conversations) {
						if (props.conversations[index]?.receiverId !== props.conversations[index + 1]?.receiverId) {
							isAvatar = true;
						}
					}

					return (
						<ConversationMessage
							key={conv.id}
							me={conv.senderId.toString() === currentUserId}
							message={conv.message}
							isAvatar={isAvatar}
                     sendingTime={conv.sendingTime}
						/>
					)
				})}
				<Typography component="div" ref={messageEndRef} />
			</Box>
		</Grid>
	);
};

export default ConversationContent;

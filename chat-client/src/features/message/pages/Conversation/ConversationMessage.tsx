import { Avatar, Box, Tooltip, Typography } from '@mui/material';
import React from 'react';

type Props = {
	message: string;
	me: boolean;
	isAvatar: boolean;
   sendingTime: Date;
};

const ConversationMessage = (props: Props) => {

	let avatar;
   console.log(props.sendingTime.toString());

	if (!props.me && props.isAvatar) {
		avatar = <Tooltip title="Tiến Sĩ" placement="left">
			<Avatar
				alt="Remy Sharp"
				sx={{ width: 24, height: 24, mr: 1 }}
			/>
		</Tooltip>;
	}

	return (
		<Box
			display="flex"
			alignItems="flex-end"
			className={props.me ? 'me' : ''}
			sx={{ pl: props.isAvatar ? 1 : 5 }}
		>
			{avatar}
			<Tooltip title={props.sendingTime.toLocaleDateString()} placement="right">
				<Typography
					variant='body2'
					sx={{
						backgroundColor: 'purple',
						maxWidth: '50%',
						p: '8px 15px',
						borderRadius: 4,
						mt: '2px',
						width: 'fit-content',
					}}
				>
					{props.message}
				</Typography>
			</Tooltip>
		</Box>
	);
};

export default ConversationMessage;

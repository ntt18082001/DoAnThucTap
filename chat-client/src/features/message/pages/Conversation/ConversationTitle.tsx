import { Avatar, Box, Grid, Typography } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { urlAvatar } from 'endpoints';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React from 'react';
import { borderColorDarkmode, borderColorDefault, defaultAvatar } from '../../../../constants/index';
import { UserModel } from '../../../../models/useridentity.model';

interface Props {
	user?: UserModel;
};

const ConversationTitle = (props: Props) => {
	console.log("Conversation title render");
	const isDarkmode = useAppSelector(selectIsDarkmode);

	const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;
	return (
		<Grid
			item
			sx={{ borderBottom: borderColor, p: 2 }}
		>
			<Box display="flex" alignItems="center">
				{props?.user ? 
					<Avatar alt="T" sx={{ mr: 1 }} src={`${urlAvatar}/${props?.user?.avatar}`} /> 
					: <Avatar alt="T" sx={{ mr: 1 }} src={`${urlAvatar}/${defaultAvatar}`} /> 
				}
				<Typography variant='body1'>
					{props.user?.nickname ? props.user?.nickname : props.user?.fullName}
				</Typography>
			</Box>
		</Grid>
	);
};

export default ConversationTitle;

import FiberManualRecordIcon from '@mui/icons-material/FiberManualRecord';
import { Avatar, Box, Typography } from '@mui/material';
import React from 'react';
import { NavLink } from 'react-router-dom';
import { useAppSelector } from '../../../app/hooks';
import { UserModel } from '../../../models/useridentity.model';
import CustomMessageButton from '../../../utils/CustomMessageButton';
import { selectIsDarkmode } from '../../darkmode/darkmodeSlice';
import { selectCurrentUserId } from '../messageSlice';
import { colorMsgDarkmode, blackColor, routeMessage } from '../../../constants/index';
import { urlAvatar } from 'endpoints';

interface Props {
	user: UserModel;
	onClick: () => void;
};

const MessageListItem = (props: Props) => {
	const currentUserId = useAppSelector(selectCurrentUserId);
	const isDarkmode = useAppSelector(selectIsDarkmode);

	const { lastMesg, id, fullName, nickname, avatar } = props.user;

	let lastMessage = "";

	if (lastMesg) {
		lastMessage = lastMesg.senderId === currentUserId ? `Bạn: ${lastMesg.message}` : lastMesg.message;
	}

	return (
		<NavLink to={`/${routeMessage}/${id}`} style={{ width: '100%' }} onClick={props.onClick}>
			<CustomMessageButton
				style={{ display: 'flex', alignItems: 'center', color: isDarkmode ? colorMsgDarkmode : blackColor }}
			>
				<Box width="20%">
					<Avatar
						alt='Tiến Sĩ'
						sx={{ width: '50px', height: '50px', mr: 2 }}
						src={`${urlAvatar}/${avatar}`}
					/>
				</Box>
				<Box width="70%" sx={{ textAlign: 'start', overflow: 'hidden', textOverflow: 'ellipsis' }}>
					<Typography variant='body1'>
						{nickname !== null ? nickname : fullName}
					</Typography>
					<Typography variant="caption" sx={{ whiteSpace: 'nowrap' }}>
						{lastMessage}
					</Typography>
				</Box>
				<Box width="10%" sx={{ textAlign: 'end' }}>
					<FiberManualRecordIcon color="secondary" />
				</Box>
			</CustomMessageButton>
		</NavLink>
	);
};

export default MessageListItem;

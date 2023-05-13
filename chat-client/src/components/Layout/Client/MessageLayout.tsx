import { Grid, LinearProgress } from '@mui/material';
import MessageList from '../../../features/message/pages/MessageList';
import React, { Suspense, useEffect } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { useAppSelector } from 'app/hooks';
import { selectIsLoggedIn } from 'features/auth/authSlice';

export interface MessageProps {
}

const MessageLayout: React.FC = (props: MessageProps): JSX.Element => {
	const isLoggedIn = useAppSelector(selectIsLoggedIn);
	const navigate = useNavigate();
	useEffect(() => {
		document.title = "Tin nhắn";
		if(!isLoggedIn) {
			navigate("/");
		}
	}, [isLoggedIn, navigate]);

	return (
		<Grid
			container
			component="main"
			sx={{ height: '100%' }}
		>
			<MessageList />
			<Suspense fallback={<LinearProgress color='secondary' />}>
				<Outlet />
			</Suspense>
		</Grid>
	);
}


export default React.memo(MessageLayout);
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import { CardHeader, Grid, IconButton, Skeleton, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';

import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { blackColor, borderColorDarkmode, borderColorDefault, colorMsgDarkmode, mainColor } from '../../../constants';
import { selectIsDarkmode } from '../../darkmode/darkmodeSlice';
import { selectSelectedUser, setSelectedUser } from '../messageSlice';
import MessageListItem from './MessageListItem';

type Props = {};

function MessageList(props: Props) {
	const [isLoading, setIsLoading] = useState(true);

	const isDarkmode = useAppSelector(selectIsDarkmode);
	const dispatch = useAppDispatch();

	const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;
	const colorText = isDarkmode ? mainColor : blackColor;
	const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;
	const bgColorHover = isDarkmode ? 'rgb(255,255,255,.2)' : "#dbdbdb";
	const bgColorSkeleton = isDarkmode ? 'grey.700' : '';

	useEffect(() => {
		const timer = setTimeout(() => {
			setIsLoading(false);
		}, 2000);

		return () => {
			clearTimeout(timer);
		}
	}, []);

	return (
		<Grid
			item
			xs={5}
			sm={5}
			md={4}
			lg={3}
			sx={{ borderRight: borderColor, paddingTop: 1, paddingRight: 1, paddingLeft: 2 }}
		>
			<Grid container>
				<Grid
					item
					xs
					container
					alignItems="center"
				>
					<Typography
						variant="h5"
						sx={{
							fontWeight: 'bold',
							fontSize: '1.5rem',
							color: isDarkmode ? colorMsgDarkmode : blackColor
						}}
					>
						Chat
					</Typography>
				</Grid>
				<Grid item xs>
					<Typography
						variant="h5"
						textAlign="end"
						sx={{
							fontWeight: 'bold',
							fontSize: '1.5rem',
							color: '#E4E6EB',
							marginRight: 1
						}}
					>
						<IconButton
							aria-label="Tùy chọn"
							size="large"
							sx={{
								color: colorText,
								backgroundColor: bgColor,
								":hover": {
									backgroundColor: bgColorHover
								}
							}}
						>
							<MoreHorizIcon />
						</IconButton>
					</Typography>
				</Grid>
			</Grid>
			<Grid container sx={{ mt: 2, overflowY: "auto", maxHeight: '568px' }}>
				{/* {
					!isLoading ? (
						listUserTalked.map(user => (
							<MessageListItem
								key={user.id}
								user={user}
								onClick={() => {
									dispatch(setSelectedUser(user));
								}}
							/>
						))
					) : (
						<>
							<CardHeader
								sx={{ width: '100%', backgroundColor: bgColor, mb: 0.5 }}
								avatar={
									<Skeleton
										animation="pulse"
										variant="circular"
										width={50}
										height={50}
										sx={{ bgcolor: bgColorSkeleton }}
									/>
								}
								title={
									<Skeleton
										animation="pulse"
										height={15}
										width="80%"
										sx={{ bgcolor: bgColorSkeleton, mb: '6px' }}
									/>
								}
								subheader={
									<Skeleton animation="pulse" height={12} width="50%" sx={{ bgcolor: bgColorSkeleton }} />
								}
							/>
							<CardHeader
								sx={{ width: '100%', backgroundColor: bgColor, mb: 0.5 }}
								avatar={
									<Skeleton
										animation="pulse"
										variant="circular"
										width={50}
										height={50}
										sx={{ bgcolor: bgColorSkeleton }}
									/>
								}
								title={
									<Skeleton
										animation="pulse"
										height={15}
										width="80%"
										sx={{ bgcolor: bgColorSkeleton, mb: '6px' }}
									/>
								}
								subheader={
									<Skeleton animation="pulse" height={12} width="50%" sx={{ bgcolor: bgColorSkeleton }} />
								}
							/>
						</>
					)
				} */}
			</Grid>
		</Grid>
	);
}

export default React.memo(MessageList);

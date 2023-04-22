import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import GitHubIcon from '@mui/icons-material/GitHub';
import { Box, Fab, Grid, Typography } from '@mui/material';
import { routeLogin } from '../../constants';
import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';

import classes from '../../assets/css/home.module.css';
import { useAppSelector } from '../../app/hooks';
import { selectIsLoggedIn, selectUser } from '../../features/auth/authSlice';

interface HomeProps {
}

const HomeLayout: React.FC = (props: HomeProps): JSX.Element => {
	const isLoggedIn = useAppSelector(selectIsLoggedIn);
	const user = useAppSelector(selectUser);

	useEffect(() => {
		document.title = "Trang chủ";
	}, []);

	return (
		<Grid
			container
			direction="row"
			justifyContent="center"
			sx={{ height: '100%', textAlign: 'center' }}
		>
			<Box>
				<GitHubIcon
					className={classes["App-logo"]}
					sx={{ fontSize: 200 }}
					color="secondary"
				/>
				<Typography
					variant='h5'
					component="div"
				>
					Kết nối với những người bạn yêu quý!
				</Typography>
				{
					!isLoggedIn ? (
						<Link to={`/${routeLogin}`}>
							<Fab
								variant="extended"
								color="secondary"
								aria-label="Đăng nhập"
								sx={{ mt: 4 }}
								size="medium"
							>
								<AccountCircleIcon sx={{ mr: 1 }} />
								Đăng nhập để nhắn tin
							</Fab>
						</Link>
					) : <Typography
						variant="h4"
						component="div"
						sx={{ mt: 2 }}
					>
						Xin chào {user?.fullName}
					</Typography>
				}
			</Box>
		</Grid>
	);
}

export default React.memo(HomeLayout);
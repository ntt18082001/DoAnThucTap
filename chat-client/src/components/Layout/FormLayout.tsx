import { ThemeProvider } from "@emotion/react";
import GitHubIcon from '@mui/icons-material/GitHub';
import HomeIcon from '@mui/icons-material/Home';
import { Avatar, Box, Button, createTheme, CssBaseline, Grid, Paper, Typography } from '@mui/material';
import { ReactElement, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAppSelector } from '../../app/hooks';
import { selectIsDarkmode } from '../../features/darkmode/darkmodeSlice';
import { selectIsLoggedIn } from "features/auth/authSlice";

interface FormLayoutProps {
	children: ReactElement;
	title: string;
	urlLink: string;
	titleUrl: string;
}

const theme = createTheme();

export default function FormLayout(props: FormLayoutProps) {
	const navigate = useNavigate();
	const isDarkmode = useAppSelector(selectIsDarkmode);
	const isLoggedIn = useAppSelector(selectIsLoggedIn);

	useEffect(() => {
		if(isLoggedIn) {
			navigate('/');
		}
	}, [isLoggedIn, navigate]);

	return (
		<ThemeProvider theme={theme}>
			<Grid container component="main" sx={{ height: '100vh' }}>
				<CssBaseline />
				<Grid
					item
					xs={false}
					sm={4}
					md={7}
					sx={{
						backgroundImage: 'url(https://source.unsplash.com/random)',
						backgroundRepeat: 'no-repeat',
						backgroundColor: (t) =>
							t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
						backgroundSize: 'cover',
						backgroundPosition: 'center',
					}}
				/>
				<Grid
					item
					xs={12}
					sm={8}
					md={5}
					component={Paper}
					elevation={6}
					square
					className={isDarkmode ? 'darkmode-bg-color darkmode-color' : '' }
				>
					<Box
						sx={{
							my: 8,
							mx: 4,
							display: 'flex',
							flexDirection: 'column',
							alignItems: 'center',
						}}
					>
						<Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
							<GitHubIcon />
						</Avatar>
						<Typography component="h1" variant="h5">
							{props.title}
						</Typography>

						{props.children}

						<Grid container>
							<Grid item xs>
								<Link to="/">
									<Button variant="outlined" endIcon={<HomeIcon />} color="secondary">
										Trang chủ
									</Button>
								</Link>
							</Grid>
							<Grid item xs>
								<Link to={`/${props.urlLink}`}>
									<Button variant="outlined" color="secondary">
										{props.titleUrl}
									</Button>
								</Link>
							</Grid>
						</Grid>
					</Box >
				</Grid >
			</Grid >
		</ThemeProvider >
	);
}

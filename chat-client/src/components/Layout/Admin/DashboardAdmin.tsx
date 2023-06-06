import React, { Suspense, useEffect } from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Container from '@mui/material/Container';
import Link from '@mui/material/Link';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import { mainColor, blackColor, routeUserAdmin, routeAdmin } from '../../../constants/index';
import { LinearProgress, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import ColorLensIcon from '@mui/icons-material/ColorLens';
import PeopleIcon from '@mui/icons-material/People';
import { Outlet, useNavigate } from 'react-router-dom';
import { selectToken } from 'features/auth/authSlice';
import { isAdmin } from 'features/auth/handleJwt';
import { toast } from 'react-toastify';

function Copyright(props: any) {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  return (
    <Typography
      variant="body2"
      sx={{ color: isDarkmode ? mainColor : blackColor }}
      align="center"
      {...props}
    >
      {'Copyright © '}
      <Link color="inherit" target="_blank" href="https://github.com/ntt18082001">
        Tiến Sĩ
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

const drawerWidth: number = 300;

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9),
        },
      }),
    },
  })
);

const mdTheme = createTheme();

function DashboardContent() {
  const navigate = useNavigate();
  const token = useAppSelector(selectToken);
  const darkmode = useAppSelector(selectIsDarkmode);
  const [open, setOpen] = React.useState(true);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  useEffect(() => {
    document.title = 'Quản lý Website';
  }, []);

  useEffect(() => {
    if (!token) {
      toast.warning('Vui lòng đăng nhập!');
      navigate('/');
    }
    if (token && !isAdmin(token)) {
      toast.warning('Hạn chế quyền truy cập!');
      navigate('/');
    }
  }, [token, navigate]);

  const navigateToUserAdmin = () => {
    navigate(`${routeUserAdmin}`);
  };

  const navigateToDashboard = () => {
    navigate(`/${routeAdmin}`);
  };

  return (
    <ThemeProvider theme={mdTheme}>
      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        <AppBar position="absolute" open={open}></AppBar>
        <Drawer variant="permanent" open={open}>
          <Toolbar
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'flex-end',
              px: [1],
            }}
          >
            {open && (
              <>
                <Typography
                  variant="h5"
                  sx={{ m: 'auto', cursor: 'pointer' }}
                  onClick={navigateToDashboard}
                >
                  Dashboard
                </Typography>
                <IconButton onClick={toggleDrawer}>
                  <ChevronLeftIcon />
                </IconButton>
              </>
            )}
            {!open && (
              <>
                <IconButton
                  edge="start"
                  color="inherit"
                  aria-label="open drawer"
                  onClick={toggleDrawer}
                >
                  <MenuIcon />
                </IconButton>
              </>
            )}
          </Toolbar>
          <Divider />
          <List component="nav">
            <ListItemButton onClick={navigateToUserAdmin}>
              <ListItemIcon>
                <PeopleIcon />
              </ListItemIcon>
              <ListItemText primary="Danh sách user" />
            </ListItemButton>
            <ListItemButton>
              <ListItemIcon>
                <ColorLensIcon />
              </ListItemIcon>
              <ListItemText primary="Quản lý màu sắc" />
            </ListItemButton>
          </List>
        </Drawer>
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            height: 'calc(100vh - 65px)',
            overflow: 'auto',
          }}
          className={darkmode ? 'darkmode-bg-color' : ''}
        >
          <Container maxWidth="xl" sx={{ p: 4, height: '100%' }}>
            <Suspense fallback={<LinearProgress color="secondary" />}>
              <Outlet />
            </Suspense>
            <Copyright
              sx={{ position: 'fixed', bottom: 0, left: 0, right: 0, width: '100%', p: '15px' }}
            />
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

function DashboardAdmin() {
  return <DashboardContent />;
}

export default React.memo(DashboardAdmin);

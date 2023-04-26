import MapsUgcIcon from '@mui/icons-material/MapsUgc';
import {
  AppBar,
  Avatar,
  Badge,
  Box,
  Button,
  Divider,
  IconButton,
  ListItemIcon,
  Menu,
  MenuItem,
  styled,
  Switch,
  Toolbar,
  Tooltip,
  Typography,
  Zoom,
} from '@mui/material';
import { baseURL } from 'endpoints';
import * as React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';

import { useAppDispatch, useAppSelector } from '../../app/hooks';
import {
  backGroundHeader,
  blackColor,
  bodyBgColorDefault,
  borderColorDarkmode,
  borderColorDefault,
  mainColor,
  routeFriends,
  routeLogin,
  routeMessage,
  routeProfile,
  routeRegister,
} from '../../constants';
import { logout, selectUser, selectIsLoggedIn } from '../../features/auth/authSlice';
import { selectIsDarkmode, updateDarkmode } from '../../features/darkmode/darkmodeSlice';
import { GroupAdd, Logout } from '@mui/icons-material';
import NotificationsActiveIcon from '@mui/icons-material/NotificationsActive';
import NotifyList from 'features/notify/pages/NotifyList';
import { selectLengthNotify } from 'features/notify/notifySlice';
import { selectCountLastMessageNotSeen } from 'features/message/messageSlice';

interface HeaderProps {}

const MaterialUISwitch = styled(Switch)(({ theme }) => ({
  width: 62,
  height: 34,
  padding: 7,
  '& .MuiSwitch-switchBase': {
    margin: 1,
    padding: 0,
    transform: 'translateX(6px)',
    '&.Mui-checked': {
      color: '#fff',
      transform: 'translateX(22px)',
      '& .MuiSwitch-thumb:before': {
        backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 20 20"><path fill="${encodeURIComponent(
          '#fff'
        )}" d="M4.2 2.5l-.7 1.8-1.8.7 1.8.7.7 1.8.6-1.8L6.7 5l-1.9-.7-.6-1.8zm15 8.3a6.7 6.7 0 11-6.6-6.6 5.8 5.8 0 006.6 6.6z"/></svg>')`,
      },
      '& + .MuiSwitch-track': {
        opacity: 1,
        backgroundColor: theme.palette.mode === 'dark' ? '#8796A5' : '#aab4be',
      },
    },
  },
  '& .MuiSwitch-thumb': {
    backgroundColor: theme.palette.mode === 'dark' ? '#003892' : '#001e3c',
    width: 32,
    height: 32,
    '&:before': {
      content: "''",
      position: 'absolute',
      width: '100%',
      height: '100%',
      left: 0,
      top: 0,
      backgroundRepeat: 'no-repeat',
      backgroundPosition: 'center',
      backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 20 20"><path fill="${encodeURIComponent(
        '#fff'
      )}" d="M9.305 1.667V3.75h1.389V1.667h-1.39zm-4.707 1.95l-.982.982L5.09 6.072l.982-.982-1.473-1.473zm10.802 0L13.927 5.09l.982.982 1.473-1.473-.982-.982zM10 5.139a4.872 4.872 0 00-4.862 4.86A4.872 4.872 0 0010 14.862 4.872 4.872 0 0014.86 10 4.872 4.872 0 0010 5.139zm0 1.389A3.462 3.462 0 0113.471 10a3.462 3.462 0 01-3.473 3.472A3.462 3.462 0 016.527 10 3.462 3.462 0 0110 6.528zM1.665 9.305v1.39h2.083v-1.39H1.666zm14.583 0v1.39h2.084v-1.39h-2.084zM5.09 13.928L3.616 15.4l.982.982 1.473-1.473-.982-.982zm9.82 0l-.982.982 1.473 1.473.982-.982-1.473-1.473zM9.305 16.25v2.083h1.389V16.25h-1.39z"/></svg>')`,
    },
  },
  '& .MuiSwitch-track': {
    opacity: 1,
    backgroundColor: theme.palette.mode === 'dark' ? '#8796A5' : '#aab4be',
    borderRadius: 20 / 2,
  },
}));

export function Header(props: HeaderProps) {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const isLoggedIn = useAppSelector(selectIsLoggedIn);
  const user = useAppSelector(selectUser);
  const notifyLength = useAppSelector(selectLengthNotify);
  const notifyMessage = useAppSelector(selectCountLastMessageNotSeen);

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [menuNotify, setMenuNotify] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const openNotify = Boolean(menuNotify);

  const handleClickMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleCloseMenu = () => {
    setAnchorEl(null);
  };

  const handleClickMenuNotify = (event: React.MouseEvent<HTMLElement>) => {
    setMenuNotify(event.currentTarget);
  };
  const handleCloseMenuNotify = () => {
    setMenuNotify(null);
  };

  const darkBackground = () => {
    if (isDarkmode) {
      return {
        borderBottom: borderColorDarkmode,
        backgroundColor: backGroundHeader,
        color: mainColor,
      };
    } else {
      return {
        borderBottom: borderColorDefault,
        backgroundColor: bodyBgColorDefault,
        color: blackColor,
      };
    }
  };

  const redirectToProfile = () => {
    navigate(`${routeProfile}/${user?.id}`);
  };

  return (
    <Box sx={{ flexGrow: 1, height: '65px' }}>
      <AppBar position="static" sx={darkBackground()}>
        <Toolbar sx={{ height: '65px' }}>
          <Typography variant="h5" component="div" sx={{ flexGrow: 1 }}>
            <NavLink to="/" style={{ color: isDarkmode ? mainColor : blackColor }}>
              TS-CHAT
            </NavLink>
          </Typography>

          <MaterialUISwitch
            sx={{ m: 1 }}
            onChange={() => dispatch(updateDarkmode())}
            checked={isDarkmode}
          />
          {isLoggedIn ? (
            <>
              <Typography variant="h5" component="div">
                <IconButton aria-label="Friendship" size="large">
                  <Tooltip TransitionComponent={Zoom} title="Kết bạn">
                    <NavLink to={routeFriends} style={{ color: darkBackground().color }}>
                      <GroupAdd />
                    </NavLink>
                  </Tooltip>
                </IconButton>
              </Typography>
              <Typography variant="h5" component="div">
                <IconButton aria-label="Message" size="large">
                  <Tooltip TransitionComponent={Zoom} title="Tin nhắn">
                    <NavLink to={routeMessage} style={{ color: darkBackground().color }}>
                      <Badge badgeContent={notifyMessage} showZero color="error" sx={{ marginRight: '10px' }}>
                        <MapsUgcIcon />
                      </Badge>
                    </NavLink>
                  </Tooltip>
                </IconButton>
              </Typography>
              <Typography variant="h5" component="div">
                <IconButton aria-label="Notify" size="large" onClick={handleClickMenuNotify}>
                  <Tooltip TransitionComponent={Zoom} title="Thông báo">
                    <Badge
                      badgeContent={notifyLength}
                      showZero
                      color="error"
                      sx={{ marginRight: '10px', color: darkBackground().color }}
                    >
                      <NotificationsActiveIcon />
                    </Badge>
                  </Tooltip>
                </IconButton>
              </Typography>
              <Menu
                anchorEl={menuNotify}
                id="account-menu"
                open={openNotify}
                onClose={handleCloseMenuNotify}
                PaperProps={{
                  elevation: 0,
                  sx: {
                    filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                    mt: 1.5,
                    maxHeight: '702px',
                    overflowY: 'scroll',
                    '& .MuiAvatar-root': {
                      width: 32,
                      height: 32,
                      ml: -0.5,
                      mr: 1,
                    },
                    '&:before': {
                      content: '""',
                      display: 'block',
                      position: 'absolute',
                      top: 0,
                      right: 14,
                      width: 10,
                      height: 10,
                      bgcolor: 'background.paper',
                      transform: 'translateY(-50%) translateX(-100%) rotate(45deg)',
                      zIndex: 0,
                    },
                  },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
              >
                <NotifyList />
              </Menu>
              <Box
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  textAlign: 'center',
                }}
              >
                <Tooltip title="Account">
                  <IconButton
                    onClick={handleClickMenu}
                    size="small"
                    sx={{ ml: 2 }}
                    aria-controls={open ? 'account-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={open ? 'true' : undefined}
                  >
                    <Avatar alt={user?.fullName} src={`${baseURL}/${user?.avatar}`} />
                  </IconButton>
                </Tooltip>
              </Box>
              <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleCloseMenu}
                onClick={handleCloseMenu}
                PaperProps={{
                  elevation: 0,
                  sx: {
                    overflow: 'visible',
                    filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                    mt: 1.5,
                    '& .MuiAvatar-root': {
                      width: 32,
                      height: 32,
                      ml: -0.5,
                      mr: 1,
                    },
                    '&:before': {
                      content: '""',
                      display: 'block',
                      position: 'absolute',
                      top: 0,
                      right: 14,
                      width: 10,
                      height: 10,
                      bgcolor: 'background.paper',
                      transform: 'translateY(-50%) rotate(45deg)',
                      zIndex: 0,
                    },
                  },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
              >
                <MenuItem onClick={redirectToProfile}>
                  <Avatar alt={user?.fullName} src={`${baseURL}/${user?.avatar}`} />
                  {user?.fullName}
                </MenuItem>
                <Divider />
                <MenuItem color="inherit" onClick={() => dispatch(logout())}>
                  <ListItemIcon>
                    <Logout fontSize="small" />
                  </ListItemIcon>
                  Logout
                </MenuItem>
              </Menu>
            </>
          ) : (
            <>
              <Typography variant="h2" component="div">
                <NavLink to={routeLogin} style={{ color: darkBackground().color }}>
                  <Button color="inherit">Đăng nhập</Button>
                </NavLink>
              </Typography>
              <Typography variant="h2" component="div">
                <NavLink to={routeRegister} style={{ color: darkBackground().color }}>
                  <Button color="inherit">Đăng ký</Button>
                </NavLink>
              </Typography>
            </>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
}

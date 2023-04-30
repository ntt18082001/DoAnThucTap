import {
  Avatar,
  Box,
  Drawer,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  ListSubheader,
  Typography,
} from '@mui/material';
import { baseURL } from 'endpoints';
import React, { useState } from 'react';
import { selectSelectedUser } from '../messageSlice';
import {  useAppSelector } from 'app/hooks';
import { OnlineAvatar } from 'utils/OnlineAvatar';
import { OfflineAvatar } from 'utils/OfflineAvatar';
import SendIcon from '@mui/icons-material/Send';
import DeleteIcon from '@mui/icons-material/Delete';
import ImageIcon from '@mui/icons-material/Image';
import ImageListDrawer from './Conversation/Components/ImageListDrawer';

type Props = {
  isOpen: boolean;
  toggleDrawer: (toggle: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => void;
};

const MessageInfo = (props: Props) => {
  const selectedUser = useAppSelector(selectSelectedUser);
  const [drawerImg, setDrawerImg] = useState(false);

  const handleToggleDrawerImg =
    (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
      if (
        event.type === 'keydown' &&
        ((event as React.KeyboardEvent).key === 'Tab' ||
          (event as React.KeyboardEvent).key === 'Shift')
      ) {
        return;
      }
      setDrawerImg(open);
    };

  return (
    <Drawer anchor="right" open={props.isOpen} onClose={props.toggleDrawer(false)}>
      <Box role="presentation" sx={{ width: '400px' }}>
        <Box
          sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20px' }}
        >
          {selectedUser?.isOnline && (
            <OnlineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                sx={{ width: '80px', height: '80px' }}
                alt={selectedUser?.name}
                src={`${baseURL}/${selectedUser?.avatar}`}
              />
            </OnlineAvatar>
          )}
          {!selectedUser?.isOnline && (
            <OfflineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                sx={{ width: '80px', height: '80px' }}
                alt={selectedUser?.name}
                src={`${baseURL}/${selectedUser?.avatar}`}
              />
            </OfflineAvatar>
          )}
          <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
            {selectedUser?.name}
          </Typography>
          {selectedUser?.isOnline ? (
            <Typography className="status-connect" component="p">
              Online
            </Typography>
          ) : (
            <Typography className="status-connect" component="p">
              Offline
            </Typography>
          )}
        </Box>
        <List
          sx={{ width: '100%', bgcolor: 'background.paper' }}
          component="nav"
          aria-labelledby="nested-list-subheader"
          subheader={
            <ListSubheader component="div" id="nested-list-subheader">
              Tùy chỉnh đoạn chat
            </ListSubheader>
          }
        >
          <ListItemButton>
            <ListItemIcon>
              <SendIcon />
            </ListItemIcon>
            <ListItemText primary="Sửa biệt danh" />
          </ListItemButton>
          <ListItemButton onClick={handleToggleDrawerImg(true)}>
            <ListItemIcon>
              <ImageIcon />
            </ListItemIcon>
            <ListItemText primary="Tất cả hình ảnh" />
          </ListItemButton>
          <ImageListDrawer isOpen={drawerImg} toggleDrawer={handleToggleDrawerImg} />
          <ListItemButton sx={{ color: 'red' }}>
            <ListItemIcon sx={{ color: 'red' }}>
              <DeleteIcon />
            </ListItemIcon>
            <ListItemText primary="Xóa đoạn chat" />
          </ListItemButton>
        </List>
      </Box>
    </Drawer>
  );
};

export default React.memo(MessageInfo);

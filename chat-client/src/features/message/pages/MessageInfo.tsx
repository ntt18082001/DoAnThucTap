import { Avatar, Box, Drawer, Typography } from '@mui/material';
import { baseURL } from 'endpoints';
import React from 'react';
import { selectSelectedUser } from '../messageSlice';
import { useAppSelector } from 'app/hooks';

type Props = {
  isOpen: boolean;
  toggleDrawer: (toggle: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => void;
};

const MessageInfo = (props: Props) => {
  const selectedUser = useAppSelector(selectSelectedUser);

  return (
    <Drawer
      anchor="right"
      open={props.isOpen}
      onClose={props.toggleDrawer(false)}
    >
      <Box role="presentation" sx={{ width: '400px' }}>
        <Box
          sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20px' }}
        >
          <Avatar
            sx={{ width: '80px', height: '80px' }}
            alt={selectedUser?.name}
            src={`${baseURL}/${selectedUser?.avatar}`}
          />
          <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
            {selectedUser?.name}
          </Typography>
        </Box>
      </Box>
    </Drawer>
  );
};

export default React.memo(MessageInfo);

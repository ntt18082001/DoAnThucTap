import { Avatar, Box, Grid, IconButton, Typography } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React, { useState } from 'react';
import {
  borderColorDarkmode,
  borderColorDefault,
  defaultAvatar,
} from '../../../../constants/index';
import { UserMessage } from 'models/messages.model';
import InfoIcon from '@mui/icons-material/Info';
import { colorMsgDarkmode } from '../../../../constants/index';
import MessageInfo from '../MessageInfo';

interface Props {
  user?: UserMessage;
}

const ConversationTitle = (props: Props) => {
  const [drawerMsg, setDrawerMsg] = useState(false);

  const isDarkmode = useAppSelector(selectIsDarkmode);
  const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;
  const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;
  const bgColorHover = isDarkmode ? 'rgb(255,255,255,.2)' : '#dbdbdb';

  const handleToggleDrawer = (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
    if (
      event.type === 'keydown' &&
      ((event as React.KeyboardEvent).key === 'Tab' ||
        (event as React.KeyboardEvent).key === 'Shift')
    ) {
      return;
    }
    setDrawerMsg(open);
  };

  return (
    <Grid
      item
      sx={{
        borderBottom: borderColor,
        p: '11px'
      }}
      className='o-hidden'
    >
      <Box display="flex" alignItems="center" justifyContent="space-between">
        <Box display="flex" alignItems="center">
          {props?.user ? (
            <Avatar alt="T" sx={{ mr: 1 }} src={`${baseURL}/${props?.user?.avatar}`} />
          ) : (
            <Avatar alt="T" sx={{ mr: 1 }} src={`${baseURL}/${defaultAvatar}`} />
          )}
          <Typography variant="body1">{props.user?.name}</Typography>
        </Box>
        <Box>
          <IconButton
            aria-label="Tùy chọn"
            size="medium"
            color="primary"
            sx={{
              mr: 1,
              backgroundColor: bgColor,
              ':hover': {
                backgroundColor: bgColorHover,
              },
            }}
            onClick={handleToggleDrawer(true)}
          >
            <InfoIcon />
          </IconButton>
          <MessageInfo isOpen={drawerMsg} toggleDrawer={handleToggleDrawer} />
        </Box>
      </Box>
    </Grid>
  );
};

export default ConversationTitle;

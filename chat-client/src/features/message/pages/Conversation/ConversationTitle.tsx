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
import { ConversationModel, UserMessage } from 'models/messages.model';
import InfoIcon from '@mui/icons-material/Info';
import { colorMsgDarkmode } from '../../../../constants/index';
import MessageInfo from '../MessageInfo';
import { OnlineAvatar } from 'utils/OnlineAvatar';
import { OfflineAvatar } from 'utils/OfflineAvatar';

interface Props {
  user?: UserMessage;
  conv?: ConversationModel;
}

const ConversationTitle = (props: Props) => {
  const [drawerMsg, setDrawerMsg] = useState(false);

  const isDarkmode = useAppSelector(selectIsDarkmode);
  const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;
  const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;
  const bgColorHover = isDarkmode ? 'rgb(255,255,255,.2)' : '#dbdbdb';

  const { conv } = props;

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
        p: 1,
      }}
      className="o-hidden"
    >
      <Box display="flex" alignItems="center" justifyContent="space-between">
        <Box display="flex" alignItems="center">
          {props?.user && props.user.isOnline && (
            <OnlineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                alt={props.user.name}
                src={`${baseURL}/${props?.user?.avatar}`}
              />
            </OnlineAvatar>
          )}
          {props?.user && !props.user.isOnline && (
            <OfflineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                alt={props.user.name}
                src={`${baseURL}/${props?.user?.avatar}`}
              />
            </OfflineAvatar>
          )}
          {!props.user && <Avatar alt="T" sx={{ mr: 1 }} src={`${baseURL}/${defaultAvatar}`} />}
          <Box sx={{ ml: 1 }}>
            <Typography variant="body1">{props.conv?.friendNickname ? props.conv.friendNickname.nickname : props.user?.name}</Typography>
            {props?.user && props.user.isOnline ? (
              <Typography className='status-connect' component="p">Online</Typography>
            ) : (
              <Typography className='status-connect' component="p">Offline</Typography>
            )}
          </Box>
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
          <MessageInfo isOpen={drawerMsg} toggleDrawer={handleToggleDrawer} conv={conv} />
        </Box>
      </Box>
    </Grid>
  );
};

export default ConversationTitle;

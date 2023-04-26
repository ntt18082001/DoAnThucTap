import FiberManualRecordIcon from '@mui/icons-material/FiberManualRecord';
import { Avatar, Box, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useAppSelector } from '../../../app/hooks';
import CustomMessageButton from '../../../utils/CustomMessageButton';
import { selectIsDarkmode } from '../../darkmode/darkmodeSlice';
import { colorMsgDarkmode, blackColor, routeMessage } from '../../../constants/index';
import { baseURL } from 'endpoints';
import { Message, UserMessage } from 'models/messages.model';
import { selectUserId } from 'features/auth/authSlice';

interface Props {
  user: UserMessage;
  friend: UserMessage;
  onClick: (user: UserMessage, lastMessage: Message | undefined) => void;
}

const MessageListItem = (props: Props) => {
  const [lastMessage, setLastMessage] = useState('');
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const currentUserId = useAppSelector(selectUserId);
  const { id, name, avatar } = props.user;
  const lastMesg = useAppSelector(
    (state) =>
      state.message.conversations.find(
        (item) =>
          (item.userId === currentUserId && item.friendId === id) ||
          (item.userId === id && item.friendId === currentUserId)
      )?.lastMessage
  );

  useEffect(() => {
    if (lastMesg) {
      if (!lastMesg.isDelete) {
        setLastMessage(
          lastMesg.senderId === currentUserId ? `Bạn: ${lastMesg.content}` : lastMesg.content
        );
      } else {
        setLastMessage(
          lastMesg.senderId === currentUserId ? `Bạn đã xóa tin nhắn` : `${props.friend.name} đã xóa tin nhắn`
        );
      }
    }
  }, [currentUserId, lastMesg, props.friend.name, setLastMessage]);

  return (
    <NavLink
      to={`/${routeMessage}/${id}`}
      style={{ width: '100%' }}
      onClick={() => props.onClick(props.user, lastMesg)}
    >
      <CustomMessageButton
        style={{
          display: 'flex',
          alignItems: 'center',
          color: isDarkmode ? colorMsgDarkmode : blackColor,
        }}
      >
        <Box sx={{ mr: '15px' }}>
          <Avatar
            alt="Tiến Sĩ"
            sx={{ width: '50px', height: '50px' }}
            src={`${baseURL}/${avatar}`}
          />
        </Box>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            width: '100%',
          }}
        >
          <Box sx={{ textAlign: 'start', overflow: 'hidden', textOverflow: 'ellipsis' }}>
            <Typography variant="body1">{name}</Typography>
            <Typography
              variant="caption"
              sx={{
                whiteSpace: 'nowrap',
                fontWeight: currentUserId !== lastMesg?.senderId && !lastMesg?.isSeen ? '600' : '',
              }}
            >
              {lastMessage}
            </Typography>
          </Box>
          {currentUserId !== lastMesg?.senderId && !lastMesg?.isSeen && !lastMesg?.isDelete && (
            <Box width="10%" sx={{ textAlign: 'end' }}>
              <FiberManualRecordIcon color="secondary" sx={{ width: '20px', height: '20px' }} />
            </Box>
          )}
          {currentUserId === lastMesg?.senderId && lastMesg?.isSeen && !lastMesg?.isDelete && (
            <Box width="10%" sx={{ textAlign: 'end' }}>
              <Avatar
                alt={props.friend.name}
                src={`${baseURL}/${props.friend.avatar}`}
                sx={{ width: '16px', height: '16px' }}
              />
            </Box>
          )}
        </Box>
      </CustomMessageButton>
    </NavLink>
  );
};

export default MessageListItem;

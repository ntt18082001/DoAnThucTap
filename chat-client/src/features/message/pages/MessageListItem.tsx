import FiberManualRecordIcon from '@mui/icons-material/FiberManualRecord';
import { Avatar, Box, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import CustomMessageButton from '../../../utils/CustomMessageButton';
import { selectIsDarkmode } from '../../darkmode/darkmodeSlice';
import { colorMsgDarkmode, blackColor, routeMessage } from '../../../constants/index';
import { baseURL } from 'endpoints';
import { Message, NicknameConv, UserMessage } from 'models/messages.model';
import { selectUserId } from 'features/auth/authSlice';
import { OnlineAvatar } from 'utils/OnlineAvatar';
import { OfflineAvatar } from 'utils/OfflineAvatar';
import { setIsScrollFalse } from '../messageSlice';

interface Props {
  user: UserMessage;
  friend: UserMessage;
  onClick: (user: UserMessage, lastMessage: Message | undefined) => void;
  userNickname?: NicknameConv;
  friendNickname?: NicknameConv;
}

const MessageListItem = (props: Props) => {
  const dispatch = useAppDispatch();
  const [lastMessage, setLastMessage] = useState('');
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const currentUserId = useAppSelector(selectUserId);
  const { id, name, avatar, isOnline } = props.user;
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
        if (!lastMesg.isNotify) {
          setLastMessage(
            lastMesg.senderId === currentUserId
              ? `Bạn: ${lastMesg.content ? lastMesg.content : 'Đã gửi hình ảnh'}`
              : lastMesg.content
              ? lastMesg.content
              : `${props.friend.name} đã gửi hình ảnh`
          );
        } else {
          setLastMessage(
            lastMesg.senderId === currentUserId
              ? 'Bạn đã thay đổi thông tin cuộc trò chuyện'
              : `${props.friend.name} đã thay đổi thông tin cuộc trò chuyện`
          );
        }
      } else {
        setLastMessage(
          lastMesg.senderId === currentUserId
            ? `Bạn đã xóa tin nhắn`
            : `${props.friend.name} đã xóa tin nhắn`
        );
      }
    }
  }, [currentUserId, lastMesg, props.friend.name, setLastMessage]);

  return (
    <NavLink
      to={`/${routeMessage}/${id}`}
      style={{ width: '100%', overflow: 'hidden' }}
      onClick={() => {
        props.onClick(props.user, lastMesg);
        dispatch(setIsScrollFalse());
      }}
    >
      <CustomMessageButton
        style={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          color: isDarkmode ? colorMsgDarkmode : blackColor,
        }}
      >
        <Box sx={{ display: 'flex' }}>
          <Box sx={{ mr: '15px' }}>
            {isOnline && (
              <OnlineAvatar
                overlap="circular"
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                variant="dot"
              >
                <Avatar
                  alt={name}
                  sx={{ width: '50px', height: '50px' }}
                  src={`${baseURL}/${avatar}`}
                />
              </OnlineAvatar>
            )}
            {!isOnline && (
              <OfflineAvatar
                overlap="circular"
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                variant="dot"
              >
                <Avatar
                  alt={name}
                  sx={{ width: '50px', height: '50px' }}
                  src={`${baseURL}/${avatar}`}
                />
              </OfflineAvatar>
            )}
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
              <Typography variant="body1">{props.friendNickname ? props.friendNickname.nickname : name}</Typography>
              <Typography
                variant="caption"
                component="p"
                sx={{
                  whiteSpace: 'nowrap',
                  fontWeight:
                    currentUserId !== lastMesg?.senderId && !lastMesg?.isSeen ? '600' : '',
                  overflow: 'hidden',
                  maxWidth: '190px',
                  textOverflow: 'ellipsis',
                }}
              >
                {lastMessage}
              </Typography>
            </Box>
          </Box>
        </Box>
        {currentUserId !== lastMesg?.senderId && !lastMesg?.isSeen && !lastMesg?.isDelete && (
          <Box width="10%" sx={{ textAlign: 'end' }}>
            <FiberManualRecordIcon color="secondary" sx={{ width: '20px', height: '20px' }} />
          </Box>
        )}
        {currentUserId === lastMesg?.senderId && lastMesg?.isSeen && !lastMesg?.isDelete && (
          <Box width="10%" sx={{ textAlign: 'end' }}>
            <Avatar
              alt={props.user.name}
              src={`${baseURL}/${props.user.avatar}`}
              sx={{ width: '16px', height: '16px' }}
            />
          </Box>
        )}
      </CustomMessageButton>
    </NavLink>
  );
};

export default MessageListItem;

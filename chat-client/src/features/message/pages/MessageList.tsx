import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import { Grid, IconButton, Typography } from '@mui/material';
import React from 'react';

import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import {
  blackColor,
  borderColorDarkmode,
  borderColorDefault,
  colorMsgDarkmode,
  mainColor,
} from '../../../constants';
import { selectIsDarkmode } from '../../darkmode/darkmodeSlice';
import { selectConversations, setSeenLastMessage, setSelectedUser } from '../messageSlice';
import MessageListItem from './MessageListItem';
import SkeletonMessage from './SkeletonMessage';
import BorderColorIcon from '@mui/icons-material/BorderColor';
import FindUserMsg from './FindUserMsg';
import { selectUserId } from 'features/auth/authSlice';
import { Message, SeenMessage, UserMessage } from 'models/messages.model';
import { useSeenMessageMutation } from '../message.service';
import { useEffect } from 'react';

type Props = {};

function MessageList(props: Props) {
  const dispatch = useAppDispatch();

  const currentUserId = useAppSelector(selectUserId);
  const listConversation = useAppSelector(selectConversations);

  const isDarkmode = useAppSelector(selectIsDarkmode);
  const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;
  const colorText = isDarkmode ? mainColor : blackColor;
  const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;
  const bgColorHover = isDarkmode ? 'rgb(255,255,255,.2)' : '#dbdbdb';

  const [seenMessage, { data, isSuccess }] = useSeenMessageMutation();

  const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(null);

  const handleClickFindUser = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleCloseFindUser = () => {
    setAnchorEl(null);
  };
  const open = Boolean(anchorEl);

  const handleSelectedUser = async (user: UserMessage, lastMessage: Message | undefined) => {
    dispatch(setSelectedUser(user));
    if (lastMessage?.receiverId === currentUserId && !lastMessage?.isSeen) {
      try {
        const data: SeenMessage = {
          senderId: lastMessage?.senderId,
          receiverId: lastMessage?.receiverId,
          id: lastMessage?.id,
        };
        await seenMessage(data).unwrap();
      } catch (error) {
        console.log(error);
      }
    }
  };

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setSeenLastMessage(data));
    }
  }, [data, dispatch, isSuccess]);

  return (
    <Grid
      item
      xs={5}
      sm={5}
      md={4}
      lg={3}
      xl={2}
      sx={{ borderRight: borderColor, paddingTop: '15px', paddingRight: 1, paddingLeft: 2 }}
    >
      <Grid container>
        <Grid item xs container alignItems="center">
          <Typography
            variant="h5"
            sx={{
              fontWeight: 'bold',
              fontSize: '1.5rem',
              color: isDarkmode ? colorMsgDarkmode : blackColor,
            }}
          >
            Chat
          </Typography>
        </Grid>
        <Grid item xs>
          <Typography
            variant="h5"
            textAlign="end"
            sx={{
              fontWeight: 'bold',
              fontSize: '1.5rem',
              color: '#E4E6EB',
              marginRight: 1,
            }}
          >
            <IconButton
              aria-label="Tùy chọn"
              size="medium"
              sx={{
                mr: 1,
                color: colorText,
                backgroundColor: bgColor,
                ':hover': {
                  backgroundColor: bgColorHover,
                },
              }}
            >
              <MoreHorizIcon />
            </IconButton>
            <IconButton
              aria-label="Tin nhắn mới"
              size="medium"
              sx={{
                color: colorText,
                backgroundColor: bgColor,
                ':hover': {
                  backgroundColor: bgColorHover,
                },
              }}
              onClick={handleClickFindUser}
            >
              <BorderColorIcon />
            </IconButton>
          </Typography>
          <FindUserMsg
            isOpen={open}
            anchorEl={anchorEl}
            handleHiddenPopper={handleCloseFindUser}
          />
        </Grid>
      </Grid>
      <Grid container sx={{ mt: 2, overflowY: 'auto', maxHeight: '568px' }}>
        {listConversation.length > 0 &&
          listConversation.map((conv) => {
            const user = conv.userId === currentUserId ? conv.friend : conv.user;
            const friend = conv.friendId === conv.lastMessage.senderId ? conv.friend : conv.user;
            return (
              <MessageListItem
                key={conv.id}
                user={user}
                friend={friend}
                friendNickname={conv.friendNickname}
                onClick={handleSelectedUser}
              />
            );
          })}
        {listConversation.length === 0 && (
          <>
            <SkeletonMessage />
            <SkeletonMessage />
            <SkeletonMessage />
            <SkeletonMessage />
          </>
        )}
      </Grid>
    </Grid>
  );
}

export default React.memo(MessageList);

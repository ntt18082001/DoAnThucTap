import { Avatar, Box, IconButton, Tooltip, Typography } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { selectSelectedUser } from 'features/message/messageSlice';
import React from 'react';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import { grey } from '@mui/material/colors';
import { Message, SeenMessage } from 'models/messages.model';
import {
  useDeleteMessageMutation,
  useToggleLikeMessageMutation,
} from 'features/message/message.service';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import OptionsMenu from './OptionsMenu';
import 'photoswipe/dist/photoswipe.css'
import ImageItem from './Components/ImageItem';

type Props = {
  message: Message;
  me: boolean;
  isAvatar: boolean;
  lastSeen?: string;
};

const ConversationMessage = (props: Props) => {
  const selectedUser = useAppSelector(selectSelectedUser);
  const [toggleLikeMessage] = useToggleLikeMessageMutation();
  const [deleteMessage] = useDeleteMessageMutation();
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const bgColorHover = isDarkmode ? 'rgb(255,255,255,.2)' : '#dbdbdb';

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  let avatar;
  if (!props.me && props.isAvatar) {
    avatar = (
      <Tooltip title={selectedUser?.name} placement="left">
        <Avatar
          alt={selectedUser?.name}
          src={`${baseURL}/${selectedUser?.avatar}`}
          sx={{ width: 24, height: 24, mr: 1 }}
        />
      </Tooltip>
    );
  }

  const dateString = new Date(props.message.sentTime).toLocaleString();
  const content = props.message.content
    ? props.message.content.replace(
        /(https?:\/\/[^\s]+)/g,
        '<a class="url-msg" target="_blank" href="$1">$1</a>'
      )
    : null;

  const handleToggleLikeMessage = async () => {
    try {
      const data: SeenMessage = {
        senderId: props.message.senderId,
        receiverId: props.message.receiverId,
        id: props.message.id,
      };
      await toggleLikeMessage(data).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  const handleDeleteMessage = async () => {
    try {
      const data: SeenMessage = {
        senderId: props.message.senderId,
        receiverId: props.message.receiverId,
        id: props.message.id,
      };
      await deleteMessage(data).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Box
      display="flex"
      alignItems="flex-end"
      className={props.me ? 'me' : ''}
      sx={{ pl: props.isAvatar ? 1 : 5 }}
    >
      {props.me && props.lastSeen && props.lastSeen === props.message.id && (
        <Avatar
          alt={selectedUser?.name}
          src={`${baseURL}/${selectedUser?.avatar}`}
          sx={{ width: '16px', height: '16px', transition: '1s ease-in-out' }}
        />
      )}
      {avatar}
      {!props.message.isDelete && (
        <Tooltip title={dateString} placement="right">
          <Box
            sx={{
              maxWidth: '50%',
              mt: '5px',
              display: 'flex',
              gap: '5px',
              mr: props.me && props.lastSeen === props.message.id ? '' : '28px',
            }}
            className={props.me ? 'me' : ''}
          >
            {content && (
              <Typography
                variant="body2"
                sx={{
                  maxWidth: '100%',
                  backgroundColor: 'purple',
                  borderRadius: 3,
                  p: '8px 15px',
                  width: 'fit-content',
                  wordBreak: 'break-all',
                  wordWrap: 'break-word',
                  whiteSpace: 'pre-wrap',
                }}
                dangerouslySetInnerHTML={{ __html: content }}
              />
            )}
            {props.message.urlImage && (
              <ImageItem alt={props.message.urlImage} src={props.message.urlImage} />
            )}
            <IconButton
              aria-label="Like"
              size="small"
              sx={{
                color: props.message.isLiked ? '#2196f3' : grey[500],
                height: 'fit-content',
                m: 'auto',
              }}
              onClick={handleToggleLikeMessage}
            >
              <Tooltip title="Thích">
                <ThumbUpIcon fontSize="inherit" />
              </Tooltip>
            </IconButton>
            {props.message.senderId !== selectedUser?.id && (
              <>
                <IconButton
                  aria-label="Like"
                  size="small"
                  sx={{
                    p: 1,
                    color: grey[500],
                    ':hover': {
                      backgroundColor: bgColorHover,
                    },
                    height: 'fit-content',
                    m: 'auto',
                  }}
                  onClick={handleClick}
                  aria-controls={open ? 'options-menu' : undefined}
                  aria-haspopup="true"
                  aria-expanded={open ? 'true' : undefined}
                >
                  <Tooltip title="Tùy chọn">
                    <MoreVertIcon fontSize="inherit" />
                  </Tooltip>
                </IconButton>
                <OptionsMenu
                  open={open}
                  anchorEl={anchorEl}
                  handleClose={handleClose}
                  handleDeleteMessage={handleDeleteMessage}
                />
              </>
            )}
          </Box>
        </Tooltip>
      )}
      {props.message.isDelete && (
        <Box
          sx={{
            maxWidth: '50%',
            mt: '2px',
            display: 'flex',
            gap: '5px',
          }}
          className={props.me ? 'me' : ''}
        >
          <Typography
            variant="body2"
            sx={{
              maxWidth: '100%',
              backgroundColor: 'inherit',
              color: grey[500],
              border: `1px solid ${grey[500]}`,
              borderRadius: 3,
              p: '8px 15px',
              width: 'fit-content',
              wordBreak: 'break-all',
              wordWrap: 'break-word',
            }}
          >
            Tin nhắn đã bị xóa
          </Typography>
        </Box>
      )}
    </Box>
  );
};

export default ConversationMessage;
